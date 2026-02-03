using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class FacultyService(IFacultyRepository repository) : IFacultyService
{
    public async Task<List<FacultyModel>> GetAllFaculties()
    {
        var faculties = await repository.GetAllAsync();       
        return faculties;
    }
    
    public async Task<FacultyModel> GetFacultyById(int id)
    {
        var faculty = await repository.GetByIdAsync(id);
        
        if (faculty == null)
        {
            throw new NullReferenceException($"Faculty with ID {id} not found.");
        }
        
        return faculty;
    }

    public async Task<FacultyModel> GetFacultyBySpecialty(int id)
    {
        return await repository.GetFacultyBySpecialty(id);
    }
    
    public async Task CreateFaculty(FacultyModel faculty)
    {
        if (faculty == null)
        {
            throw new ArgumentNullException(nameof(faculty), "Faculty cannot be null.");
        }
        
        await repository.CreateAsync(faculty);
    }
    
    public async Task UpdateFaculty(FacultyModel faculty)
    {
        if (faculty == null)
        {
            throw new ArgumentNullException(nameof(faculty), "Faculty cannot be null.");
        }
        
        var existingFaculty = await repository.GetByIdAsync(faculty.Id);
        
        if (existingFaculty == null)
        {
            throw new NullReferenceException($"Faculty with ID {faculty.Id} not found.");
        }
        
        await repository.UpdateAsync(faculty);
    }
    
    public async Task DeleteFaculty(int id)
    {
        var faculty = await repository.GetByIdAsync(id);
        
        if (faculty == null)
        {
            throw new NullReferenceException($"Faculty with ID {id} not found.");
        }
        
        await repository.DeleteAsync(id);
    }
}