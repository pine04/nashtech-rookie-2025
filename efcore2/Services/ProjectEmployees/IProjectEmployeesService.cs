using efcore2.DTOs;

namespace efcore2.Services;

public interface IProjectEmployeesService
{
    public Task<List<ProjectEmployeeDTO>> GetAllProjectEmployees(int projectId);

    public Task<ProjectEmployeeDTO?> GetProjectEmployeeById(int projectId, int employeeId);

    public Task<ProjectEmployeeDTO?> CreateProjectEmployee(int projectId, CreateProjectEmployeeDTO createProjectEmployeeDTO);

    public Task<ProjectEmployeeDTO?> UpdateProjectEmployee(int projectId, int employeeId, UpdateProjectEmployeeDTO updateProjectEmployeeDTO);

    public Task<bool> DeleteProjectEmployee(int projectId, int employeeId);
}