using System.ComponentModel.DataAnnotations;

namespace efcore2.DTOs;

public class CreateEmployeeDTO
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [Required]
    public DateOnly JoinedDate { get; set; }
}