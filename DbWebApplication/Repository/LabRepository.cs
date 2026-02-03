using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class LabRepository(AppDbContext context) : ILabRepository
{
    public async Task<LabModel> GetLabByIdAsync(int labId)
    {
        var lab = await context.LabWorks
            .Include(l => l.Subject)
            .Include(l => l.LabWorkGrades)
            .FirstOrDefaultAsync(l => l.LabWorkID == labId);

        return lab;
    }

    public async Task CreateLab(LabModel model)
    {
        context.LabWorks.Add(model);
        await context.SaveChangesAsync();
    }

    public async Task UpdateLab(LabModel model)
    {
        context.LabWorks
            .Where(l => l.LabWorkID == model.LabWorkID)
            .ExecuteUpdateAsync(l => l
                .SetProperty(p => p.LabWorkName, model.LabWorkName)
                .SetProperty(p => p.SubjectID, model.SubjectID)
            );
    }

    public async Task DeleteLab(int id)
    {
        await context.LabWorks
            .Where(l => l.LabWorkID == id)
            .ExecuteDeleteAsync();
    }
    
    public async Task SetLabGradeAsync(int studentId, int labId, int grade)
    {
        var lab = await context.LabWorks
            .Include(l => l.Subject)
            .FirstOrDefaultAsync(l => l.LabWorkID == labId);

        if (lab == null)
        {
            throw new NullReferenceException("Lab not found");
        }
        
        var labGrade = await context.LabWorkGrade
            .FirstOrDefaultAsync(g => g.StudentID == studentId && g.LabWorkID == labId);

        if (labGrade == null)
        {
            labGrade = new LabWorkGradeModel
            {
                StudentID = studentId,
                LabWorkID = labId,
                GradeValue = grade
            };
            context.LabWorkGrade.Add(labGrade);
        }
        else
        {
            labGrade.GradeValue = grade;
            context.LabWorkGrade.Update(labGrade);
        }

        await context.SaveChangesAsync();

        await UpdateSubjectGrade(studentId, lab.SubjectID);
    }

    private async Task UpdateSubjectGrade(int studentId, int subjectId)
    {
        var labGrades = await context.LabWorkGrade
            .Include(lg => lg.LabWork)
            .Where(lg => lg.StudentID == studentId && lg.LabWork.SubjectID == subjectId)
            .ToListAsync();

        if (!labGrades.Any()) return;

        var grade = labGrades
            .Select(lg => lg.GradeValue)
            .Sum();

        var subjectGrade = await context.SubjectsGrades
            .FirstOrDefaultAsync(sg => sg.StudentId == studentId && sg.SubjectId == subjectId);

        if (subjectGrade == null)
        {
            subjectGrade = new SubjectGrade
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Grade = grade,
                
            };
            context.SubjectsGrades.Add(subjectGrade);
        }
        else
        {
            subjectGrade.Grade = grade;
            context.SubjectsGrades.Update(subjectGrade);
        }

        await context.SaveChangesAsync();
    }
}