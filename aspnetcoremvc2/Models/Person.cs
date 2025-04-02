using System.ComponentModel.DataAnnotations;

namespace aspnetcoremvc2.Models;

public class Person : IExcelExportable
{
    public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(@"^[A-Z]+[A-Za-z\s]*$")]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(@"^[A-Z]+[A-Za-z\s]*$")]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
    public Gender Gender { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [RegularExpression(@"^\d{10}$")]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Birth place")]
    public string BirthPlace { get; set; }

    [Display(Name = "Is graduated")]
    public bool IsGraduated { get; set; }

    public Person() { }

    public Person(int id, string firstName, string lastName, Gender gender, DateOnly dateOfBirth, string phoneNumber, string birthPlace, bool isGraduated)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
        IsGraduated = isGraduated;
    }

    public string[] GetHeaders()
    {
        return ["Id", "First name", "Last name", "Gender", "Date of Birth", "Phone Number", "Birthplace", "Is Graduated"];
    }

    public string[] ToRow()
    {
        return [
            Id.ToString(),
            FirstName,
            LastName,
            Gender.ToString(),
            DateOfBirth.ToString(),
            PhoneNumber,
            BirthPlace,
            IsGraduated ? "Yes" : "No"
        ];
    }
}

public enum Gender
{
    Male,
    Female,
    NonBinary
}