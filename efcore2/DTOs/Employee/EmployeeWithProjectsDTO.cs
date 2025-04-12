using efcore2.Models;

namespace efcore2.DTOs;

public class EmployeeWithProjectsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public DateOnly JoinedDate { get; set; }
    public double? Salary { get; set; }
    public List<ProjectWithoutEmployeesDTO> Projects { get; set; }
}

public class ProjectWithoutEmployeesDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}