using efcore2.Models;

namespace efcore2.DTOs;

public class SalaryDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeDepartment { get; set; }
    public double Amount { get; set; }
}

public static class ExtensionsForSalaryDTO
{
    public static SalaryDTO ToSalaryDTO(this Salary salary)
    {
        return new SalaryDTO()
        {
            Id = salary.Id,
            EmployeeId = salary.Employee.Id,
            EmployeeName = salary.Employee.Name,
            EmployeeDepartment = salary.Employee.Department.Name,
            Amount = salary.Amount
        };
    }
}