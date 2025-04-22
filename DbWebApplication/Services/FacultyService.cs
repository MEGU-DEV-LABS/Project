using DbWebApplication.Interfaces;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class FacultyService(IFacultyRepository repository)
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
}