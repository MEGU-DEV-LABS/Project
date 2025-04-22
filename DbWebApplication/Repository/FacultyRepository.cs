using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class FacultyRepository(AppDbContext context) : IFacultyRepository
{
    public async Task<List<FacultyModel>> GetAllAsync()
    {
        var faculties = await context.Faculties
            .Include(f => f.Specialties)
            .ToListAsync();
        
        return faculties;
    }

    public async Task<FacultyModel?> GetByIdAsync(int id)
    {
        var faculty = await context.Faculties
            .Include(f => f.Specialties)
            .FirstOrDefaultAsync(f => f.Id == id);
        
        return faculty;
    }

    public async Task CreateAsync(FacultyModel faculty)
    {
        await context.Faculties.AddAsync(faculty);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FacultyModel faculty)
    {
        await context.Faculties
            .Where(f => f.Id == faculty.Id)
            .ExecuteUpdateAsync(f => f
                .SetProperty(p => p.Name, faculty.Name)
            );
    }

    public async Task DeleteAsync(int id)
    {
        await context.Faculties
            .Where(f => f.Id == id)
            .ExecuteDeleteAsync();
    }
}