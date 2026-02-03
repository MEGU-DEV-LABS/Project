using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IFacultyRepository
{
    Task<List<FacultyModel>> GetAllAsync();
    Task<FacultyModel?> GetByIdAsync(int id);
    Task<FacultyModel> GetFacultyBySpecialty(int id);
    Task CreateAsync(FacultyModel faculty);
    Task UpdateAsync(FacultyModel faculty);
    Task DeleteAsync(int id);
}