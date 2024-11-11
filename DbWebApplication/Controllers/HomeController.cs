using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using DbWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using DbWebApplication.Models;
using DbWebApplication.Repository;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace DbWebApplication.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    StudentService studentService,
    UserManager<ApplicationUser> userManager)
    : Controller
{

    /*public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(StudentModel model)
    {
        var students = await _studentRepository
            .FindByConditionAsync(s => 
                s.LastName == model.LastName &&
                s.FirstName == model.FirstName &&
                s.FatherName == model.FatherName &&
                s.Faculty == model.Faculty);

        var foundStudent = students.FirstOrDefault();

        if (foundStudent == null)
        {
            // Якщо студента не знайдено, повертаємо помилку
            ViewBag.ErrorMessage = "Студента не знайдено";
            return View(model);
        }

        var subjects = foundStudent.Subjects.Select(sub => new SubjectOverallViewModel
        {
            SubjectName = sub.SubjectName,
            OverallGrade = (int)sub.LabWorks
                .Select(lab => lab.LabWorkGrades
                    .FirstOrDefault(lg => lg.StudentID == foundStudent.Id)?.GradeValue ?? 0)
                .Sum() // Підсумовуємо всі оцінки за лабораторні роботи
        }).ToList();
        
        // Формування ViewModel для передачі на сторінку GradesOfStudent
        var studentGradesViewModel = new StudentGradesViewModel
        {
            StudentFullName = $"{foundStudent.LastName} {foundStudent.FirstName} {foundStudent.FatherName}",
            Faculty = foundStudent.Faculty.GetDisplayName(),
            Subjects = subjects
        };

        // Зберігаємо ViewModel у TempData
        TempData["StudentGrades"] = JsonConvert.SerializeObject(studentGradesViewModel);

        // Перенаправляємо на сторінку GradesOfStudent
        return RedirectToAction("GradesOfStudent", "Home");
    }*/

    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Index()
    {
        /*if (User.IsInRole("User"))
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("User not found");
            }

            var student = _studentService.GetStudentByUserId(user.Id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            foreach (var subject in student.Subjects)
            {
                if (subject.ImageData != null && subject.ImageData.Length > 0)
                {
                    subject.ImageBase64 = Convert.ToBase64String(subject.ImageData);
                }
            }

            return View(student);
        }
        else if(User.IsInRole("Admin"))
        {
            return RedirectToAction("AdminPanel", "Admin");
        }
        */
        if(User.IsInRole("Admin"))
        {
            return RedirectToAction("AdminPanel", "Admin");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GradesOfStudent()
    {
        /*if (TempData["StudentGrades"] == null)
        {
            ViewBag.ErrorMessage = "Дані не знайдено";
            return View();
        }

        var studentGradesViewModel = JsonConvert.DeserializeObject<StudentGradesViewModel>(TempData["StudentGrades"].ToString());

        TempData.Keep("StudentGrades");

        return View(studentGradesViewModel);*/
        
        
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View("User not found");
        }

        /*var student = _studentService.GetStudentByUserId(user.Id);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        foreach (var subject in student.Subjects)
        {
            if (subject.ImageData != null && subject.ImageData.Length > 0)
            {
                subject.ImageBase64 = Convert.ToBase64String(subject.ImageData);
            }
        }*/
        var student = await studentService.PrepareStudentGradesViewModelAsync(user.Id);
        
        if (typeof(StudentGradesViewModel) == null)
        {
            return View();
        }
        
        return View(student);
        
    }

    [HttpGet]
    public async Task<IActionResult> SubjectDetails(int id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View("User not found");
        }

        var subject = await studentService.PrepareSubjectGradesViewModelAsync(id, user.Id);

        return View(subject);
    }
}