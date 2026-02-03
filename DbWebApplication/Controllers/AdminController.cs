using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Extensions;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

[AuthorizeByRole(Role.Admin)]
[Route("admin")]
public class AdminController()
    : Controller
{
    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
}