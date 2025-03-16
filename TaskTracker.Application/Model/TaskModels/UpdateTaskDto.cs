namespace TaskTracker.Application.Model.TaskModels;

public class UpdateTaskDto : CreateTaskDto
{
    public long Id { get; set; }
    
    public bool? Executed { get; set; }

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public bool InWork { get; set; }

}