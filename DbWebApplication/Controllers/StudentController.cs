using DbWebApplication.Enum;
using DbWebApplication.Extensions;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Controllers;

[Route("student")]
[AuthorizeByRole(Role.Student)]
public class StudentController(
    IStudentService studentService)
    : Controller
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
        return View();
    }

    [HttpGet("subjects")]
    public async Task<IActionResult> StudentSubjects()
    {
        var userId = GetUserId();
        var student = await studentService.GetStudentById(userId);
        var subjects = await studentService.GetSubjectsWithGrades(student.Id);
        
        foreach (var subject in subjects)
        {
            if (subject.ImageData != null)
            {
                subject.ImageBase64 = Convert.ToBase64String(subject.ImageData);
            }
        }
        
        var model = new StudentSubjectsViewModel
        {
            Student = student,
            Subjects = subjects
        };

        return View(model);
    }
    
    [HttpGet("sessionsubjects")]
    public async Task<IActionResult> StudentSessionSubjects()
    {
        var userId = GetUserId();
        var student = await studentService.GetStudentById(userId);
        var subjects = await studentService.GetSessionSubjectsWithGrades(student.Id);
        
        var semesters = subjects
            .GroupBy(s => s.Session.SemesterNumber)
            .Select(g => new SemesterSubjectsViewModel
            {
                SemesterNumber = g.Key,

                ZalikSubjects = g
                    .Where(s => s.Type == Zalik_Ispit.Zalik)
                    .ToList(),

                ExamSubjects = g
                    .Where(s => s.Type == Zalik_Ispit.Ispit)
                    .ToList()
            })
            .OrderBy(s => s.SemesterNumber)
            .ToList();

        var model = new StudentSessionSubjectsViewModel
        {
            Student = student,
            Semesters = semesters
        };

        return View(model);
    }

    [HttpGet("edit/{id?}")]
    public async Task<IActionResult> Edit()
    {
        return View();
    }
    
    [HttpPost("edit/{id?}")]
    public async Task<IActionResult> Edit(StudentModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = GetUserId();
        var student = await studentService.GetStudentById(userId);
        
        student.FirstName = model.FirstName;
        student.LastName = model.LastName;

        await studentService.UpdateStudent(student);

        return RedirectToAction("Index");
    }
    
}