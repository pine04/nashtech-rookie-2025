using aspnetcoreapi2.Models;

namespace aspnetcoreapi2.Repositories;

public interface IPeopleRepository
{
    public Person? GetById(int id);

    public List<Person> GetMany(string? firstName, string? lastName, Gender? gender, string? birthPlace);

    public Person? Create(CreatePersonDTO createPersonDTO);

    public Person? Update(int id, UpdatePersonDTO updatePersonDTO);

    public bool Delete(Person person);
}