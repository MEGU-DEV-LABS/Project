using DbWebApplication.Interfaces;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class SpecialtyService(ISpecialtyRepository repository)
{
    public async Task<List<SpecialtyModel>> GetSpecialties()
    {
        var specialties = await repository.GetAllAsync();
        return specialties;
    }

    public async Task<SpecialtyModel> GetSpecialtyById(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        
        if (specialty == null)
        {
            throw new NullReferenceException($"Specialty with ID {id} not found.");
        }

        return specialty;
    }
    
    public async Task AddSpecialty(SpecialtyModel specialty)
    {
        await repository.Create(specialty);
    }

    public async Task UpdateSpecialty(SpecialtyModel specialty)
    {
        await repository.Update(specialty);
    }
    
    public async Task DeleteSpecialty(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        
        if (specialty == null)
        {
            throw new NullReferenceException($"Specialty with ID {id} not found.");
        }

        await repository.Delete(specialty.Id);
    }
}