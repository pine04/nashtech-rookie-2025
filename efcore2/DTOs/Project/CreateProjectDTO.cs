using System.ComponentModel.DataAnnotations;

namespace efcore2.DTOs;

public class CreateProjectDTO
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
}