using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface ISubjectRepository
{
    Task<List<SubjectModel>> GetAllAsync();
    Task<SubjectModel?> GetByIdAsync(int id);
    Task CreateAsync(SubjectModel specialty);
    Task UpdateAsync(SubjectModel specialty);
    Task DeleteAsync(int id);
}