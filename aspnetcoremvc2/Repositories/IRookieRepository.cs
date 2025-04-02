using aspnetcoremvc2.Models;

namespace aspnetcoremvc2.Repositories;

public interface IRookieRepository
{
    public List<Person> GetAll();

    public Person GetById(int id);

    public List<Person> GetMales();

    public Person? GetOldest();

    public List<string> GetFullNames();

    public List<Person> GetRookiesBornInYear(int year);

    public List<Person> GetRookiesBornBeforeYear(int year);

    public List<Person> GetRookiesBornAfterYear(int year);

    public Person Create(Person rookie);

    public bool Update(Person rookie);

    public Person Delete(int id);
}