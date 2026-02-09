using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode.Internal;

namespace DbWebApplication.Controllers;

[Route("faculty")]
public class FacultyController(
    IFacultyService facultyService,
    IUserService userService
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
    
    [HttpGet("faculties")]
    public async Task<IActionResult> AllFaculties()
    {
        var faculties = await facultyService.GetAllFaculties();
        
        return View(faculties);
    }

    [HttpGet("showFaculty/{id}")]
    public async Task<IActionResult> ShowFaculty(int id)
    {
        var faculty = await facultyService.GetFacultyById(id);
        
        return View(faculty);
    }
    
    [HttpGet("create")]
    public async Task<IActionResult> CreateFaculty()
    {
        return View();
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateFaculty(CreateFacultyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        FacultyModel faculty = new FacultyModel()
        {
            Name = model.Name
        };
        
        await facultyService.CreateFaculty(faculty);
        
        return RedirectToAction("AllFaculties");
    }
    
    [HttpGet("edit/{id}")]
    public async Task<IActionResult> EditFaculty(int id)
    {
        var faculty = await facultyService.GetFacultyById(id);
        
        if (faculty == null)
        {
            return NotFound();
        }
        
        return View(faculty);
    }
    
    [HttpPost("edit/{id}")]
    public async Task<IActionResult> EditFaculty(FacultyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await facultyService.UpdateFaculty(model);
        
        return RedirectToAction("AllFaculties");
    }
    
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        try
        {
            await facultyService.DeleteFaculty(id);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
        
        return RedirectToAction("AllFaculties");
    }
}