using efcore2.Models;
using efcore2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace efcore2.Services;

public class ProjectEmployeesService : IProjectEmployeesService
{
    private CompanyContext _companyContext;

    public ProjectEmployeesService(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<List<ProjectEmployeeDTO>> GetAllProjectEmployees(int projectId)
    {
        List<ProjectEmployee> projectEmployees = await _companyContext.ProjectEmployees.Include(pe => pe.Employee).ThenInclude(e => e.Department).Where(pe => pe.ProjectId == projectId).ToListAsync();
        return projectEmployees.Select(projectEmployee => projectEmployee.ToProjectEmployeeDTO()).ToList();
    }

    public async Task<ProjectEmployeeDTO?> GetProjectEmployeeById(int projectId, int employeeId)
    {
        ProjectEmployee? projectEmployee = await _companyContext.ProjectEmployees.Include(pe => pe.Employee).ThenInclude(e => e.Department).FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
        return projectEmployee?.ToProjectEmployeeDTO();
    }

    public async Task<ProjectEmployeeDTO?> CreateProjectEmployee(int projectId, CreateProjectEmployeeDTO createProjectEmployeeDTO)
    {
        ProjectEmployee projectEmployee = new ProjectEmployee()
        {
            ProjectId = projectId,
            EmployeeId = createProjectEmployeeDTO.EmployeeId,
            Enable = createProjectEmployeeDTO.Enable
        };
        _companyContext.ProjectEmployees.Add(projectEmployee);

        int insertedRecords = await _companyContext.SaveChangesAsync();
        if (insertedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(projectEmployee).Reference(pe => pe.Employee).Load();
        _companyContext.Entry(projectEmployee.Employee).Reference(e => e.Department).Load();

        return projectEmployee.ToProjectEmployeeDTO();
    }

    public async Task<ProjectEmployeeDTO?> UpdateProjectEmployee(int projectId, int employeeId, UpdateProjectEmployeeDTO updateProjectEmployeeDTO)
    {
        ProjectEmployee? projectEmployee = await _companyContext.ProjectEmployees.FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);

        if (projectEmployee == null)
        {
            return null;
        }

        projectEmployee.Enable = updateProjectEmployeeDTO.Enable;

        int updatedRecords = await _companyContext.SaveChangesAsync();
        if (updatedRecords != 1)
        {
            return null;
        }

        return projectEmployee.ToProjectEmployeeDTO();
    }

    public async Task<bool> DeleteProjectEmployee(int projectId, int employeeId)
    {
        ProjectEmployee? projectEmployee = await _companyContext.ProjectEmployees.FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);

        if (projectEmployee == null)
        {
            return false;
        }

        _companyContext.ProjectEmployees.Remove(projectEmployee);

        int deletedRecords = await _companyContext.SaveChangesAsync();
        if (deletedRecords != 1)
        {
            return false;
        }

        return true;
    }
}