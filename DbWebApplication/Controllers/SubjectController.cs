using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

public class SubjectController(ISubjectService service, AppDbContext context) : Controller
{
    [HttpGet("subjects")]
    public async Task<IActionResult> AllSubjects()
    {
        var subjects = await service.GetAllSubjectsAsync();
        foreach (var subject in subjects)
        {
            if (subject.ImageData != null)
            {
                subject.ImageBase64 = Convert.ToBase64String(subject.ImageData);
            }
        }
        return View(subjects);
    }
    
    [HttpGet("showSubject/{id}")]
    public async Task<IActionResult> ShowSubject(int id)
    {
        var subject = await service.GetSubjectByIdAsync(id);
        
        if (subject == null)
        {
            return NotFound();
        }
        
        return View(subject);
    }
    
    [HttpGet("create")]
    public IActionResult CreateSubject(int specialtyId)
    {
        var teachers = context.Teachers.ToList();
        ViewBag.Teachers = teachers;
        ViewBag.SpecialtyId = specialtyId;
        return View();
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateSubject(CreateSubjectViewModel model, IFormFile ImageFile, int specialtyId)
    {
        if(!ModelState.IsValid)
        {
            var teachers = context.Teachers.ToList();
            ViewBag.Teachers = teachers;
            ViewBag.SpecialtyId = specialtyId;
            return View(model);
        }

        SubjectModel subject = new SubjectModel()
        {
            SubjectName = model.SubjectName,
            Hours = model.Hours,
            Credits = model.Credits,
            TeacherId = model.TeacherId
        };
        
        if (ImageFile != null && ImageFile.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await ImageFile.CopyToAsync(ms);
                subject.ImageData = ms.ToArray();
            }
        }
        await service.CreateSubjectAsync(subject, specialtyId);
        
        return RedirectToAction("AllSubjects");
    }
    
    [HttpGet("edit/{id}")]
    public async Task<IActionResult> EditSubject(int id)
    {
        var subject = await service.GetSubjectByIdAsync(id);
        
        if (subject == null)
        {
            return NotFound();
        }
        
        return View(subject);
    }
    
    [HttpPost("edit/{id}")]
    public async Task<IActionResult> EditSubject(SubjectModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await service.UpdateSubjectAsync(model);
        
        return RedirectToAction("AllSubjects");
    }
    
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        try
        {
            await service.DeleteSubjectAsync(id);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
        
        return RedirectToAction("AllSubjects");
    }
}