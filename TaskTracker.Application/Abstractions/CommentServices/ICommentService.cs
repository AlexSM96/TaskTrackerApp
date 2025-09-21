using TaskTracker.Application.Model.CommentModels;

namespace TaskTracker.Application.Abstractions.CommentServices;

public interface ICommentService
{
    public Task<IEnumerable<CommentDto>> GetComments(long taskId);

    public Task<bool> AddComment(CreateCommentDto createCommentDto);

    public Task<bool>UpdateComment(UpdateCommentDto updateCommentDto);

    public Task<bool> DeleteComment(long commentId);
}
