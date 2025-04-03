namespace aspnetcoreapi1.Models;

public class TaskDTO
{
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public TaskDTO(string title, bool isCompleted)
    {
        Title = title;
        IsCompleted = isCompleted;
    }
}