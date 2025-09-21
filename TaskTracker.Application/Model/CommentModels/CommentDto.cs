using TaskTracker.Application.Model.TaskModels;
using TaskTracker.Application.Model.UserModels;

namespace TaskTracker.Application.Model.CommentModels;

public record CommentDto(long Id, string Text, UserDto Author, TaskResponseDto Task, DateTime createdAt);

