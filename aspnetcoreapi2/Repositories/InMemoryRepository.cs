using Microsoft.Extensions.Caching.Memory;
using aspnetcoreapi2.Models;

namespace aspnetcoreapi2.Repositories;

public class InMemoryPeopleRepository : IPeopleRepository
{
    private IMemoryCache _cache;
    private static int CurrentId = 1;

    public InMemoryPeopleRepository(IMemoryCache cache)
    {
        _cache = cache;

        if (!_cache.TryGetValue("People", out _))
        {
            List<Person> people = new List<Person> {
                new Person(GenerateId(), "Tung", "Ta", Gender.Male, new DateOnly(2004, 9, 21), "Hanoi"),
                new Person(GenerateId(), "Bach", "Dang", Gender.Male, new DateOnly(2002, 9, 8), "Hanoi"),
                new Person(GenerateId(), "Chien", "Thai", Gender.Male, new DateOnly(2003, 7, 29), "Hanoi"),
                new Person(GenerateId(), "Jane", "Doe", Gender.Female, new DateOnly(1995, 3, 22), "Paris"),
                new Person(GenerateId(), "Amelia", "Watson", Gender.Female, new DateOnly(1992, 7, 1), "Washington D.C."),
                new Person(GenerateId(), "Sophia", "Carter", Gender.NonBinary, new DateOnly(2000, 10, 10), "London"),
            };

            _cache.Set("People", people);
        }
    }

    private static int GenerateId()
    {
        return CurrentId++;
    }

    public Person? GetById(int id)
    {
        List<Person>? people = _cache.Get<List<Person>?>("People");

        if (people == null)
        {
            return null;
        }

        return people.Find(person => person.Id == id);
    }

    public List<Person> GetMany(string? firstName, string? lastName, Gender? gender, string? birthPlace)
    {
        List<Person>? people = _cache.Get<List<Person>?>("People");

        if (people == null)
        {
            return new List<Person>();
        }

        people = people.Where(person =>
                (firstName == null || person.FirstName.Equals(firstName)) &&
                (lastName == null || person.LastName.Equals(lastName)) &&
                (gender == null || person.Gender.Equals(gender)) &&
                (birthPlace == null || person.BirthPlace.Equals(birthPlace)))
            .ToList();

        return people;
    }

    public Person? Create(CreatePersonDTO createPersonDTO)
    {
        List<Person>? people = _cache.Get<List<Person>?>("People");

        if (people == null)
        {
            return null;
        }

        Person person = new Person()
        {
            Id = GenerateId(),
            FirstName = createPersonDTO.FirstName,
            LastName = createPersonDTO.LastName,
            DateOfBirth = createPersonDTO.DateOfBirth,
            Gender = createPersonDTO.Gender,
            BirthPlace = createPersonDTO.BirthPlace
        };

        people.Add(person);

        return person;
    }

    public Person? Update(int id, UpdatePersonDTO updatePersonDTO)
    {
        List<Person>? people = _cache.Get<List<Person>?>("People");

        if (people == null)
        {
            return null;
        }

        Person? person = people.Find(person => person.Id == id);

        if (person == null)
        {
            return null;
        }

        person.FirstName = updatePersonDTO.FirstName;
        person.LastName = updatePersonDTO.LastName;
        person.DateOfBirth = updatePersonDTO.DateOfBirth;
        person.Gender = updatePersonDTO.Gender;
        person.BirthPlace = updatePersonDTO.BirthPlace;

        return person;
    }

    public bool Delete(Person person)
    {
        List<Person>? people = _cache.Get<List<Person>?>("People");

        if (people == null)
        {
            return false;
        }

        people.Remove(person);
        return true;
    }
}