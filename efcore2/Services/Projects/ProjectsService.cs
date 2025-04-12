using efcore2.Models;
using efcore2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace efcore2.Services;

public class ProjectsService : IProjectsService
{
    private CompanyContext _companyContext;

    public ProjectsService(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<List<ProjectDTO>> GetAll()
    {
        List<Project> projects = await _companyContext.Projects.Include(p => p.ProjectEmployees).ThenInclude(pe => pe.Employee).ThenInclude(e => e.Department).ToListAsync();
        return projects.Select(project => project.ToProjectDTO()).ToList();
    }

    public async Task<ProjectDTO?> GetById(int id)
    {
        Project? project = await _companyContext.Projects.Include(p => p.ProjectEmployees).ThenInclude(pe => pe.Employee).ThenInclude(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
        return project?.ToProjectDTO();
    }

    public async Task<ProjectDTO?> Create(CreateProjectDTO createProjectDTO)
    {
        Project project = new Project()
        {
            Name = createProjectDTO.Name
        };
        _companyContext.Projects.Add(project);

        int insertedRecords = await _companyContext.SaveChangesAsync();
        if (insertedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(project).Collection(p => p.ProjectEmployees).Load();

        return project.ToProjectDTO();
    }

    public async Task<ProjectDTO?> Update(int id, UpdateProjectDTO updateProjectDTO)
    {
        Project? project = await _companyContext.Projects.Include(p => p.ProjectEmployees).ThenInclude(pe => pe.Employee).ThenInclude(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

        if (project == null)
        {
            return null;
        }

        project.Name = updateProjectDTO.Name;

        int updatedRecords = await _companyContext.SaveChangesAsync();
        if (updatedRecords != 1)
        {
            return null;
        }

        return project.ToProjectDTO();
    }

    public async Task<bool> Delete(int id)
    {
        Project? project = await _companyContext.Projects.FindAsync(id);

        if (project == null)
        {
            return false;
        }

        _companyContext.Projects.Remove(project);

        int deletedRecords = await _companyContext.SaveChangesAsync();
        if (deletedRecords != 1)
        {
            return false;
        }

        return true;
    }
}