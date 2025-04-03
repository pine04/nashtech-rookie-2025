namespace aspnetcoreapi1.Models;

public class TaskUpdateDTO
{
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public TaskUpdateDTO(string? title, bool? isCompleted)
    {
        Title = title;
        IsCompleted = isCompleted;
    }
}