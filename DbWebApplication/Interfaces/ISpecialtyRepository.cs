using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface ISpecialtyRepository
{
    Task<List<SpecialtyModel>> GetAllAsync();
    Task<SpecialtyModel?> GetByIdAsync(int id);
    Task Create(SpecialtyModel specialty);
    Task Update(SpecialtyModel specialty);
    Task Delete(int id);
}