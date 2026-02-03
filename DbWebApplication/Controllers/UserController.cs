using DbWebApplication.Enum;
using DbWebApplication.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

[Route("user")]
public class UserController(
        IUserService service)
    : Controller
{
    [HttpGet("userslist")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await service.GetUsers();
        return View(users);
    }
    
    [HttpGet("students")]
    public async Task<IActionResult> GetAllStudents(int specialtyId)
    {
        var user = await service.GetUsers();
        var students = user
            .Where(u => u.Student?.SpecialtyId == specialtyId && u.Role == Role.Student)
            .ToList();
        return View(students);
    }
}