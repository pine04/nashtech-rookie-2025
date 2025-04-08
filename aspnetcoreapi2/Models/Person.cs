namespace aspnetcoreapi2.Models;

public class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string BirthPlace { get; set; }

    public Person() { }

    public Person(int id, string firstName, string lastName, Gender gender, DateOnly dateOfBirth, string birthPlace)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        BirthPlace = birthPlace;
    }
}

