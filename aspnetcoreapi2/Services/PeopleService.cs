using aspnetcoreapi2.Models;
using aspnetcoreapi2.Repositories;

namespace aspnetcoreapi2.Services;

public class PeopleService : IPeopleService
{
    private IPeopleRepository _peopleRepository;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public Person? GetById(int id)
    {
        return _peopleRepository.GetById(id);
    }

    public List<Person> GetMany(string? firstName, string? lastName, Gender? gender, string? birthPlace)
    {
        return _peopleRepository.GetMany(firstName, lastName, gender, birthPlace);
    }

    public Person? Create(CreatePersonDTO createPersonDTO)
    {
        return _peopleRepository.Create(createPersonDTO);
    }

    public Person? Update(int id, UpdatePersonDTO updatePersonDTO)
    {
        return _peopleRepository.Update(id, updatePersonDTO);
    }

    public bool Delete(Person person)
    {
        return _peopleRepository.Delete(person);
    }
}