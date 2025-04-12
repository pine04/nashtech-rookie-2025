using efcore2.Models;

namespace efcore2.DTOs;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProjectEmployeeDTO> Employees { get; set; }
}

public static class ExtensionsForProjectDTO
{
    public static ProjectDTO ToProjectDTO(this Project project)
    {
        return new ProjectDTO()
        {
            Id = project.Id,
            Name = project.Name,
            Employees = project.ProjectEmployees.Select(pe => pe.ToProjectEmployeeDTO()).ToList()
        };
    }
}