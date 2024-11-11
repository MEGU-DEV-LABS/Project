using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Models;
using DbWebApplication;
using DbWebApplication.Data;
using DbWebApplication.Extensions;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Services;

public class StudentService(AppDbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<T> GetByIdAsync<T>(EnrollStudentViewModel model) where T : class
    {
        if (typeof(T) == typeof(StudentModel))
        {
            return await context.Set<StudentModel>()
                .Where(s => 
                    s.FirstName == model.FirstName && 
                    s.LastName == model.LastName && 
                    s.FatherName == model.FatherName && 
                    s.Faculty == model.Faculty)
                .FirstOrDefaultAsync() as T;
        }
        else if (typeof(T) == typeof(SubjectModel))
        {
            return await context.Set<SubjectModel>()
                .Where(s => s.SubjectName == model.SubjectName)
                .FirstOrDefaultAsync() as T;
        }

        return null;
    }
    
    public async Task AddStudentToSubjectAsync(int studentId, int subjectId)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        var subject = await context.Subjects
            .FirstOrDefaultAsync(s => s.SubjectID == subjectId);

        if (student == null || subject == null)
        {
            throw new Exception("Student or Subject not found.");
        }

        // Додаємо предмет до колекції Subjects студента, якщо ще не додано
        if (!student.Subjects.Contains(subject))
        {
            student.Subjects.Add(subject);
            await context.SaveChangesAsync();
        }
    }

    public List<string> GetSubjectsNames()
    {
        return context.Subjects.Select(s => s.SubjectName).ToList();
    }

    public async Task<byte[]> ConvertImageToByteArrayAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }
        
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray(); 
        }
    }
    
    public StudentModel GetStudentByUserId(string applicationUserId)
    {
        // Шукаємо студента за ApplicationUserId та завантажуємо його предмети (Subjects)
        var student = context.Students
            .Include(s => s.Subjects) // Явно завантажуємо колекцію предметів
            .FirstOrDefault(s => s.ApplicationUserId == applicationUserId);

        return student;
    }

    public async Task AddQrTokenToStudentAsync(StudentModel student)
    {
        Guid token = System.Guid.NewGuid();
        
        student.QrCodeToken = token;
        DateTime now = DateTime.Now;
        DateTime time = now.AddDays(7);
        student.TokenDateExpired = time;
        context.Students.Update(student);
        await context.SaveChangesAsync();
    }

    public async Task<StudentModel> GetStudentByQrToken(string token)
    {
        Guid studentToken = Guid.Parse(token);
        return await context.Students.FirstOrDefaultAsync(s => s.QrCodeToken == studentToken && s.TokenDateExpired >= DateTime.Now);
    }

    public async Task<StudentGradesViewModel> PrepareStudentGradesViewModelAsync(string userId)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .ThenInclude(sub => sub.LabWorks)
            .ThenInclude(lab => lab.LabWorkGrades)
            .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);

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
            Faculty = student.Faculty.GetDisplayName(),
            Subjects = subjects
        };

        return studentGradesViewModel;
    }

    public async Task<SubjectDetailsViewModel> PrepareSubjectGradesViewModelAsync(int subjectId, string userId)
    {
        var student = await context.Students
            .Include(s => s.Subjects)
            .ThenInclude(sub => sub.LabWorks)
            .ThenInclude(lab => lab.LabWorkGrades)
            .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
        
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
    }


}
