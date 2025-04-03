using aspnetcoreapi1.Models;
using aspnetcoreapi1.Repositories;
using Task = aspnetcoreapi1.Models.Task;

namespace aspnetcoreapi1.Services;

public class TaskService : ITaskService
{
    private ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<Task> GetAll()
    {
        return _taskRepository.GetAll();
    }

    public Task GetById(int id)
    {
        return _taskRepository.GetById(id);
    }

    public Task Create(TaskDTO task)
    {
        return _taskRepository.Create(task);
    }

    public Task Update(int id, TaskUpdateDTO taskDTO)
    {
        return _taskRepository.Update(id, taskDTO);
    }

    public void Delete(Task task)
    {
        _taskRepository.Delete(task);
    }

    public List<Task> BulkCreate(List<TaskDTO> taskDTOs)
    {
        return _taskRepository.BulkCreate(taskDTOs);
    }

    public void BulkDelete(List<int> ids)
    {
        _taskRepository.BulkDelete(ids);
    }
}