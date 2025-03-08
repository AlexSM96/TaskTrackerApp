using TaskTracker.Application.Model.TaskModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.Mappers;

public static class TaskMapper
{
    public static TaskResponseDto ToTaskResponseDto(this TaskEntity taskEntity)
    {
        return new TaskResponseDto(
            taskEntity.Id, 
            taskEntity.Title, 
            taskEntity.Description, 
            taskEntity.AuthorId, 
            taskEntity.ExecutorId);
    }

    public static TaskListDto ToTaskListDto(this IEnumerable<TaskEntity> taskEntities)
    {
        return new TaskListDto(taskEntities
            .Select(x => x.ToTaskResponseDto())
            .ToList());
    }
}
