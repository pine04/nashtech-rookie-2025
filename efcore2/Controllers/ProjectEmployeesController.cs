using efcore2.Services;
using efcore2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace efcore2.Controllers;

[ApiController]
[Route("/api/projects/{projectId}/employees")]
[Produces("application/json")]
public class ProjectEmployeesController : ControllerBase
{
    private IProjectEmployeesService _projectEmployeesService;

    public ProjectEmployeesController(IProjectEmployeesService projectEmployeesService)
    {
        _projectEmployeesService = projectEmployeesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectEmployeeDTO>>> GetAllProjectEmployees(int projectId)
    {
        return await _projectEmployeesService.GetAllProjectEmployees(projectId);
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ProjectEmployeeDTO>> GetProjectEmployeeById(int projectId, int employeeId)
    {
        ProjectEmployeeDTO? projectEmployeeDTO = await _projectEmployeesService.GetProjectEmployeeById(projectId, employeeId);

        if (projectEmployeeDTO == null)
        {
            return NotFound();
        }

        return projectEmployeeDTO;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectEmployeeDTO>> CreateProjectEmployee(int projectId, [FromBody] CreateProjectEmployeeDTO createProjectEmployeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        ProjectEmployeeDTO? projectEmployeeDTO = await _projectEmployeesService.CreateProjectEmployee(projectId, createProjectEmployeeDTO);

        if (projectEmployeeDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return projectEmployeeDTO;
    }

    [HttpPut("{employeeId}")]
    public async Task<ActionResult<ProjectEmployeeDTO>> UpdateProjectEmployee(int projectId, int employeeId, [FromBody] UpdateProjectEmployeeDTO updateProjectEmployeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        ProjectEmployeeDTO? projectEmployeeDTO = await _projectEmployeesService.GetProjectEmployeeById(projectId, employeeId);

        if (projectEmployeeDTO == null)
        {
            return NotFound();
        }

        projectEmployeeDTO = await _projectEmployeesService.UpdateProjectEmployee(projectId, employeeId, updateProjectEmployeeDTO);

        if (projectEmployeeDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return projectEmployeeDTO;
    }

    [HttpDelete("{employeeId}")]
    public async Task<ActionResult> DeleteProjectEmployee(int projectId, int employeeId)
    {
        ProjectEmployeeDTO? projectEmployeeDTO = await _projectEmployeesService.GetProjectEmployeeById(projectId, employeeId);

        if (projectEmployeeDTO == null)
        {
            return NotFound();
        }

        bool success = await _projectEmployeesService.DeleteProjectEmployee(projectId, employeeId);

        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}