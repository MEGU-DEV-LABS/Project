using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using DbWebApplication.Data;
using DbWebApplication.LightMethodHelpers;
using Microsoft.AspNetCore.Mvc;
using DbWebApplication.Models;
using DbWebApplication.Repository;
using DbWebApplication.ViewModels;
using Newtonsoft.Json;


namespace DbWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GenericRepository<StudentModel> _studentRepository;
    private readonly GenericRepository<SubjectModel> _subjectRepository;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _studentRepository = new GenericRepository<StudentModel>(context);
        _subjectRepository = new GenericRepository<SubjectModel>(context);
    }

    
    public IActionResult Index()
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
    }



    public async Task<IActionResult> GradesOfStudent()
    {
        if (TempData["StudentGrades"] == null)
        {
            ViewBag.ErrorMessage = "Дані не знайдено";
            return View();
        }

        var studentGradesViewModel = JsonConvert.DeserializeObject<StudentGradesViewModel>(TempData["StudentGrades"].ToString());

        // Зберігаємо дані в TempData, щоб вони залишилися доступними після перезавантаження
        TempData.Keep("StudentGrades");

        return View(studentGradesViewModel);
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}