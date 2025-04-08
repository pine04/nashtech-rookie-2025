using Microsoft.AspNetCore.Mvc;
using aspnetcoreapi2.Models;
using aspnetcoreapi2.Services;

namespace aspnetcoreapi2.Controllers;

[ApiController]
[Route("/api/people")]
[Produces("application/json")]
public class PeopleController : ControllerBase
{
    private IPeopleService _peopleService;

    public PeopleController(IPeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    [HttpGet]
    public ActionResult<dynamic> GetMany([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] Gender? gender, [FromQuery] string? birthPlace)
    {
        return _peopleService.GetMany(firstName, lastName, gender, birthPlace);
    }

    [HttpPost]
    public ActionResult<Person> Create([FromBody] CreatePersonDTO createPersonDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Person? person = _peopleService.Create(createPersonDTO);

        if (person == null)
        {
            return StatusCode(500);
        }

        return person;
    }

    [HttpPut("{id}")]
    public ActionResult<Person> Update(int id, [FromBody] UpdatePersonDTO updatePersonDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Person? person = _peopleService.GetById(id);

        if (person == null)
        {
            return NotFound();
        }

        person = _peopleService.Update(id, updatePersonDTO);

        if (person == null)
        {
            return StatusCode(500);
        }

        return person;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Person? person = _peopleService.GetById(id);

        if (person == null)
        {
            return NotFound();
        }

        bool success = _peopleService.Delete(person);

        if (success)
        {
            return NoContent();
        }

        return StatusCode(500);
    }
}



