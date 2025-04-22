using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

[Route("specialty")]
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

    [HttpGet("specialties")]
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

    [HttpGet("specialty/{id}")]
    public async Task<IActionResult> ShowSpecialty(int id)
    {
        var specialty = await specialtyService.GetSpecialtyById(id);

        return View(specialty);
    }

    [HttpGet("/create")]
    public async Task<IActionResult> CreateSpecialty(int id)
    {
        return View(id);
    }
    
    [HttpPost("/create")]
    public async Task<IActionResult> CreateSpecialty(SpecialtyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await specialtyService.AddSpecialty(model);
        
        return RedirectToAction("Index"); 
    }
    
    [HttpGet("edit/{id}")]
    public async Task<IActionResult> EditSpecialty(int id)
    {
        var specialty = await specialtyService.GetSpecialtyById(id);
        
        return View(specialty);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> EditSpecialty(SpecialtyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await specialtyService.UpdateSpecialty(model);
        
        return RedirectToAction("Index");
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteSpecialty(int id)
    {
        await specialtyService.DeleteSpecialty(id);
        
        return RedirectToAction("Index"); 
    }
}