using efcore2.DTOs;

namespace efcore2.Services;

public interface IProjectsService
{
    public Task<List<ProjectDTO>> GetAll();

    public Task<ProjectDTO?> GetById(int id);

    public Task<ProjectDTO?> Create(CreateProjectDTO createProjectDTO);

    public Task<ProjectDTO?> Update(int id, UpdateProjectDTO updateProjectDTO);

    public Task<bool> Delete(int id);
}