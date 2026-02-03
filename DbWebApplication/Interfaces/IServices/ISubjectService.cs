using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface ISubjectService
{
    Task<List<SubjectModel>> GetAllSubjectsAsync();
    Task<SubjectModel> GetSubjectByIdAsync(int id);
    Task CreateSubjectAsync(SubjectModel subject, int specialtyId);
    Task UpdateSubjectAsync(SubjectModel subject);
    Task DeleteSubjectAsync(int id);
    Task SetSubjectGradeAsync(int studentId, int subjectId, int grade);
}