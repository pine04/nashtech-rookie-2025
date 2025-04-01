using aspnetcoremvc1.Models;

namespace aspnetcoremvc1.Repositories;

public interface IRookieRepository
{
    public List<Person> GetAll();

    public List<Person> GetMales();

    public Person? GetOldest();

    public List<string> GetFullNames();

    public List<Person> GetRookiesBornInYear(int year);

    public List<Person> GetRookiesBornBeforeYear(int year);

    public List<Person> GetRookiesBornAfterYear(int year);
}