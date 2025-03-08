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

public class TaskTrackerService(ITaskTrackerDbContext dbContext, UserManager<UserEntity> userManager) : ITaskTrackService
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
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            AuthorId = createTaskDto.AuthorId,
            ExecutorId = executor?.Id,
            Executed = false,
        });

        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity.ToTaskResponseDto();
    }

    public async Task<TaskResponseDto> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        var existedTask = await _dbContext.Tasks.FindAsync(updateTaskDto.Id);

        if (existedTask is null)
        {
            throw new Exception($"Not exist task with id {updateTaskDto.Id}");
        }

        var executor = await _userManager.FindByIdAsync(updateTaskDto.ExecutorId!.Value.ToString());
        var author = await _userManager.FindByIdAsync(updateTaskDto.AuthorId.ToString());

        var task = await _dbContext.Tasks
            .Where(t => t.Id == updateTaskDto.Id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(t => t.StartWorkDate, updateTaskDto.StartWorkDate)
                .SetProperty(t => t.EndWorkDate, updateTaskDto.EndWorkDate)
                .SetProperty(t => t.Executed, updateTaskDto.Executed)
                .SetProperty(t => t.Author, author)
                .SetProperty(t => t.Executor, executor));

        return existedTask.ToTaskResponseDto();
    }

    public async Task<TaskListDto> GetTasks(TaskFilter filter)
    {
        return (await _dbContext.Tasks
            .AsNoTracking()
            .Filter(filter)
            .ToListAsync())
            .ToTaskListDto();
    }
}
