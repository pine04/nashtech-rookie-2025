using efcore2.DTOs;

namespace efcore2.Services;

public interface IEmployeesService
{
    public Task<List<EmployeeDTO>> GetAll();

    public Task<List<EmployeeWithProjectsDTO>> GetAllWithProjects();

    public Task<List<EmployeeDTO>> GetRecent();

    public Task<EmployeeDTO?> GetById(int id);

    public Task<EmployeeDTO?> Create(CreateEmployeeDTO createEmployeeDTO);

    public Task<EmployeeDTO?> Update(int id, UpdateEmployeeDTO updateEmployeeDTO);

    public Task<bool> Delete(int id);
}