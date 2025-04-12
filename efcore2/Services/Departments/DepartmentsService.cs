using efcore2.Models;
using efcore2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace efcore2.Services;

public class DepartmentsService : IDepartmentsService
{
    private CompanyContext _companyContext;

    public DepartmentsService(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<List<DepartmentDTO>> GetAll()
    {
        List<Department> departments = await _companyContext.Departments.ToListAsync();
        return departments.Select(department => department.ToDepartmentDTO()).ToList();
    }

    public async Task<DepartmentDTO?> GetById(int id)
    {
        Department? department = await _companyContext.Departments.FindAsync(id);
        return department?.ToDepartmentDTO();
    }

    public async Task<DepartmentDTO?> Create(CreateDepartmentDTO createDepartmentDTO)
    {
        Department department = new Department() { Name = createDepartmentDTO.Name };
        _companyContext.Departments.Add(department);

        int insertedRecords = await _companyContext.SaveChangesAsync();
        if (insertedRecords != 1)
        {
            return null;
        }

        return department.ToDepartmentDTO();
    }

    public async Task<DepartmentDTO?> Update(int id, UpdateDepartmentDTO updateDepartmentDTO)
    {
        Department? department = await _companyContext.Departments.FindAsync(id);

        if (department == null)
        {
            return null;
        }

        department.Name = updateDepartmentDTO.Name;

        int updatedRecords = await _companyContext.SaveChangesAsync();
        if (updatedRecords != 1)
        {
            return null;
        }

        return department.ToDepartmentDTO();
    }

    public async Task<bool> Delete(int id)
    {
        Department? department = await _companyContext.Departments.FindAsync(id);

        if (department == null)
        {
            return false;
        }

        _companyContext.Departments.Remove(department);

        int deletedRecords = await _companyContext.SaveChangesAsync();
        if (deletedRecords != 1)
        {
            return false;
        }

        return true;
    }
}