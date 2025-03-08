namespace TaskTracker.Application.Model.TaskModels;

public class CreateTaskDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public long AuthorId { get; set; }

    public long? ExecutorId { get; set; }
}
