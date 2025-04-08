using System.ComponentModel.DataAnnotations;

namespace aspnetcoreapi2.Models;

public class UpdatePersonDTO
{
    [Required]
    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(@"^[A-Z]+[A-Za-z\s]*$")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(@"^[A-Z]+[A-Za-z\s]*$")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
    public Gender Gender { get; set; }

    [Required]
    [StringLength(100)]
    public string BirthPlace { get; set; }

    public UpdatePersonDTO() { }

    public UpdatePersonDTO(string firstName, string lastName, Gender gender, DateOnly dateOfBirth, string birthPlace)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        BirthPlace = birthPlace;
    }
}

