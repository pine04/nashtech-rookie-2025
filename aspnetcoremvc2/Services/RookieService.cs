using aspnetcoremvc2.Models;
using aspnetcoremvc2.Repositories;

namespace aspnetcoremvc2.Services;

public class RookieService : IRookieService
{
    private IRookieRepository _rookieRepository;

    public RookieService(IRookieRepository rookieRepository)
    {
        _rookieRepository = rookieRepository;
    }

    public Person GetById(int id)
    {
        return _rookieRepository.GetById(id);
    }

    public List<Person> GetRookiesBornInYear(int year)
    {
        return _rookieRepository.GetRookiesBornInYear(year);
    }

    public List<string> GetFullNames()
    {
        return _rookieRepository.GetFullNames();
    }

    public List<Person> GetMales()
    {
        return _rookieRepository.GetMales();
    }

    public Person? GetOldest()
    {
        return _rookieRepository.GetOldest();
    }

    public List<Person> GetRookiesBornAfterYear(int year)
    {
        return _rookieRepository.GetRookiesBornAfterYear(year);
    }

    public List<Person> GetRookiesBornBeforeYear(int year)
    {
        return _rookieRepository.GetRookiesBornBeforeYear(year);
    }

    public List<Person> GetAll()
    {
        return _rookieRepository.GetAll();
    }

    public Person Create(Person rookie)
    {
        return _rookieRepository.Create(rookie);
    }

    public bool Update(Person rookie)
    {
        return _rookieRepository.Update(rookie);
    }

    public Person Delete(int id)
    {
        return _rookieRepository.Delete(id);
    }
}