using DbWebApplication.Dto;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class SessionService(ISessionRepository repository) : ISessionService
{
    public async Task AddSubjectsToSession(Session session, List<SessionSubjects> sessionSubjects)
    {
        await repository.CreateAsync(session, sessionSubjects);
    }
    
    public async Task<List<Session>> GetAllSessionsAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Session> GetSessionByIdAsync(int sessionId)
    {
        return await repository.GetByIdAsync(sessionId);
    }

    public async Task DeleteSessionAsync(int sessionId)
    {
        await repository.DeleteAsync(sessionId);
    }
    
    public async Task SetGradeAsync(int sessionId, int studentId, int subjectId, int grade)
    {
        await repository.SetGradeAsync(sessionId, studentId, subjectId, grade);
    }

    public async Task<bool> SessionLastNew(int specialtyId)
    {
       return await repository.SessionLastNew(specialtyId);
    }
}