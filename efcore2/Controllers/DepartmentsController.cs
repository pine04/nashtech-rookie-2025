using efcore2.Services;
using efcore2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace efcore2.Controllers;

[ApiController]
[Route("/api/departments")]
[Produces("application/json")]
public class DepartmentsController : ControllerBase
{
    private IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        _departmentsService = departmentsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> GetAll()
    {
        return await _departmentsService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetById(int id)
    {
        DepartmentDTO? departmentDTO = await _departmentsService.GetById(id);

        if (departmentDTO == null)
        {
            return NotFound();
        }

        return departmentDTO;
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> Create([FromBody] CreateDepartmentDTO createDepartmentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        DepartmentDTO? departmentDTO = await _departmentsService.Create(createDepartmentDTO);

        if (departmentDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return departmentDTO;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DepartmentDTO>> Update(int id, [FromBody] UpdateDepartmentDTO updateDepartmentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        DepartmentDTO? departmentDTO = await _departmentsService.GetById(id);

        if (departmentDTO == null)
        {
            return NotFound();
        }

        departmentDTO = await _departmentsService.Update(id, updateDepartmentDTO);

        if (departmentDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return departmentDTO;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        DepartmentDTO? departmentDTO = await _departmentsService.GetById(id);

        if (departmentDTO == null)
        {
            return NotFound();
        }

        bool success = await _departmentsService.Delete(id);

        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}