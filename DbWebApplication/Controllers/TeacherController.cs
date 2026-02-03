using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Extensions;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZXing;

namespace DbWebApplication.Controllers;

[AuthorizeByRole(Role.Teacher, Role.Admin)]
[Route("teacher")]
public class TeacherController(ISubjectService subjectService, ISessionSubjectRepository sessionSubjectRepository,
    ITeacherRepository teacherRepository, ISessionService sessionService,
    ILabService labService) : Controller
{
    private int GetUserId()
    {
        if (HttpContext.Items["userId"] is int userId)
        {
            return userId;
        }

        throw new UnauthorizedAccessException("UserId not found in the context.");
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        var teacher = await teacherRepository.GetByAppUserIdAsync(userId);
        @ViewBag.teacherId = teacher.Id;
        return View();
    }
    
    [HttpGet("subjects")]
    public async Task<IActionResult> Subjects(int teacherId)
    {
        var subjects = await teacherRepository.GetTeacherSubjects(teacherId);
        return View(subjects);
    }
    
    [HttpGet("vidomists")]
    public async Task<IActionResult> Vidomists(int teacherId) 
    {
        var sessionSubjects = await teacherRepository.GetTeacherSessionSubjects(teacherId);
        return View(sessionSubjects);
    }
    
    [HttpGet("subject")]
    public async Task<IActionResult> Subject(int teacherId, int subjectId)
    {
        var subject = await subjectService.GetSubjectByIdAsync(subjectId);

        var teacher = await teacherRepository.GetByIdAsync(subject.TeacherId.GetValueOrDefault());
        subject.Teacher = teacher;
        
        var model = new SubjectLabsViewModel
        {
            Subject = subject,
            Labs = subject.LabWorks.Select(lab => new LabViewModel
            {
                Lab = lab,
                Students = lab.LabWorkGrades.Select(g => new StudentLabGradeViewModel
                {
                    StudentId =g.Student.Id,
                    FirstName = g.Student.FirstName,
                    LastName = g.Student.LastName,
                    FatherName = g.Student.FatherName,
                    GradeValue = g.GradeValue
                }).ToList()
            }).ToList()
        };

        
        return View(model);
    }
   
    [HttpPost("subject")]
    public async Task<IActionResult> Subject(SubjectLabsViewModel model)
    {
        foreach (var lab in model.Labs)
        {
            int labId = lab.Lab.LabWorkID;

            foreach (var student in lab.Students)
            {
                if (student.GradeValue.HasValue)
                {
                    await labService.SetLabGradeAsync(student.StudentId, labId, student.GradeValue.Value);
                }
            }
        }

        return RedirectToAction("Index");
    }

    [HttpGet("vidomist")]
    public async Task<IActionResult> Vidomist(int teacherId, int subjectId)
    {
        var subject = await sessionSubjectRepository.GetSessionSubjectByIdAsync(subjectId);
        
        var vmStudents = subject.Session.Specialty.Students
            .Select(s => new VidomistStudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FatherName = s.FatherName,
                Grade = subject.SessionGrades
                    .FirstOrDefault(sg => sg.StudentId == s.Id)?.Grade
            }).ToList();
        
        var vm = new VidomistTypeViewModel
        {
            Subject = subject,
            Teacher = subject.Teacher,
            Faculty = subject.Teacher.Faculty,
            Specialty = subject.Session.Specialty,
            Students = vmStudents,
            Session = subject.Session
        };
        
        return View(vm);
    }

    [HttpPost("vidomist")]
    public async Task<IActionResult> Vidomist(VidomistSubmitViewModel model)
    {
        foreach (var studentGrade in model.StudentGrades)
        {
            await sessionService.SetGradeAsync(model.SessionId,studentGrade.StudentId,
                model.SessionSubjectId, studentGrade.Grade);
        }
        
        return RedirectToAction("Vidomists", "Teacher");
    }
}