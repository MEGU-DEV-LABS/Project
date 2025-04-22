using DbWebApplication.Interfaces;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class SpecialtyService(ISpecialtyRepository repository)
{
    public async Task<List<SpecialtyModel>> GetSpecialtiesAsync()
    {
        var specialties = await repository.GetAllAsync();
        return specialties;
    }
    
    public async Task AddSpecialtyAsync(SpecialtyModel specialty)
    {
        await repository.Create(specialty);
    }
}