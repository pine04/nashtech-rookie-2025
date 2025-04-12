using System.ComponentModel.DataAnnotations;

namespace efcore2.DTOs;

public class CreateSalaryDTO
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public double Amount { get; set; }
}