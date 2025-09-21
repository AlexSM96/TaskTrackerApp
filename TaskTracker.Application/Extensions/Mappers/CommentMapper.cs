using TaskTracker.Application.Model.CommentModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(this CommentEntity commentEntity)
    {
        return new CommentDto(
            commentEntity.Id,
            commentEntity.Text,
            commentEntity.Author.ToDto(),
            commentEntity.Task.ToTaskResponseDto(),
            commentEntity.CreatedAt);
    }
}
