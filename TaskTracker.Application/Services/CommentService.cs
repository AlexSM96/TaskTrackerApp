using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.CommentServices;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.CommentModels;

namespace TaskTracker.Application.Services
{
    public class CommentService(ITaskTrackerDbContext context) : ICommentService
    {
        private readonly ITaskTrackerDbContext _context = context;

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
                await _context.Comments.AddAsync(new Domain.Entities.CommentEntity()
                {
                    Text = createCommentDto.Text,
                    AuthorId = createCommentDto.AuthorId,
                    TaskId = createCommentDto.TaskId,
                    CreatedAt = DateTime.UtcNow,
                });


                await _context.SaveChangesAsync();

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
