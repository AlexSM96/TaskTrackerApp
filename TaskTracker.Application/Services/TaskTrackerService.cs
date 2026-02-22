using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Abstractions.Notifications;
using TaskTracker.Application.Abstractions.TaskServices;
using TaskTracker.Application.Extensions.FilterExtensions;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.Filters;
using TaskTracker.Application.Model.Notifications;
using TaskTracker.Application.Model.TaskModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services;

public class TaskTrackerService(
    ITaskTrackerDbContext dbContext,
    INotificationService notificationService,
    UserManager<UserEntity> userManager) : ITaskTrackService
{
    public async Task<TaskResponseDto> CreateTask(CreateTaskDto createTaskDto)
    {
        if (createTaskDto is null)
        {
            throw new ArgumentNullException(nameof(CreateTaskDto));
        }

        var executor = createTaskDto.ExecutorId is null
            ? null
            : await userManager.FindByIdAsync(createTaskDto.ExecutorId.Value.ToString());

        var entityEntry = await dbContext.Tasks.AddAsync(new TaskEntity
        {
            Title = createTaskDto.Title!,
            Description = createTaskDto.Description!,
            AuthorId = createTaskDto.AuthorId!.Value,
            ExecutorId = executor?.Id,
            WorkStatus = Domain.Models.TaskWorkStatus.None
        });

        await dbContext.SaveChangesAsync();

        var taskDto = entityEntry.Entity.ToTaskResponseDto();
        await notificationService.SendNotificationAsync(new NotificationDto()
        {
            Message = "Новая задча: " + taskDto.Title,
            Data = JsonSerializer.Serialize(taskDto),
            UserIds = taskDto is { Executor.Id: not null } ? [taskDto.Executor.Id.Value] : [],
            TaskIds = [taskDto.Id],
            IsRead = false,
        });
        
        return taskDto;
    }

    public async Task<TaskResponseDto> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        var executor = await userManager.FindByIdAsync(updateTaskDto.ExecutorId!.Value.ToString());
        var author = await userManager.FindByIdAsync(updateTaskDto.AuthorId!.Value.ToString());
        var currentUser = await userManager.FindByIdAsync(updateTaskDto.CurrentUserId.ToString());

        if (currentUser != author) 
        {
            throw new Exception("Only Author can change task");
        }

        var task = await dbContext.Tasks
            .Where(t => t.Id == updateTaskDto.Id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(t => t.Title, updateTaskDto.Title)
                .SetProperty(t => t.Description, updateTaskDto.Description)
                .SetProperty(t => t.StartWorkDate, updateTaskDto.StartWorkDate)
                .SetProperty(t => t.EndWorkDate, updateTaskDto.EndWorkDate)
                .SetProperty(t => t.WorkStatus, updateTaskDto.TaskWorkStatus)
                .SetProperty(t => t.AuthorId, author!.Id)
                .SetProperty(t => t.ExecutorId, executor!.Id)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

        var taskDto = (await dbContext.Tasks.FindAsync(updateTaskDto.Id))?.ToTaskResponseDto()!;
        await notificationService.SendNotificationAsync(new NotificationDto()
        {
            Message = "Новая задча: " + taskDto.Title,
            Data = JsonSerializer.Serialize(taskDto),
            UserIds = taskDto is { Executor.Id: not null } ? [taskDto.Executor.Id.Value] : [],
            TaskIds = [taskDto.Id],
            IsRead = false,
        });
        return taskDto;
    }

    public async Task<TaskListDto> GetTasks(TaskFilter filter)
    {
        return (await dbContext.Tasks
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Executor)
            .Filter(filter)
            .ToListAsync())
            .ToTaskListDto();
    }
}
