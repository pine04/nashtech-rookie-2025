using System.ComponentModel.DataAnnotations;

namespace efcore2.DTOs;

public class UpdateProjectEmployeeDTO
{
    [Required]
    public bool Enable { get; set; }
}