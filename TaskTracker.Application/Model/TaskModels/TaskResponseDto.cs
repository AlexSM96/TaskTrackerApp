using TaskTracker.Application.Model.UserModels;

namespace TaskTracker.Application.Model.TaskModels;

public class TaskResponseDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public UserResponseDto? Executor { get; set; }

    public UserResponseDto Author { get; set; }

    public bool? Executed { get; set; }

    public bool? InWork { get; set; }

}
