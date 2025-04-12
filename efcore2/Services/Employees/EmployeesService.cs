using efcore2.Models;
using efcore2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace efcore2.Services;

public class EmployeesService : IEmployeesService
{
    private CompanyContext _companyContext;

    public EmployeesService(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<List<EmployeeDTO>> GetAll()
    {
        // List<Employee> employees = await _companyContext.Employees.Include(e => e.Department).Include(e => e.Salary).ToListAsync();

        // Using LINQ Inner Join as per the requirement. Why would anyone do this tho?
        var employees = from employee in _companyContext.Employees
                        join department in _companyContext.Departments on employee.DepartmentId equals department.Id
                        join salary in _companyContext.Salaries on employee.Id equals salary.Id into salaryGrouping
                        from s in salaryGrouping.DefaultIfEmpty()
                        select new Employee() { Id = employee.Id, Name = employee.Name, Department = department, JoinedDate = employee.JoinedDate, Salary = s };

        return await employees.Select(employee => employee.ToEmployeeDTO()).ToListAsync();
    }

    public async Task<List<EmployeeWithProjectsDTO>> GetAllWithProjects()
    {
        // sorry you have to read this :)
        var employeesWithProjects = from employee in _companyContext.Employees
                                    join department in _companyContext.Departments on employee.DepartmentId equals department.Id
                                    join salary in _companyContext.Salaries on employee.Id equals salary.Id into sGroup
                                    from s in sGroup.DefaultIfEmpty()
                                    join projectEmployee in _companyContext.ProjectEmployees on employee.Id equals projectEmployee.EmployeeId into peGroup
                                    from pe in peGroup.DefaultIfEmpty()
                                    join project in _companyContext.Projects on pe.ProjectId equals project.Id into pGroup
                                    from p in pGroup.DefaultIfEmpty()
                                    group p by new
                                    {
                                        EmployeeId = employee.Id,
                                        EmployeeName = employee.Name,
                                        DepartmentId = department.Id,
                                        DepartmentName = department.Name,
                                        EmployeeJoinedDate = employee.JoinedDate,
                                        EmployeeSalary = s == null ? null : (double?)s.Amount
                                    } into g
                                    select new
                                    {
                                        Employee = new Employee()
                                        {
                                            Id = g.Key.EmployeeId,
                                            Name = g.Key.EmployeeName,
                                            Department = new Department()
                                            {
                                                Id = g.Key.DepartmentId,
                                                Name = g.Key.DepartmentName
                                            },
                                            JoinedDate = g.Key.EmployeeJoinedDate,
                                            Salary = g.Key.EmployeeSalary != null ? new Salary()
                                            {
                                                Amount = (double)g.Key.EmployeeSalary
                                            } : null
                                        },
                                        Projects = g.Where(p => p != null).ToList()
                                    };

        List<EmployeeWithProjectsDTO> result = new List<EmployeeWithProjectsDTO>();

        foreach (var e in await employeesWithProjects.ToListAsync())
        {
            EmployeeWithProjectsDTO dto = new EmployeeWithProjectsDTO()
            {
                Id = e.Employee.Id,
                Name = e.Employee.Name,
                Department = e.Employee.Department.Name,
                JoinedDate = e.Employee.JoinedDate,
                Salary = e.Employee.Salary?.Amount,
                Projects = e.Projects.Select(p => new ProjectWithoutEmployeesDTO() { Id = p.Id, Name = p.Name }).ToList()
            };
            result.Add(dto);
        }

        return result;
    }

    public async Task<List<EmployeeDTO>> GetRecent()
    {
        var employees = from employee in _companyContext.Employees
                        join department in _companyContext.Departments on employee.DepartmentId equals department.Id
                        join salary in _companyContext.Salaries on employee.Id equals salary.Id into salaryGrouping
                        from s in salaryGrouping.DefaultIfEmpty()
                        where s != null && s.Amount > 100.0 && employee.JoinedDate >= new DateOnly(2024, 1, 1)
                        select new Employee() { Id = employee.Id, Name = employee.Name, Department = department, JoinedDate = employee.JoinedDate, Salary = s };

        return await employees.Select(employee => employee.ToEmployeeDTO()).ToListAsync();
    }

    public async Task<EmployeeDTO?> GetById(int id)
    {
        Employee? employee = await _companyContext.Employees.Include(e => e.Department).Include(e => e.Salary).FirstOrDefaultAsync(e => e.Id == id);
        return employee?.ToEmployeeDTO();
    }

    public async Task<EmployeeDTO?> Create(CreateEmployeeDTO createEmployeeDTO)
    {
        Employee employee = new Employee()
        {
            Name = createEmployeeDTO.Name,
            DepartmentId = createEmployeeDTO.DepartmentId,
            JoinedDate = createEmployeeDTO.JoinedDate
        };
        _companyContext.Employees.Add(employee);

        int insertedRecords = await _companyContext.SaveChangesAsync();
        if (insertedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(employee).Reference(e => e.Department).Load();
        _companyContext.Entry(employee).Reference(e => e.Salary).Load();

        return employee.ToEmployeeDTO();
    }

    public async Task<EmployeeDTO?> Update(int id, UpdateEmployeeDTO updateEmployeeDTO)
    {
        Employee? employee = await _companyContext.Employees.FindAsync(id);

        if (employee == null)
        {
            return null;
        }

        employee.Name = updateEmployeeDTO.Name;
        employee.DepartmentId = updateEmployeeDTO.DepartmentId;
        employee.JoinedDate = updateEmployeeDTO.JoinedDate;

        int updatedRecords = await _companyContext.SaveChangesAsync();
        if (updatedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(employee).Reference(e => e.Department).Load();
        _companyContext.Entry(employee).Reference(e => e.Salary).Load();

        return employee.ToEmployeeDTO();
    }

    public async Task<bool> Delete(int id)
    {
        Employee? employee = await _companyContext.Employees.FindAsync(id);

        if (employee == null)
        {
            return false;
        }

        _companyContext.Employees.Remove(employee);

        int deletedRecords = await _companyContext.SaveChangesAsync();
        if (deletedRecords != 1)
        {
            return false;
        }

        return true;
    }
}