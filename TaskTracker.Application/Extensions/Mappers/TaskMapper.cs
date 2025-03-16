using TaskTracker.Application.Model.TaskModels;
using TaskTracker.Application.Model.UserModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.Mappers;

public static class TaskMapper
{
    public static TaskResponseDto ToTaskResponseDto(this TaskEntity taskEntity)
    {
        return new TaskResponseDto
        {
            Id = taskEntity.Id,
            Title = taskEntity.Title,
            Description = taskEntity.Description,
            Executed = taskEntity.Executed,
            InWork = taskEntity.InWork,
            EndWorkDate = taskEntity.EndWorkDate,
            CreatedAt = taskEntity.CreatedAt,
            Author = new UserResponseDto()
            {
                Id = taskEntity.AuthorId,
                Email = taskEntity.Author?.Email,
                Username = taskEntity.Author?.UserName,
            },
            Executor = new UserResponseDto
            {
                Id = taskEntity.ExecutorId,
                Email = taskEntity.Executor?.Email,
                Username = taskEntity.Executor?.UserName
            } 
        };

    }

    public static TaskListDto ToTaskListDto(this IEnumerable<TaskEntity> taskEntities)
    {
        return new TaskListDto(taskEntities
            .Select(x => x.ToTaskResponseDto())
            .ToList());
    }
}
