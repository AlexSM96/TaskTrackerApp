namespace TaskTracker.Application.Model.TaskModels;

public record TaskResponseDto(
    long Id, 
    string Title, 
    string Description, 
    long AuthorId,
    long? Executor = null);
