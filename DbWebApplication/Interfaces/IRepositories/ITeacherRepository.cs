using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IRepositories;

public interface ITeacherRepository
{
    Task<List<TeacherModel>> GetAllAsync();
    Task<TeacherModel?> GetByIdAsync(int id);
    Task<TeacherModel?> GetByAppUserIdAsync(int appUserId);
    Task CreateAsync(TeacherModel teacher);
    Task UpdateAsync(TeacherModel teacher);
    Task DeleteAsync(int id);
    Task<List<SubjectModel>> GetTeacherSubjects(int teacherId);
    Task<List<SessionSubjects>> GetTeacherSessionSubjects(int teacherId);
}