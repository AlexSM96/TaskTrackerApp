using System.Linq.Expressions;
using TaskTracker.Application.Model.Filters;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.FilterExtensions
{
    public static class TaskFilterExtension
    {
        public static IQueryable<TaskEntity> Filter(this IQueryable<TaskEntity> entities, TaskFilter filter)
        {
            Expression<Func<TaskEntity, object>> sortItem = filter.SortItem?.ToLower() switch
            {
                "date" => t => t.CreatedAt,
                "title" => t => t.Title,
                _ => t => t.CreatedAt
            };


            var filteredData = entities.
                 Where(x => filter != null
                      && (filter.CreatedAt == null || x.CreatedAt == filter.CreatedAt.Value)
                      && (string.IsNullOrWhiteSpace(filter.Title) || x.Title.ToLower().Contains(filter.Title.ToLower()))
                      && (filter.ExecutorId == null || x.ExecutorId == filter.ExecutorId)
                      && (filter.AuthorId == null || x.AuthorId == filter.AuthorId)
                      && (string.IsNullOrWhiteSpace(filter.TaskStatus) 
                          || (filter.TaskStatus == "inWork" && x.InWork == true)
                          || (filter.TaskStatus == "executed" && x.Executed == true))
                 );

            return filteredData = filter.SortOrder.ToLower() == "desc"
                ? filteredData.OrderByDescending(sortItem)
                : filteredData.OrderBy(sortItem);
        }
    }
}
