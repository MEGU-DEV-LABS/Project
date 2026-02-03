using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IRepositories;

public interface ISubjectRepository
{
    Task<List<SubjectModel>> GetAllAsync();
    Task<SubjectModel?> GetByIdAsync(int id);
    Task CreateAsync(SubjectModel specialty, int specialtyId);
    Task UpdateAsync(SubjectModel specialty);
    Task DeleteAsync(int id);
    Task SetSubjectGrade(int studentId, int subjectId, int grade);
}