using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SubjectRepository(AppDbContext context) : ISubjectRepository
{
    public async Task<List<SubjectModel>> GetAllAsync()
    {
        var subjects = await context.Subjects
            .Include(s => s.Specialties)
            .Include(s => s.LabWorks)
            .ToListAsync();

        return subjects;
    }

    public async Task<SubjectModel?> GetByIdAsync(int id)
    {
        var subject = await context.Subjects
            .Include(s => s.Specialties)
            .Include(s => s.LabWorks)
            .FirstOrDefaultAsync(s => s.SubjectID == id);

        return subject;
    }

    public async Task CreateAsync(SubjectModel subject)
    {
        context.Subjects.Add(subject);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SubjectModel subject)
    {
        await context.Subjects
            .Where(s => s.SubjectID == subject.SubjectID)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.SubjectName, subject.SubjectName)
                .SetProperty(p => p.ImageData, subject.ImageData)
                .SetProperty(p => p.ImageBase64, subject.ImageBase64)
            );
    }

    public async Task DeleteAsync(int id)
    {
        await context.Subjects
            .Where(s => s.SubjectID == id)
            .ExecuteDeleteAsync();
    }
}