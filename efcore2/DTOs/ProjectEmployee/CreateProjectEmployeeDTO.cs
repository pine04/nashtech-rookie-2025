using System.ComponentModel.DataAnnotations;

namespace efcore2.DTOs;

public class CreateProjectEmployeeDTO
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public bool Enable { get; set; }
}