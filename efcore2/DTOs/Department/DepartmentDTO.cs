using efcore2.Models;

namespace efcore2.DTOs;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class ExtensionsForDepartmentDTO
{
    public static DepartmentDTO ToDepartmentDTO(this Department department)
    {
        return new DepartmentDTO()
        {
            Id = department.Id,
            Name = department.Name,
        };
    }
}