using System.Diagnostics;
using DbWebApplication.Data;
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

        // Отримання предметів студента разом з лабораторними роботами та оцінками
        var subjects = foundStudent.Subjects.Select(sub => new SubjectViewModel
        {
            SubjectName = sub.SubjectName,
            LabWorks = sub.LabWorks.Select(lab => new LabWorkViewModel
            {
                LabWorkName = lab.LabWorkName,
                GradeValue = lab.LabWorkGrades
                    .FirstOrDefault(lg => lg.StudentID == foundStudent.Id)?.GradeValue ?? 0
            }).ToList()
        }).ToList();

        // Формування ViewModel для передачі на сторінку GradesOfStudent
        var studentGradesViewModel = new StudentGradesViewModel
        {
            StudentFullName = $"{foundStudent.LastName} {foundStudent.FirstName} {foundStudent.FatherName}",
            Faculty = foundStudent.Faculty,
            Subjects = subjects
        };

        // Зберігаємо ViewModel у TempData
        TempData["StudentGrades"] = JsonConvert.SerializeObject(studentGradesViewModel);

        // Перенаправляємо на сторінку GradesOfStudent
        return RedirectToAction("GradesOfStudent", "Home");
    }



    public IActionResult GradesOfStudent()
    {
        if (TempData["StudentGrades"] == null)
        {
            ViewBag.ErrorMessage = "Дані не знайдено";
            return View();
        }

        var studentGradesViewModel = JsonConvert.DeserializeObject<StudentGradesViewModel>(TempData["StudentGrades"].ToString());
        return View(studentGradesViewModel);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}