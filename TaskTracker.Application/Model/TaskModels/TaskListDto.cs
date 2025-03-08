namespace TaskTracker.Application.Model.TaskModels;

public record TaskListDto(IList<TaskResponseDto> TaskDtos);
