using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class StudyPlanRepository(AppDbContext context):IStudyPlanRepository
{
    public async Task CreateStudyPlanAsync(int specialtyId,
        List<int> subjectIds)
    {
        var subjects = await context.Subjects
            .Where(s => subjectIds.Contains(s.SubjectID))
            .ToListAsync();

        if (!subjects.Any())
            throw new Exception("No valid subjects found.");

        var semester = await GetLastSemester(specialtyId);
        
        var semesterNumber = semester?.SemesterNumber + 1 ?? 1;
        
        var studyPlan = new StudyPlan
        {
            SpecialtyId = specialtyId,
            SemesterNumber = semesterNumber,
            Subjects = subjects 
        };

        context.StudyPlans.Add(studyPlan);
        await context.SaveChangesAsync();
    }
    
    public async Task<StudyPlan> GetLastSemester(int specialtyId)
    {
        var s =  await context.StudyPlans
            .Where(i => i.SpecialtyId == specialtyId)
            .Include(s => s.Subjects)
            .OrderByDescending(x => x.SemesterNumber)
            .FirstOrDefaultAsync();

        if (s == null)
            return null;
        else
            return s;
    }
}