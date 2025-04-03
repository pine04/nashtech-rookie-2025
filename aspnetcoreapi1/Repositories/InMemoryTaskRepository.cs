using Task = aspnetcoreapi1.Models.Task;
using Microsoft.Extensions.Caching.Memory;
using aspnetcoreapi1.Models;

namespace aspnetcoreapi1.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private IMemoryCache _cache;
    private static int CurrentId = 1;

    public InMemoryTaskRepository(IMemoryCache cache)
    {
        _cache = cache;

        if (!_cache.TryGetValue("Tasks", out _))
        {
            _cache.Set("Tasks", new List<Task>() {
                new Task(GenerateId(), "Take out the trash", false),
                new Task(GenerateId(), "Clean the house", false),
                new Task(GenerateId(), "Walk the dog", false),
            });
        }
    }

    private static int GenerateId()
    {
        return CurrentId++;
    }

    public List<Task> GetAll()
    {
        return _cache.Get<List<Task>?>("Tasks") ?? new List<Task>();
    }

    public Task GetById(int id)
    {
        List<Task>? tasks = _cache.Get<List<Task>?>("Tasks");

        if (tasks == null)
        {
            return null;
        }

        return tasks.Find(task => task.Id == id);
    }

    public Task Create(TaskDTO task)
    {
        List<Task>? tasks = _cache.Get<List<Task>?>("Tasks");

        if (tasks == null)
        {
            return null;
        }

        Task newTask = new Task(GenerateId(), task.Title, task.IsCompleted);
        tasks.Add(newTask);
        return newTask;
    }

    public Task Update(int id, TaskUpdateDTO taskDTO)
    {
        List<Task>? tasks = _cache.Get<List<Task>?>("Tasks");

        if (tasks == null)
        {
            return null;
        }

        Task? task = tasks.Find(task => task.Id == id);

        if (task == null)
        {
            return null;
        }

        if (taskDTO.Title != null)
        {
            task.Title = taskDTO.Title;
        }
        if (taskDTO.IsCompleted != null)
        {
            task.IsCompleted = (bool)taskDTO.IsCompleted;
        }

        return task;
    }

    public void Delete(Task task)
    {
        _cache.Get<List<Task>>("Tasks")?.Remove(task);
    }

    public List<Task> BulkCreate(List<TaskDTO> taskDTOs)
    {
        List<Task>? tasks = _cache.Get<List<Task>?>("Tasks");

        if (tasks == null)
        {
            return null;
        }

        List<Task> newTasks = taskDTOs.Select(taskDTO => new Task(GenerateId(), taskDTO.Title, taskDTO.IsCompleted)).ToList();
        tasks.AddRange(newTasks);
        return newTasks;
    }

    public void BulkDelete(List<int> ids)
    {
        _cache.Get<List<Task>>("Tasks")?.RemoveAll(task => ids.Contains(task.Id));
    }
}