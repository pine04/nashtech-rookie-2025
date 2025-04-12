using efcore2.DTOs;

namespace efcore2.Services;

public interface IDepartmentsService
{
    public Task<List<DepartmentDTO>> GetAll();

    public Task<DepartmentDTO?> GetById(int id);

    public Task<DepartmentDTO?> Create(CreateDepartmentDTO createDepartmentDTO);

    public Task<DepartmentDTO?> Update(int id, UpdateDepartmentDTO updateDepartmentDTO);

    public Task<bool> Delete(int id);
}