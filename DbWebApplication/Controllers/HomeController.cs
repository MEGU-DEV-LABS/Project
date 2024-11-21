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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace DbWebApplication.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    StudentService studentService,
    UserManager<ApplicationUser> userManager,
    AppDbContext context)
    : Controller
{

    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Index()
    {
        if(User.IsInRole("Admin"))
        {
            return RedirectToAction("AdminPanel", "Admin");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GradesOfStudent()
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View("User not found");
        }

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

    [HttpGet]
    public async Task<IActionResult> StudentZalikovka()
    {
        var user = await userManager.GetUserAsync(User);
        var student = await studentService.GetStudentByUserId(user.Id);
        var sessionSubject = await studentService.PrepareSessionSubjectViewModelAsync(student);
        return View(sessionSubject);
    }
}