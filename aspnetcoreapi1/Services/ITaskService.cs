using aspnetcoreapi1.Models;
using Task = aspnetcoreapi1.Models.Task;

namespace aspnetcoreapi1.Services;

public interface ITaskService
{
    public List<Task> GetAll();

    public Task GetById(int id);

    public Task Create(TaskDTO task);

    public Task Update(int id, TaskUpdateDTO taskDTO);

    public void Delete(Task task);

    public List<Task> BulkCreate(List<TaskDTO> taskDTOs);

    public void BulkDelete(List<int> ids);
}