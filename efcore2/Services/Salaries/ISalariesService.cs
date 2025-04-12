using efcore2.DTOs;

namespace efcore2.Services;

public interface ISalariesService
{
    public Task<List<SalaryDTO>> GetAll();

    public Task<SalaryDTO?> GetById(int id);

    public Task<SalaryDTO?> Create(CreateSalaryDTO createSalaryDTO);

    public Task<SalaryDTO?> Update(int id, UpdateSalaryDTO updateSalaryDTO);

    public Task<bool> Delete(int id);
}