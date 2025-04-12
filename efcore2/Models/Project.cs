namespace efcore2.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    // public ICollection<Employee> Employees { get; set; }
}