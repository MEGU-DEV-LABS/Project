using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

public class SpecialtyController(
    SpecialtyService specialtyService,
     UserService userService): Controller
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
        var specialties = await specialtyService.GetSpecialtiesAsync();

        var model = new Specialty_Index_ViewModel
        {
            User = user,
            Specialties = specialties
        };
        
        return View(model);
    }

    public async Task<IActionResult> CreateSpecialty()
    {
        
        return View();
    }
    
    public async Task CreateSpecialty(SpecialtyModel model)
    {
        if (!ModelState.IsValid)
        {
            model.FacultyId
        }
        specialtyService.AddSpecialtyAsync(model);
    }
}