using TaskTracker.Application.Model.UserModels;
using TaskTracker.Domain.Models;

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

    public TaskWorkStatus TaskWorkStatus { get; set; }

}
