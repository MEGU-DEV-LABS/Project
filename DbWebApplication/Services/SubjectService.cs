using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class SubjectService(ISubjectRepository subjectRepository) : ISubjectService
{
    public async Task<List<SubjectModel>> GetAllSubjectsAsync()
    {
        return await subjectRepository.GetAllAsync();
    }
    
    public async Task<SubjectModel> GetSubjectByIdAsync(int id)
    {
        return await subjectRepository.GetByIdAsync(id);
    }
    
    public async Task CreateSubjectAsync(SubjectModel subject, int specialtyId)
    {
        await subjectRepository.CreateAsync(subject, specialtyId);
    }
    
    public async Task UpdateSubjectAsync(SubjectModel subject)
    {
        await subjectRepository.UpdateAsync(subject);
    }
    
    public async Task DeleteSubjectAsync(int id)
    {
        await subjectRepository.DeleteAsync(id);
    }
    
    public async Task SetSubjectGradeAsync(int studentId, int subjectId, int grade)
    {
        await subjectRepository.SetSubjectGrade(studentId, subjectId, grade);
    }
}