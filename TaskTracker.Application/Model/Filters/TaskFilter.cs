namespace TaskTracker.Application.Model.Filters
{
    public class TaskFilter
    {
        public DateTime? CreatedAt { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public long? ExecutorId { get; set; }

        public long? AuthorId { get; set; }

        public bool? Executed { get; set; }
    }
}
