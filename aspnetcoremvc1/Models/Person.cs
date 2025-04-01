using System.Text.Json;
using System.Text.Json.Serialization;

namespace aspnetcoremvc1.Models;

public class Person
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    [JsonConverter(typeof(GenderSerializer))]
    public Gender Gender { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public string PhoneNumber { get; private set; }

    public string BirthPlace { get; private set; }

    public bool IsGraduated { get; private set; }

    public Person(string firstName, string lastName, Gender gender, DateOnly dateOfBirth, string phoneNumber, string birthPlace, bool isGraduated)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
        IsGraduated = isGraduated;
    }
}

public enum Gender
{
    Male,
    Female,
    NonBinary
}

public class GenderSerializer : JsonConverter<Gender>
{
    public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Enum.Parse<Gender>(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}