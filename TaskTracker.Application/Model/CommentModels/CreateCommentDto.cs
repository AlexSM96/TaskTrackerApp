namespace TaskTracker.Application.Model.CommentModels;

public class CreateCommentDto
{
    public string Text { get; set; }

    public long TaskId { get; set; }

    public long AuthorId { get; set; }
}

public class UpdateCommentDto : CreateCommentDto
{
    public long CommentId { get; set; }
}
