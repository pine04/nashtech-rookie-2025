using System.Text.RegularExpressions;
using aspnetcoreapi1.Models;
using aspnetcoreapi1.Services;
using Microsoft.AspNetCore.Mvc;
using Task = aspnetcoreapi1.Models.Task;

namespace aspnetcoreapi1.Controllers;

[ApiController]
[Route("/api/tasks")]
[Produces("application/json")]
public class TaskController : ControllerBase
{
    private ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<List<Task>> GetAll()
    {
        List<Task> tasks = _taskService.GetAll();

        if (tasks.Count == 0)
        {
            return NotFound();
        }

        return tasks;
    }

    [HttpGet("{id}")]
    public ActionResult<Task> GetById(int id)
    {
        Task task = _taskService.GetById(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public ActionResult<Task> Create(TaskDTO taskDTO)
    {
        Task task = _taskService.Create(taskDTO);

        if (task == null)
        {
            return StatusCode(500);
        }

        return task;
    }

    [HttpPatch("{id}")]
    public ActionResult<Task> Update(int id, TaskUpdateDTO taskDTO)
    {
        Task task = _taskService.GetById(id);

        if (task == null)
        {
            return NotFound();
        }

        task = _taskService.Update(id, taskDTO);

        if (task == null)
        {
            return StatusCode(500);
        }

        return task;
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteById(int id)
    {
        Task task = _taskService.GetById(id);

        if (task == null)
        {
            return NotFound();
        }

        _taskService.Delete(task);

        return NoContent();
    }

    [HttpPost("bulk-create")]
    public ActionResult<List<Task>> BulkCreate(List<TaskDTO> taskDTOs)
    {
        List<Task> tasks = _taskService.BulkCreate(taskDTOs);

        if (tasks == null)
        {
            return StatusCode(500);
        }

        return tasks;
    }

    [HttpPost("bulk-delete")]
    public ActionResult BulkDelete([FromQuery] string ids)
    {
        Regex regex = new Regex(@"^(\d+(,\d+)*)?$");

        if (!regex.IsMatch(ids))
        {
            return BadRequest("'ids' must be a comma-separated string of integers.");
        }

        _taskService.BulkDelete(ids.Split(",").Where(id => !string.IsNullOrWhiteSpace(id)).Select(int.Parse).ToList());

        return NoContent();
    }
}