using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Abstractions.TaskServices;
using TaskTracker.Application.Extensions.FilterExtensions;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.Filters;
using TaskTracker.Application.Model.TaskModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services;

public class TaskTrackerService(
    ITaskTrackerDbContext dbContext, 
    UserManager<UserEntity> userManager) : ITaskTrackService
{
    private readonly ITaskTrackerDbContext _dbContext = dbContext;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<TaskResponseDto> CreateTask(CreateTaskDto createTaskDto)
    {
        if (createTaskDto is null)
        {
            throw new ArgumentNullException(nameof(CreateTaskDto));
        }

        var executor = createTaskDto.ExecutorId is null
            ? null
            : await _userManager.FindByIdAsync(createTaskDto.ExecutorId.Value.ToString());

        var entityEntry = await _dbContext.Tasks.AddAsync(new TaskEntity
        {
            Title = createTaskDto.Title!,
            Description = createTaskDto.Description!,
            AuthorId = createTaskDto.AuthorId!.Value,
            ExecutorId = executor?.Id,
            Executed = false
        });

        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity.ToTaskResponseDto();
    }

    public async Task<TaskResponseDto> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        var executor = await _userManager.FindByIdAsync(updateTaskDto.ExecutorId!.Value.ToString());
        var author = await _userManager.FindByIdAsync(updateTaskDto.AuthorId!.Value.ToString());
        var currentUser = await _userManager.FindByIdAsync(updateTaskDto.CurrentUserId.ToString());

        if (currentUser != author) 
        {
            throw new Exception("Only Author can change task");
        }

        var task = await _dbContext.Tasks
            .Where(t => t.Id == updateTaskDto.Id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(t => t.Title, updateTaskDto.Title)
                .SetProperty(t => t.Description, updateTaskDto.Description)
                .SetProperty(t => t.StartWorkDate, updateTaskDto.StartWorkDate)
                .SetProperty(t => t.EndWorkDate, updateTaskDto.EndWorkDate)
                .SetProperty(t => t.Executed, updateTaskDto.Executed)
                .SetProperty(t => t.AuthorId, author!.Id)
                .SetProperty(t => t.ExecutorId, executor!.Id)
                .SetProperty(t => t.InWork, updateTaskDto.InWork)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

        return (await _dbContext.Tasks.FindAsync(updateTaskDto.Id))?.ToTaskResponseDto()!;
    }

    public async Task<TaskListDto> GetTasks(TaskFilter filter)
    {
        return (await _dbContext.Tasks
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Executor)
            .Filter(filter)
            .ToListAsync())
            .ToTaskListDto();
    }
}
