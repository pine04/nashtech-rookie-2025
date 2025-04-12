using efcore2.Models;
using efcore2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace efcore2.Services;

public class SalariesService : ISalariesService
{
    private CompanyContext _companyContext;

    public SalariesService(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<List<SalaryDTO>> GetAll()
    {
        List<Salary> salaries = await _companyContext.Salaries.Include(s => s.Employee).ThenInclude(e => e.Department).ToListAsync();
        return salaries.Select(salary => salary.ToSalaryDTO()).ToList();
    }

    public async Task<SalaryDTO?> GetById(int id)
    {
        Salary? salary = await _companyContext.Salaries.Include(s => s.Employee).ThenInclude(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
        return salary?.ToSalaryDTO();
    }

    public async Task<SalaryDTO?> Create(CreateSalaryDTO createSalaryDTO)
    {
        Salary salary = new Salary()
        {
            EmployeeId = createSalaryDTO.EmployeeId,
            Amount = createSalaryDTO.Amount
        };
        _companyContext.Salaries.Add(salary);

        int insertedRecords = await _companyContext.SaveChangesAsync();
        if (insertedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(salary).Reference(s => s.Employee).Load();
        _companyContext.Entry(salary.Employee).Reference(e => e.Department).Load();

        return salary.ToSalaryDTO();
    }

    public async Task<SalaryDTO?> Update(int id, UpdateSalaryDTO updateSalaryDTO)
    {
        Salary? salary = await _companyContext.Salaries.FindAsync(id);

        if (salary == null)
        {
            return null;
        }

        salary.EmployeeId = updateSalaryDTO.EmployeeId;
        salary.Amount = updateSalaryDTO.Amount;

        int updatedRecords = await _companyContext.SaveChangesAsync();
        if (updatedRecords != 1)
        {
            return null;
        }

        _companyContext.Entry(salary).Reference(s => s.Employee).Load();

        return salary.ToSalaryDTO();
    }

    public async Task<bool> Delete(int id)
    {
        Salary? salary = await _companyContext.Salaries.FindAsync(id);

        if (salary == null)
        {
            return false;
        }

        _companyContext.Salaries.Remove(salary);

        int deletedRecords = await _companyContext.SaveChangesAsync();
        if (deletedRecords != 1)
        {
            return false;
        }

        return true;
    }
}