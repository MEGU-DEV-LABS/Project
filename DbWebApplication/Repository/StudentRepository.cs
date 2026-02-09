using DbWebApplication.Data;
using DbWebApplication.Dto;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class  StudentRepository(AppDbContext context) : IStudentRepository
{
    public async Task<List<StudentModel>> GetAllAsync()
    {
        var students = await context.Students
            .Include(s => s.Specialty)
            .Include(s => s.SubjectGrades)
            .Include(s => s.SessionGrades)
            .Include(s => s.LabWorkGrades)
            .ToListAsync();

        return students;
    }

    public async Task<StudentModel?> GetByIdAsync(int id)
    {
        var student = await context.Students
            .Include(s => s.Specialty)
            .Include(s => s.SubjectGrades)
            .Include(s => s.SessionGrades)
            .Include(s => s.LabWorkGrades)
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
            .Include(s => s.SubjectGrades)
            .Include(s => s.SessionGrades)
            .Include(s => s.LabWorkGrades)
            .FirstOrDefaultAsync(s => s.AppUserId == appUserId);
    }
    
    public async Task<List<SubjectWithGradesDto>> GetSubjectsWithLabGradesAsync(int studentId)
    {
        var student = await context.Students
            .Include(s => s.SubjectGrades)
            .ThenInclude(sg => sg.Subject)
            .Include(s => s.LabWorkGrades)
            .ThenInclude(lg => lg.LabWork)
            .ThenInclude(lw => lw.Subject)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
            return [];

        var subjectDtos = student.SubjectGrades.Select(subjectGrade => new SubjectWithGradesDto
        {
            SubjectId = subjectGrade.Subject.SubjectID,
            SubjectName = subjectGrade.Subject.SubjectName,
            SubjectGrade = subjectGrade.Grade,
            ImageData = subjectGrade.Subject.ImageData,
            ImageBase64 = subjectGrade.Subject.ImageBase64,
            LabWorks = student.LabWorkGrades
                .Where(lg => lg.LabWork.SubjectID == subjectGrade.Subject.SubjectID)
                .Select(lg => new LabWithGradeDto
                {
                    LabId = lg.LabWork.LabWorkID,
                    LabName = lg.LabWork.LabWorkName,
                    Grade = lg.GradeValue
                }).ToList()
        }).ToList();

        return subjectDtos;
    }
    
    public async Task<List<SessionSubjectWithGradesDto>> GetSessionSubjectsWithGradesAsync(int studentId)
    {
        var student = await context.Students
            .Include(s => s.SessionGrades)
                .ThenInclude(sg => sg.SessionSubject)
                    .ThenInclude(s=>s.Session)
            .Include(s => s.SessionGrades)
                .ThenInclude(sg => sg.SessionSubject)
                    .ThenInclude(t=> t.Teacher)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
            return [];

        var sessionDtos = student.SessionGrades.Select(sessionGrade => new SessionSubjectWithGradesDto
        {
            Id = sessionGrade.SessionSubject.Id,
            SubjectName = sessionGrade.SessionSubject.SubjectName,
            Type = sessionGrade.SessionSubject.Type,
            SessionId = sessionGrade.SessionSubject.SessionId,
            Session = sessionGrade.SessionSubject.Session,
            TeacherId = sessionGrade.SessionSubject.TeacherId,
            Teacher = sessionGrade.SessionSubject.Teacher,
            Credits = sessionGrade.SessionSubject.Credits,
            Hours = sessionGrade.SessionSubject.Hours,
            Points = sessionGrade.Grade
        }).ToList();

        return sessionDtos;
    }
}