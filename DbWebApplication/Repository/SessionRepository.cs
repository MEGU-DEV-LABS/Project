using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class SessionRepository(AppDbContext context) : ISessionRepository
{
    public async Task<Session> GetByIdAsync(int id)
    {
        return await context.Sessions
            .Include(s => s.SessionSubjects)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Session>> GetAllAsync()
    {
        return await context.Sessions
            .Include(s => s.SessionSubjects)
            .ToListAsync();
    }

    public async Task CreateAsync(Session session, List<SessionSubjects> subjects)
    {
        var lastSemesterNumber = await context.Sessions
            .Where(s => s.SpecialtyId == session.SpecialtyId)
            .MaxAsync(s => (int?)s.SemesterNumber) ?? 0;

        var studyPlan =  await context.StudyPlans
            .Where(i => i.SpecialtyId == session.SpecialtyId)
            .OrderByDescending(x => x.SemesterNumber)
            .FirstOrDefaultAsync();

        if (studyPlan.SemesterNumber == lastSemesterNumber + 1)
        {
            session.SemesterNumber = lastSemesterNumber + 1;

            await context.Sessions.AddAsync(session);
            await context.SaveChangesAsync(); 

            var students = await context.Students
                .Where(s => s.SpecialtyId == session.SpecialtyId)
                .ToListAsync();

            foreach (var subject in subjects)
            {
                subject.SessionId = session.Id;

                foreach (var student in students)
                { 
                    subject.SessionGrades.Add(new SessionGrades
                    {
                        StudentId = student.Id,
                        Grade = null 
                    });
                }
            }

            await context.SessionSubjects.AddRangeAsync(subjects);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        await context.Sessions
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task SetGradeAsync(int sessionId, int studentId, int subjectId, int grade)
    {
        var sessionGrade = await context.SessionGrades
            .FirstOrDefaultAsync(sg => sg.SessionId == sessionId
                                       && sg.StudentId == studentId
                                       && sg.SessionSubjectsId == subjectId);

        if (sessionGrade != null)
        {
            sessionGrade.Grade = grade;
            context.SessionGrades.Update(sessionGrade);
            await context.SaveChangesAsync();
        }
        else
        {
            var newGrade = new SessionGrades
            {
                SessionId = sessionId,
                StudentId = studentId,
                SessionSubjectsId = subjectId,
                Grade = grade
            };
            
            await context.SessionGrades.AddAsync(newGrade);
            await context.SaveChangesAsync();
        }
    }
}