using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface ISpecialtyService
{
    Task<List<SpecialtyModel>> GetSpecialties(int facultyId);
    Task<SpecialtyModel> GetSpecialtyById(int id);
    Task AddSpecialty(SpecialtyModel specialty);
    Task UpdateSpecialty(SpecialtyModel specialty);
    Task DeleteSpecialty(int id);
    Task AddSubjectsToStudyPlan(int specialtyId, StudyPlan studyPlan);
    Task AddStudents(List<StudentModel> students, int specialtyId);
    Task<List<StudentModel>> GetStudentsBySpecialtyId(int specialtyId);
}