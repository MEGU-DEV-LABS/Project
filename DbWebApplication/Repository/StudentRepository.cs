using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class StudentRepository(AppDbContext context) : IStudentRepository
{
    public async Task<List<StudentModel>> GetAllAsync()
    {
        var students = await context.Students
            .Include(s => s.Subjects)
            .ToListAsync();

        return students;
    }

    public async Task<StudentModel?> GetByIdAsync(int id)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return student;
    }

    public async Task CreateAsync(StudentModel student)
    {
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StudentModel student)
    {
        await context.Students
            .Where(s => s.Id == student.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.FirstName, student.FirstName)
                .SetProperty(p => p.LastName, student.LastName)
                .SetProperty(p => p.FatherName, student.FatherName)
                .SetProperty(p => p.SpecialtyId, student.SpecialtyId)
            );
    }
    
    public async Task DeleteAsync(int id)
    {
        await context.Students
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
    }
    
    public async Task<StudentModel?> GetByAppUserIdAsync(int appUserId)
    {
        return await context.Students
            .Include(s => s.Specialty)
            .Include(s => s.Subjects)
            .Include(s => s.SessionGrades)
            .FirstOrDefaultAsync(s => s.AppUserId == appUserId);
    }
}