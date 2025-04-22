using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IStudentRepository
{
    Task<List<StudentModel>> GetAllAsync();
    Task<StudentModel?> GetByIdAsync(int id);
    Task CreateAsync(StudentModel student);
    Task UpdateAsync(StudentModel student);
    Task DeleteAsync(int id);
    Task<StudentModel?> GetByAppUserIdAsync(int appUserId);
    //Task SaveChangesAsync();
}