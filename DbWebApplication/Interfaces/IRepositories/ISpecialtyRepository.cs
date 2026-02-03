using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IRepositories;

public interface ISpecialtyRepository
{
    Task<List<SpecialtyModel>> GetAllAsync(int facultyId);
    Task<SpecialtyModel?> GetByIdAsync(int id);
    Task Create(SpecialtyModel specialty);
    Task Update(SpecialtyModel specialty);
    Task Delete(int id);
    Task AddSubjectsToStudyPlan(int specialtyId, StudyPlan studyPlan);
    Task AddStudents(List<StudentModel> students, int specialtyId);
    Task<List<StudentModel>> GetStudentsBySpecialty(int specialtyId);
}