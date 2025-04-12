namespace efcore2.Models;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public DateOnly JoinedDate { get; set; }

    public Salary? Salary { get; set; }

    public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    // public ICollection<Project> Projects { get; set; }
}