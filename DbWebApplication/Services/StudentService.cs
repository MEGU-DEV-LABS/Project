using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Services;

public class StudentService(IStudentRepository studentRepository, ISubjectRepository subjectRepository, IUserRepository userRepository)
{
    
    public async Task AddStudentToSubjectAsync(int studentId, int subjectId)
    {
        var student = await studentRepository.GetByIdAsync(studentId);

        var subject = await subjectRepository.GetByIdAsync(subjectId);

        if (student == null || subject == null)
        {
            throw new Exception("Student or Subject not found.");
        }

        if (!student.Subjects.Any(s => s.SubjectID == subjectId))
        {
            student.Subjects.Add(subject);
            await studentRepository.UpdateAsync(student);
        }
    }

    public List<string> GetSubjectsNames()
    {
        return subjectRepository.GetAllAsync().Result.Select(s => s.SubjectName).ToList();
    }

    public async Task<StudentModel> GetStudentByUserId(int applicationUserId)
    {
        var student = studentRepository.GetByAppUserIdAsync(applicationUserId)
            
        return await student;
    }
    
    //TODO: ПЕРЕРОБИТИ ОЦІНКИ ЗА ВСІ ПРЕДМЕТИ
    /*public async Task<StudentGradesViewModel> PrepareStudentGradesViewModelAsync(int userId)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .ThenInclude(sub => sub.LabWorks)
            .ThenInclude(lab => lab.LabWorkGrades)
            .FirstOrDefaultAsync(s => s.AppUserId == userId);

        var subjects = student.Subjects.Select(sub => new SubjectOverallViewModel
        {
            Id = sub.SubjectID,
            SubjectName = sub.SubjectName,
            OverallGrade = (int)sub.LabWorks
                .Select(lab => lab.LabWorkGrades
                    .FirstOrDefault(lg => lg.StudentID == student.Id)?.GradeValue ?? 0)
                .Sum(),
            Picture = Convert.ToBase64String(sub.ImageData)
        }).ToList();

        var studentGradesViewModel = new StudentGradesViewModel
        {
            StudentFullName = $"{student.LastName} {student.FirstName} {student.FatherName}",
            Faculty = student.SpecialtyModel.GetDisplayName(),
            Subjects = subjects
        };

        return studentGradesViewModel;
    }*/

    
    //TODO: ПЕРЕРОБИТИ ОЦІНКИ ЗА ПРЕДМЕТ
    /*public async Task<SubjectDetailsViewModel> PrepareSubjectGradesViewModelAsync(int subjectId, string userId)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .ThenInclude(sub => sub.LabWorks)
            .ThenInclude(lab => lab.LabWorkGrades)
            //.FirstOrDefaultAsync(s => s.AppUserId == userId);

        var subjectSearch = student.Subjects.FirstOrDefault(s => s.SubjectID == subjectId);

        var labs = subjectSearch.LabWorks.Select(lab => new LabaratoryWorkViewModel
        {
            LabWorkName = lab.LabWorkName,
            LabWorkGrade = (int)(lab.LabWorkGrades.FirstOrDefault()?.GradeValue ?? 0)
        }).ToList();

        var subjectDetailsViewModel = new SubjectDetailsViewModel
        {
            SubjectName = subjectSearch.SubjectName,
            Picture = Convert.ToBase64String(subjectSearch.ImageData),
            OverallGrade = (int)subjectSearch.LabWorks
                .Select(lab => lab.LabWorkGrades
                    .FirstOrDefault(lg => lg.StudentID == student.Id)?.GradeValue ?? 0)
                .Sum(),
            LabaratoryWorks = labs
        };

        return subjectDetailsViewModel;
    }*/

    
    //TODO: ЗРОБИТИ
    /*public async Task<List<SessionSubjectViewModel>> PrepareSessionSubjectViewModelAsync(StudentModel student)
    {
        if (student == null)
        {
            return new List<SessionSubjectViewModel>();
        }

        var sessionGrades = await context.SessionGrades
            .Where(sg => sg.StudentId == student.Id)
            .Include(sg => sg.SessionSubjects)
            .ToListAsync();

        var sessionSubjectViewModels = sessionGrades.Select(s => new SessionSubjectViewModel
        {
            SessionSubject = s.SessionSubjects.Subject,
            Grade = (int)s.Grade,
        }).ToList();

        return sessionSubjectViewModels;
    }*/

    public async Task<ListSubcestsAndIDViewModel> GetSubjectsWithIdAsync()
    {
        var subjects = await context.Subjects.Select(s => new DetermineSessionSubjectsViewModel()
        {
            Id = s.SubjectID,
            Name = s.SubjectName,
            IsSelected = false
        }).ToListAsync();

        var response = new ListSubcestsAndIDViewModel()
        {
            Subjects = subjects
        };
        return response;
    }

    public async Task AddSubjectToSessionAsync(ListSubcestsAndIDViewModel model)
    {
        var existingSessionSubjects = await context.SessionSubjects
            .Where(s => s.SpecialtyModel == model.Faculty)
            .ToListAsync();

        var newSubjects = model.Subjects
            .Where(s => s.IsSelected && !existingSessionSubjects.Any(es => es.Subject == s.Name))
            .ToList();

        if (newSubjects.Any())
        {
            var sessionSubjects = newSubjects.Select(s => new SessionSubjects
            {
                SpecialtyModel = model.Faculty,
                Subject = s.Name
            }).ToList();

            await context.SessionSubjects.AddRangeAsync(sessionSubjects);
            await context.SaveChangesAsync();

            existingSessionSubjects.AddRange(sessionSubjects);
        }

        var students = await context.Students
            .Where(s => s.SpecialtyModel == model.Faculty)
            .ToListAsync();

        var existingGrades = await context.SessionGrades
            .Where(sg => students.Select(st => st.Id).Contains(sg.StudentId))
            .ToListAsync();

        var connections = new List<SessionGrades>();

        foreach (var student in students)
        {
            foreach (var sessionSubject in existingSessionSubjects)
            {
                if (!existingGrades.Any(eg => eg.StudentId == student.Id && eg.SessionId == sessionSubject.Id))
                {
                    connections.Add(new SessionGrades
                    {
                        StudentId = student.Id,
                        SessionId = sessionSubject.Id
                    });
                }
            }
        }

        if (connections.Any())
        {
            await context.SessionGrades.AddRangeAsync(connections);
            await context.SaveChangesAsync();
        }
    }

    public async Task<StudentModel> GetStudentByIdAsync(int studentId)
    {
        var student = await studentRepository.GetByIdAsync(studentId);
        if (student == null)
        {
            throw new NullReferenceException("Student not found.");
        }
        return  student;
    }
    
    
    /*public async Task UpdateSessionGradeAsync(SessionGrades grade, int newGrade)
    {
        grade.Grade = newGrade;
        context.SessionGrades.Update(grade);
        await context.SaveChangesAsync();
    }*/
    
    /*public async Task<SessionSubjects> GetSessionSubjectAsync(string subjectName, SpecialtyModel specialty)
    {
        return await context.Specialties
                .Include(s => s.SessionSubjects)
                .Where(s => s.SessionSubjects.Any(ss => ss.SubjectName == subjectName))
                .SelectMany(s => s.SessionSubjects)
                .FirstOrDefaultAsync(ss => ss.SubjectName == subjectName);
    }*/
}