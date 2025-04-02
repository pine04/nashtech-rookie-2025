using Microsoft.Extensions.Caching.Memory;
using aspnetcoremvc2.Models;
using aspnetcoremvc2.Controllers;

namespace aspnetcoremvc2.Repositories;

public class InMemoryRookieRepository : IRookieRepository
{
    private IMemoryCache _cache;
    private static int CurrentId = 1;

    public InMemoryRookieRepository(IMemoryCache cache)
    {
        _cache = cache;

        if (!_cache.TryGetValue("rookies", out _))
        {
            List<Person> rookies = new List<Person>
            {
                new Person(GenerateId(), "Tung", "Ta", Gender.Male, new DateOnly(2004, 9, 21), "0921426803", "Hanoi", false),
                new Person(GenerateId(), "Bach", "Dang", Gender.Male, new DateOnly(2002, 9, 8), "0123456789", "Hanoi", false),
                new Person(GenerateId(), "Chien", "Thai", Gender.Male, new DateOnly(2003, 7, 29), "0987654321", "Hanoi", false),
                new Person(GenerateId(), "Jane", "Doe", Gender.Female, new DateOnly(1995, 3, 22), "0123465789", "Paris", true),
                new Person(GenerateId(), "Amelia", "Watson", Gender.Female, new DateOnly(1992, 7, 1), "0921426803", "Washington D.C.", false),
                new Person(GenerateId(), "Sophia", "Carter", Gender.Female, new DateOnly(2000, 10, 10), "0921426803", "London", true),
            };

            _cache.Set("rookies", rookies);
        }
    }

    private static int GenerateId()
    {
        return CurrentId++;
    }

    public List<Person> GetRookiesBornInYear(int year)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies")?.Where(person => person.DateOfBirth.Year == year).ToList();

        if (rookies == null)
        {
            return new List<Person>();
        }
        else
        {
            return rookies;
        }
    }

    public List<string> GetFullNames()
    {
        List<string>? names = _cache.Get<List<Person>?>("rookies")?.Select(person => $"{person.LastName} {person.FirstName}").ToList();

        if (names == null)
        {
            return new List<string>();
        }
        else
        {
            return names;
        }
    }

    public List<Person> GetMales()
    {
        List<Person>? males = _cache.Get<List<Person>?>("rookies")?.Where(person => person.Gender == Gender.Male).ToList();

        if (males == null)
        {
            return new List<Person>();
        }
        else
        {
            return males;
        }
    }

    public Person? GetOldest()
    {
        return _cache.Get<List<Person>?>("rookies")?.MinBy(person => person.DateOfBirth);
    }

    public List<Person> GetRookiesBornAfterYear(int year)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies")?.Where(person => person.DateOfBirth.Year > year).ToList();

        if (rookies == null)
        {
            return new List<Person>();
        }
        else
        {
            return rookies;
        }
    }

    public List<Person> GetRookiesBornBeforeYear(int year)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies")?.Where(person => person.DateOfBirth.Year < year).ToList();

        if (rookies == null)
        {
            return new List<Person>();
        }
        else
        {
            return rookies;
        }
    }

    public List<Person> GetAll()
    {
        return _cache.Get<List<Person>?>("rookies") ?? new List<Person>();
    }

    public Person GetById(int id)
    {
        return _cache.Get<List<Person>?>("rookies")?.Find(rookie => rookie.Id == id);
    }

    public Person Create(Person rookie)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies");

        if (rookies == null || rookies.Count == 0)
        {
            return null;
        }

        rookie.Id = GenerateId();
        rookies.Add(rookie);

        return rookie;
    }

    public bool Update(Person rookie)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies");

        if (rookies == null || rookies.Count == 0)
        {
            return false;
        }

        int index = rookies.FindIndex(oldRookie => oldRookie.Id == rookie.Id);

        if (index == -1)
        {
            return false;
        }

        Person oldRookie = rookies[index];
        oldRookie.FirstName = rookie.FirstName;
        oldRookie.LastName = rookie.LastName;
        oldRookie.Gender = rookie.Gender;
        oldRookie.DateOfBirth = rookie.DateOfBirth;
        oldRookie.PhoneNumber = rookie.PhoneNumber;
        oldRookie.BirthPlace = rookie.BirthPlace;
        oldRookie.IsGraduated = rookie.IsGraduated;

        return true;
    }

    public Person Delete(int id)
    {
        List<Person>? rookies = _cache.Get<List<Person>?>("rookies");

        if (rookies == null || rookies.Count == 0)
        {
            return null;
        }

        int index = rookies.FindIndex(rookie => rookie.Id == id);

        if (index == -1)
        {
            return null;
        }

        Person removedRookie = rookies[index];
        rookies.RemoveAt(index);
        return removedRookie;
    }
}