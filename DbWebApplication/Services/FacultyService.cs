using DbWebApplication.Interfaces;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class FacultyService(IFacultyRepository repository)
{
    public async Task<List<FacultyModel>> GetAllFacultiesAsync()
    {
        var faculties = await repository.GetAllAsync();       
        return faculties;
    }
}