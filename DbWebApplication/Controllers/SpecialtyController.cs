using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Controllers;

[Authorize(Roles = "Admin")]
[Route("specialty")]
public class SpecialtyController(
    ISpecialtyService specialtyService,
    IStudyPlanService studyPlanService, ISubjectService subjectService,
    ISessionService sessionService,
    AppDbContext context): Controller
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
    public async Task<IActionResult> AllSpecialties(int id)
    {
        var specialties = await specialtyService.GetSpecialties(id);
        
        return View(specialties);
    }

    [HttpGet("specialty/{id}")]
    public async Task<IActionResult> ShowSpecialty(int id)
    {
        var specialty = await specialtyService.GetSpecialtyById(id);

        return View(specialty);
    }

    [HttpGet]
    public async Task<IActionResult> CreateSpecialty(int id)
    {
        var model = new SpecialtyModel
        {
            FacultyId = id
        };

        return View(model);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateSpecialty(SpecialtyModel model)
    {
        await specialtyService.AddSpecialty(model);
        
        return RedirectToAction("ShowFaculty", "Faculty", new { id = model.FacultyId });
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
        
        return RedirectToAction("AllSpecialties");
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteSpecialty(int id)
    {
        await specialtyService.DeleteSpecialty(id);
        
        return RedirectToAction("AllSpecialties"); 
    }
    
    [HttpGet("specialty-students/{specialtyId}")]
    public async Task<IActionResult> SpecialtyStudents(int specialtyId)
    {
        var students = await specialtyService.GetStudentsBySpecialtyId(specialtyId);
        return View(students);
    }
    
    [HttpGet("create-study-plan")]
    public async Task<IActionResult> CreateStudyPlan(int specialtyId)
    {
        var subjects = await subjectService.GetAllSubjectsAsync();
        @ViewBag.SpecialtyId = specialtyId;
        return View(subjects);
    }
    
    [HttpPost("create-study-plan")]
    public async Task<IActionResult> CreateStudyPlan(int specialtyId, List<int> subjectIds)
    {
        await studyPlanService.CreateStudyPlanAsync(specialtyId, subjectIds);
        
        return RedirectToAction("ShowSpecialty", new { id = specialtyId });
    }
    
    [HttpGet("create-session")]
    public async Task<IActionResult> CreateSession(int specialtyId)
    {
        var s = await studyPlanService.GetLastSemester(specialtyId);
        var subjects = s.Subjects;
        @ViewBag.SpecialtyId = specialtyId;
        return View((List<SubjectModel>)subjects);
    }
    
    [HttpPost("create-session")]
    public async Task<IActionResult> CreateSession(
        int specialtyId,
        List<int> subjectIds,
        List<int> examTypes)
    {
        if (subjectIds.Count != examTypes.Count)
            return BadRequest("Subjects and exam types mismatch");

        var subjects = await context.Subjects
            .Where(s => subjectIds.Contains(s.SubjectID))
            .Select(s => new
            {
                s.SubjectID,
                s.SubjectName,
                s.Hours,
                s.Credits,
                s.TeacherId
            })
            .ToListAsync();

        var session = new Session
        {
            SpecialtyId = specialtyId
        };

        var sessionSubjects = subjectIds.Select((id, index) =>
        {
            var subject = subjects.First(s => s.SubjectID == id);

            return new SessionSubjects
            {
                SubjectName = subject.SubjectName,
                TeacherId = subject.TeacherId,
                Type = examTypes[index] == 1
                    ? Enum.Zalik_Ispit.Zalik
                    : Enum.Zalik_Ispit.Ispit,
                SubjectId = subject.SubjectID,
                Hours = subject.Hours,
                Credits = subject.Credits
            };
        }).ToList();

        await sessionService.AddSubjectsToSession(session, sessionSubjects);

        return RedirectToAction("ShowSpecialty", new { id = specialtyId });
    }



}