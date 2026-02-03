using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface IFacultyService
{
    Task<List<FacultyModel>> GetAllFaculties();
    Task<FacultyModel> GetFacultyById(int id);
    Task<FacultyModel> GetFacultyBySpecialty(int id);
    Task CreateFaculty(FacultyModel faculty);
    Task UpdateFaculty(FacultyModel faculty);
    Task DeleteFaculty(int id);
    
}