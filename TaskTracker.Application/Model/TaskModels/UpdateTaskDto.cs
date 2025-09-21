using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Model.TaskModels;

public class UpdateTaskDto : CreateTaskDto
{
    public long Id { get; set; }

    public long? CurrentUserId { get; set; }
   
    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public TaskWorkStatus TaskWorkStatus { get; set; }

}