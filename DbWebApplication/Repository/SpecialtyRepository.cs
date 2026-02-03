using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SpecialtyRepository(AppDbContext context) : ISpecialtyRepository
{
    public async Task<List<SpecialtyModel>> GetAllAsync(int facultyId)
    {
        var specialties = await context.Specialties
            .Where(s => s.FacultyId == facultyId)
            .Include(s => s.Faculty)
            .Include(s => s.StudyPlans)
            .Include(s => s.Students)
            .Include(s => s.Sessions)
            .Include(s => s.SpecialtyScheduleForWeeks)
            .ToListAsync();
        
        return specialties;
    }

    public async Task<SpecialtyModel?> GetByIdAsync(int id)
    {
        var specialty = await context.Specialties
            .Include(s => s.Faculty)
            .Include(s => s.StudyPlans)
            .Include(s => s.Students)
            .Include(s => s.Sessions)
            .Include(s => s.SpecialtyScheduleForWeeks)
            .FirstOrDefaultAsync(s => s.Id == id);

        return specialty;
    }

    public async Task Create(SpecialtyModel specialty)
    {
        await context.Specialties.AddAsync(specialty);
        await context.SaveChangesAsync();
    }
    
    public async Task Update(SpecialtyModel specialty)
    {
        await context.Specialties
            .Where(s => s.Id == specialty.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, specialty.Name)
                .SetProperty(p => p.FacultyId, specialty.FacultyId)
            );
    }

    public async Task Delete(int id)
    {
        await context.Specialties
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task AddSubjectsToStudyPlan(int specialtyId, StudyPlan studyPlan)
    {
        var specialty = await context.Specialties
            .Include(s => s.StudyPlans)
            .FirstOrDefaultAsync(s => s.Id == specialtyId);

        if (specialty == null) return;

        specialty.StudyPlans.Add(studyPlan);
        await context.SaveChangesAsync();
    }

    public async Task AddStudents(List<StudentModel> students, int specialtyId)
    {
        var studentIds = students.Select(s => s.Id).ToList();
        var dbStudents = await context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync();
        
        var specialty = await context.Specialties
            .Include(s => s.Students)
            .FirstOrDefaultAsync(s => s.Id == specialtyId);

        if (specialty == null) return;

        foreach (var student in dbStudents)
        {
            if (specialty.Students.Any(s => s.Id == student.Id)) continue;
            specialty.Students.Add(student);
        }

        await context.SaveChangesAsync();
    }
    
    public async Task<List<StudentModel>> GetStudentsBySpecialty(int specialtyId)
    {
        return await context.Students
            .Where(s => s.SpecialtyId == specialtyId)
            .ToListAsync();
    }

}