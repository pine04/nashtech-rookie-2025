using efcore2.Services;
using efcore2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace efcore2.Controllers;

[ApiController]
[Route("/api/salaries")]
[Produces("application/json")]
public class SalariesController : ControllerBase
{
    private ISalariesService _salariesService;

    public SalariesController(ISalariesService salariesService)
    {
        _salariesService = salariesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SalaryDTO>>> GetAll()
    {
        return await _salariesService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaryDTO>> GetById(int id)
    {
        SalaryDTO? salaryDTO = await _salariesService.GetById(id);

        if (salaryDTO == null)
        {
            return NotFound();
        }

        return salaryDTO;
    }

    [HttpPost]
    public async Task<ActionResult<SalaryDTO>> Create([FromBody] CreateSalaryDTO createSalaryDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        SalaryDTO? salaryDTO = await _salariesService.Create(createSalaryDTO);

        if (salaryDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return salaryDTO;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SalaryDTO>> Update(int id, [FromBody] UpdateSalaryDTO updateSalaryDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        SalaryDTO? salaryDTO = await _salariesService.GetById(id);

        if (salaryDTO == null)
        {
            return NotFound();
        }

        salaryDTO = await _salariesService.Update(id, updateSalaryDTO);

        if (salaryDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return salaryDTO;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        SalaryDTO? salaryDTO = await _salariesService.GetById(id);

        if (salaryDTO == null)
        {
            return NotFound();
        }

        bool success = await _salariesService.Delete(id);

        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}