using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SubjectRepository(AppDbContext context) : ISubjectRepository
{
    public async Task<List<SubjectModel>> GetAllAsync()
    {
        var subjects = await context.Subjects
            .Include(s => s.StudyPlans)
            .Include(s=> s.SubjectGrades)
            .Include(s => s.LabWorks)
            .ToListAsync();

        return subjects;
    }

    public async Task<SubjectModel?> GetByIdAsync(int id)
    {
        var subject = await context.Subjects
            .Include(s => s.StudyPlans)
            .ThenInclude(s=> s.Specialty)
            .ThenInclude(f=> f.Faculty)
            .Include(s=> s.SubjectGrades)
            .ThenInclude(s => s.Student)
            .Include(s => s.LabWorks)
            .ThenInclude(l => l.LabWorkGrades)
            .ThenInclude(s=>s.Student)
            .FirstOrDefaultAsync(s => s.SubjectID == id);

        return subject;
    }

    public async Task CreateAsync(SubjectModel subject, int specialtyId)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var students = await context.Students
                .Where(s => s.SpecialtyId == specialtyId)
                .ToListAsync();
            
            context.Subjects.Add(subject);
            await context.SaveChangesAsync();

            var labs = new List<LabModel>();

            for (int i = 1; i <= 4; i++)
            {
                labs.Add(new LabModel
                {
                    LabWorkName = $"Лабораторна робота №{i}",
                    SubjectID = subject.SubjectID
                });
            }

            context.LabWorks.AddRange(labs);
            await context.SaveChangesAsync();

            var grades = new List<LabWorkGradeModel>();

            foreach (var lab in labs)
            {
                foreach (var student in students)
                {
                    grades.Add(new LabWorkGradeModel
                    {
                        LabWorkID = lab.LabWorkID,
                        StudentID = student.Id,
                        GradeValue = null 
                    });
                }
            }

            context.LabWorkGrade.AddRange(grades);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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

    public async Task SetSubjectGrade(int studentId, int subjectId, int grade)
    {
        var subjectGrade = await context.SubjectsGrades
            .FirstOrDefaultAsync(sg => sg.StudentId == studentId && sg.SubjectId == subjectId);

        if (subjectGrade == null)
        {
            subjectGrade = new SubjectGrade
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Grade = grade
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