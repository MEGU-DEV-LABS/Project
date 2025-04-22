using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SpecialtyRepository(AppDbContext context) : ISpecialtyRepository
{
    public async Task<List<SpecialtyModel>> GetAllAsync()
    {
        var specialties = await context.Specialties
            .Include(s => s.Faculty)
            .Include(s => s.Subjects)
            .Include(s => s.Students)
            .Include(s => s.SessionSubjects)
            .Include(s => s.SpecialtyScheduleForWeeks)
            .ToListAsync();
        
        return specialties;
    }

    public async Task<SpecialtyModel?> GetByIdAsync(int id)
    {
        var specialty = await context.Specialties
            .Include(s => s.Faculty)
            .Include(s => s.Subjects)
            .Include(s => s.Students)
            .Include(s => s.SessionSubjects)
            .Include(s => s.SpecialtyScheduleForWeeks)
            .FirstOrDefaultAsync(s => s.Id == id);

        return specialty;
    }

    public async Task CreateAsync(SpecialtyModel specialty)
    {
        await context.Specialties.AddAsync(specialty);
        await context.SaveChangesAsync();
    }
    //TODO: Add Collections update
    public async Task UpdateAsync(SpecialtyModel specialty)
    {
        await context.Specialties
            .Where(s => s.Id == specialty.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, specialty.Name)
                .SetProperty(p => p.FacultyId, specialty.FacultyId)
            );
    }

    public async Task DeleteAsync(int id)
    {
        await context.Specialties
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }
}