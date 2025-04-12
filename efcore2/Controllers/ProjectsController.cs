using efcore2.Services;
using efcore2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace efcore2.Controllers;

[ApiController]
[Route("/api/projects")]
[Produces("application/json")]
public class ProjectsController : ControllerBase
{
    private IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDTO>>> GetAll()
    {
        return await _projectsService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetById(int id)
    {
        ProjectDTO? projectDTO = await _projectsService.GetById(id);

        if (projectDTO == null)
        {
            return NotFound();
        }

        return projectDTO;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> Create([FromBody] CreateProjectDTO createProjectDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        ProjectDTO? projectDTO = await _projectsService.Create(createProjectDTO);

        if (projectDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return projectDTO;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDTO>> Update(int id, [FromBody] UpdateProjectDTO updateProjectDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        ProjectDTO? projectDTO = await _projectsService.GetById(id);

        if (projectDTO == null)
        {
            return NotFound();
        }

        projectDTO = await _projectsService.Update(id, updateProjectDTO);

        if (projectDTO == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return projectDTO;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        ProjectDTO? projectDTO = await _projectsService.GetById(id);

        if (projectDTO == null)
        {
            return NotFound();
        }

        bool success = await _projectsService.Delete(id);

        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}