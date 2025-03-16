using TaskTracker.Application.Model.Filters;
using TaskTracker.Application.Model.TaskModels;

namespace TaskTracker.Application.Abstractions.TaskServices;

public interface ITaskTrackService
{
    public Task<TaskListDto> GetTasks(TaskFilter filter);

    public Task<TaskResponseDto?> CreateTask(CreateTaskDto createTaskDto);

    public Task<TaskResponseDto?> UpdateTask(UpdateTaskDto updateTaskDto);
}
