using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface ISessionRepository
{
    Task<Session> GetByIdAsync(int id);
    Task<List<Session>> GetAllAsync();
    Task CreateAsync(Session session, List<SessionSubjects> subjects);
    Task DeleteAsync(int id);
    Task SetGradeAsync(int sessionId, int studentId, int subjectId, int grade);
}