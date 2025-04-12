using efcore2.Models;

namespace efcore2.DTOs;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public DateOnly JoinedDate { get; set; }
    public double? Salary { get; set; }
}

public static class ExtensionsForEmployeeDTO
{
    public static EmployeeDTO ToEmployeeDTO(this Employee employee)
    {
        return new EmployeeDTO()
        {
            Id = employee.Id,
            Name = employee.Name,
            Department = employee.Department.Name,
            JoinedDate = employee.JoinedDate,
            Salary = employee.Salary?.Amount
        };
    }
}