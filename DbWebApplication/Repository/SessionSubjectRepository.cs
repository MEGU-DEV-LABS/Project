using DbWebApplication.Data;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SessionSubjectRepository(AppDbContext context) : ISessionSubjectRepository
{
    public async Task<SessionSubjects> GetSessionSubjectByIdAsync(int id)
    {
        return await context.SessionSubjects
            .Include(ss => ss.Teacher)
            .ThenInclude(t => t.Faculty)

            .Include(ss => ss.Session)
            .ThenInclude(s => s.Specialty)
            .ThenInclude(sp => sp.Faculty)

            .Include(ss => ss.Session)
            .ThenInclude(s => s.Specialty)
            .ThenInclude(sp => sp.Students)
            .ThenInclude(s=>s.SessionGrades)

            .Include(ss => ss.SessionGrades)
            .ThenInclude(sg => sg.Student)

            .FirstOrDefaultAsync(ss => ss.Id == id);
    }
    
    public async Task GetAllSessionSubjectsAsync()
    {
        // Implementation goes here
    }
    
}