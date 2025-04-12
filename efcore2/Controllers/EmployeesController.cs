using efcore2.Services;
using efcore2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace efcore2.Controllers;

[ApiController]
[Route("/api/employees")]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private IEmployeesService _employeesService;

    public EmployeesController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllWithProjects([FromQuery] string? option)
    {
        if (!string.IsNullOrEmpty(option) && option.Equals("include-projects"))
        {
            return Ok(await _employeesService.GetAllWithProjects());
        }

        if (!string.IsNullOrEmpty(option) && option.Equals("get-recent"))
        {
            return Ok(await _employeesService.GetRecent());
        }

        return Ok(await _employeesService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetById(int id)
    {
        EmployeeDTO? employeeDTO = await _employeesService.GetById(id);

        if (employeeDTO == null)
        {
            return NotFound();
        }

        return employeeDTO;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeDTO createEmployeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        EmployeeDTO? employeeDTO = await _employeesService.Create(createEmployeeDTO);

        if (employeeDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return employeeDTO;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDTO>> Update(int id, [FromBody] UpdateEmployeeDTO updateEmployeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        EmployeeDTO? employeeDTO = await _employeesService.GetById(id);

        if (employeeDTO == null)
        {
            return NotFound();
        }

        employeeDTO = await _employeesService.Update(id, updateEmployeeDTO);

        if (employeeDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return employeeDTO;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        EmployeeDTO? employeeDTO = await _employeesService.GetById(id);

        if (employeeDTO == null)
        {
            return NotFound();
        }

        bool success = await _employeesService.Delete(id);

        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}