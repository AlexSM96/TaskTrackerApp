using TaskTracker.Application.Model.Filters;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.FilterExtensions
{
    public static class TaskFilterExtension
    {
        public static IQueryable<TaskEntity> Filter(this IQueryable<TaskEntity> entities, TaskFilter filter)
        {
            return entities.
                Where(x => filter != null 
                     && (filter.CreatedAt == null || x.CreatedAt == filter.CreatedAt.Value)
                     && (string.IsNullOrWhiteSpace(filter.Title) || x.Title.ToLower().Contains(filter.Title.ToLower()))
                     && (string.IsNullOrWhiteSpace(filter.Description) || x.Description.ToLower().Contains(filter.Description.ToLower()))
                     && (filter.Executed == null || x.Executed == filter.Executed.Value)
                     && (filter.ExecutorId == null || x.ExecutorId == filter.ExecutorId)
                     && (filter.AuthorId == null || x.AuthorId == filter.AuthorId));
        }
    }
}
