using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IRepositories;

public interface ISessionSubjectRepository
{
    Task<SessionSubjects> GetSessionSubjectByIdAsync(int id);
}