using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface ISpecialtyRepository
{
    Task<List<SpecialtyModel>> GetAllAsync();
    Task<SpecialtyModel?> GetByIdAsync(int id);
    Task CreateAsync(SpecialtyModel specialty);
    Task UpdateAsync(SpecialtyModel specialty);
    Task DeleteAsync(int id);
}