using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.CommentServices;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Abstractions.Notifications;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.CommentModels;
using TaskTracker.Application.Model.Notifications;

namespace TaskTracker.Application.Services
{
    public class CommentService(ITaskTrackerDbContext context, 
        INotificationService notificationService) : ICommentService
    {
        private readonly ITaskTrackerDbContext _context = context;
        private readonly INotificationService _notificationService = notificationService;

        public async Task<IEnumerable<CommentDto>> GetComments(long taskId)
        {
            return await _context.Comments
                .AsNoTracking()
                .Where(x => x.TaskId == taskId)
                .Include(x => x.Task)
                .Include(x => x.Author)
                .Select(x => x.ToDto())
                .ToListAsync();
        }

        public async Task<bool> AddComment(CreateCommentDto createCommentDto)
        {
            try
            {
                var comment = await _context.Comments.AddAsync(new Domain.Entities.CommentEntity()
                {
                    Text = createCommentDto.Text,
                    AuthorId = createCommentDto.AuthorId,
                    TaskId = createCommentDto.TaskId,
                    CreatedAt = DateTime.UtcNow,
                });
                
                await _context.SaveChangesAsync();
                await _notificationService.SendNotificationAsync(new NotificationDto()
                {
                    Message = "Новый комментарий к задаче: " + comment.Entity.TaskId,
                    Data = JsonSerializer.Serialize(comment.Entity),
                    UserIds = [],
                    TaskIds = [comment.Entity.TaskId],
                    IsRead = false,
                });
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            try
            {
               int countUpdatedRow = await _context.Comments
                    .Where(x => x.Id == updateCommentDto.CommentId 
                        && x.AuthorId == updateCommentDto.AuthorId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(e => e.Text, updateCommentDto.Text)
                        .SetProperty(e => e.UpdatedAt, DateTime.UtcNow));

                return countUpdatedRow > 0;
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        public async Task<bool> DeleteComment(long commentId)
        {
            try
            {
                int countDeletedRow = await _context.Comments
                    .Where(x => x.Id == commentId)
                    .ExecuteDeleteAsync();

                return countDeletedRow > 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
