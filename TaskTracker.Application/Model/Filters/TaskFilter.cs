namespace TaskTracker.Application.Model.Filters;

public class TaskFilter
{
    public DateTime? CreatedAt { get; set; }

    public string SortOrder { get; set; } = "desc";

    public string SortItem { get; set; } = "date";

    public string? Title { get; set; }

    public long? ExecutorId { get; set; }

    public long? AuthorId { get; set; }

    public int? TaskStatus { get; set; }
}
