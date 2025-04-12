using efcore2.Models;

namespace efcore2.DTOs;

public class ProjectEmployeeDTO
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public bool Enable { get; set; }
}

public static class ExtensionsForProjectEmployeeDTO
{
    public static ProjectEmployeeDTO ToProjectEmployeeDTO(this ProjectEmployee projectEmployee)
    {
        return new ProjectEmployeeDTO()
        {
            EmployeeId = projectEmployee.Employee.Id,
            Name = projectEmployee.Employee.Name,
            Department = projectEmployee.Employee.Department.Name,
            Enable = projectEmployee.Enable
        };
    }
}