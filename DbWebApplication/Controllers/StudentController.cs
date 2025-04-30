using DbWebApplication.Enum;
using DbWebApplication.Extensions;
using DbWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using DbWebApplication.Services;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Controllers;

public class StudentController(
    StudentService studentService,
    UserManager<AppUserModel> userManager)
    : Controller
{
    private int GetUserId()
    {
        if (HttpContext.Items["UserId"] is int userId)
        {
            return userId;
        }

        throw new UnauthorizedAccessException("UserId not found in the context.");
    }

    [AuthorizeByRole(Role.Admin, Role.User)]
    public async Task<IActionResult> Index()
    {
        /*
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

     /*[HttpGet]
    public async Task<IActionResult> SubjectDetails(int id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return View("User not found");
        }

        var subject = await studentService.PrepareSubjectGradesViewModelAsync(id, user.Id);

        return View(subject);
    }*/

    [HttpGet]
    public async Task<IActionResult> StudentZalikovka()
    {
        var user = await userManager.GetUserAsync(User);
        var student = await studentService.GetStudentByUserId(user.Id);
        var sessionSubject = await studentService.PrepareSessionSubjectViewModelAsync(student);
        return View(sessionSubject);
    }
}