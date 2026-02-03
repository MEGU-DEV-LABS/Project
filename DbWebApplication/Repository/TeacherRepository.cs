using DbWebApplication.Data;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class TeacherRepository(AppDbContext context) : ITeacherRepository
{
    public async Task<List<TeacherModel>> GetAllAsync()
    {
        return await context.Teachers
            .Include(t => t.Faculty)
            .Include(t => t.Subjects)
            .Include(t => t.SessionSubjects)
            .ToListAsync();
    }

    public async Task<TeacherModel?> GetByIdAsync(int id)
    {
        return await context.Teachers
            .Include(t => t.Faculty)
            .Include(t => t.Subjects)
            .Include(t => t.SessionSubjects)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TeacherModel?> GetByAppUserIdAsync(int appUserId)
    {
        return await context.Teachers
            .Include(t => t.Faculty)
            .Include(t => t.Subjects)
            .Include(t => t.SessionSubjects)
            .FirstOrDefaultAsync(t => t.AppUserId == appUserId);
    }

    public async Task CreateAsync(TeacherModel teacher)
    {
        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TeacherModel teacher)
    {
        await context.Teachers
            .Where(t => t.Id == teacher.Id)
            .ExecuteUpdateAsync(t => t
                .SetProperty(p => p.FirstName, teacher.FirstName)
                .SetProperty(p => p.LastName, teacher.LastName)
                .SetProperty(p => p.FatherName, teacher.FatherName)
                .SetProperty(p => p.FacultyId, teacher.FacultyId)
            );
    }

    public async Task DeleteAsync(int id)
    {
        await context.Teachers
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    } 
    
    public async Task<List<SubjectModel>> GetTeacherSubjects(int teacherId)
    {
        var subjects = await context.Subjects
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();

        return subjects;
    }
    
    public async Task<List<SessionSubjects>> GetTeacherSessionSubjects(int teacherId)
    {
        var subjects = await context.SessionSubjects
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();

        return subjects;
    }
    
    

}