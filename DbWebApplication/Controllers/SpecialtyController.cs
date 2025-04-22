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
        var specialties = await specialtyService.GetSpecialties();

        var model = new Specialty_Index_ViewModel
        {
            User = user,
            Specialties = specialties
        };
        
        return View(model);
    }

    public async Task<IActionResult> ShowSpecialty(int id)
    {
        var specialty = await specialtyService.GetSpecialtyById(id);

        return View(specialty);
    }

    public async Task<IActionResult> CreateSpecialty(int id)
    {
        return View(id);
    }
    
    public async Task<IActionResult> CreateSpecialty(SpecialtyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await specialtyService.AddSpecialty(model);
        
        return RedirectToAction("Index"); 
    }
    
    public async Task<IActionResult> EditSpecialty(int id)
    {
        var specialty = await specialtyService.GetSpecialtyById(id);
        
        return View(specialty);
    }

    public async Task<IActionResult> EditSpecialty(SpecialtyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await specialtyService.UpdateSpecialty(model);
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteSpecialty(int id)
    {
        await specialtyService.DeleteSpecialty(id);
        
        return RedirectToAction("Index"); 
    }
}