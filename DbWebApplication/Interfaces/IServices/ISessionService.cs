using DbWebApplication.Dto;
using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface ISessionService
{
    Task AddSubjectsToSession(Session session, List<SessionSubjects> sessionSubjects);
    Task<List<Session>> GetAllSessionsAsync();
    Task<Session> GetSessionByIdAsync(int sessionId);
    Task DeleteSessionAsync(int sessionId);
    Task SetGradeAsync(int sessionId, int studentId, int subjectId, int grade);
    Task<bool> SessionLastNew(int specialtyId);
}