using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class SpecialtyService(ISpecialtyRepository repository) : ISpecialtyService
{
    public async Task<List<SpecialtyModel>> GetSpecialties(int facultyId)
    {
        var specialties = await repository.GetAllAsync( facultyId);
        return specialties;
    }

    public async Task<SpecialtyModel> GetSpecialtyById(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        
        if (specialty == null)
        {
            throw new NullReferenceException($"Specialty with ID {id} not found.");
        }

        return specialty;
    }
    
    public async Task AddSpecialty(SpecialtyModel specialty)
    {
        await repository.Create(specialty);
    }

    public async Task UpdateSpecialty(SpecialtyModel specialty)
    {
        await repository.Update(specialty);
    }
    
    public async Task DeleteSpecialty(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        
        if (specialty == null)
        {
            throw new NullReferenceException($"Specialty with ID {id} not found.");
        }

        await repository.Delete(specialty.Id);
    }
    
    public async Task AddSubjectsToStudyPlan(int specialtyId, StudyPlan studyPlan)
    {
        await repository.AddSubjectsToStudyPlan(specialtyId, studyPlan);
    }
    
    public async Task AddStudents(List<StudentModel> students, int specialtyId)
    {
        await repository.AddStudents(students, specialtyId);
    }
    
    public async Task<List<StudentModel>> GetStudentsBySpecialtyId(int specialtyId)
    {
        var students = await repository.GetStudentsBySpecialty(specialtyId);
        return students;
    }
}