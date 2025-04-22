using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

public class FacultyController(
    FacultyService facultyService,
    UserService userService
    ): Controller
{
    private int GetUserId()
    {
        if (HttpContext.Items["UserId"] is int userId)
        {
            return userId;
        }

        throw new UnauthorizedAccessException("UserId not found in the context.");
    }
    
    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        
        var user = await userService.GetUser(userId);
        var faculties = await facultyService.GetAllFaculties();

        var model = new Faculty_Index_ViewModel
        {
            User = user,
            Faculties = faculties
        };
        
        return View(model);
    }

    public async Task<IActionResult> ShowFaculty(int id)
    {
        var faculty = facultyService.GetFacultyById(id);
        
        return View(faculty);
    }
}