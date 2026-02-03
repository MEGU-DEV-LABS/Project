using System.Security.Claims;
using DbWebApplication.Enum;
using DbWebApplication.Extensions;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using DbWebApplication.ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DbWebApplication.Controllers;

[Route("auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService,
    IQrCodeService qrCodeService,
    IUserService userService
) : Controller
{
    [HttpGet("login")]
    public IActionResult Login() => View();
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequests request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        else
        {
            var token = await authService.Login(request.Email, request.Password);
            HttpContext.Response.Cookies.Append("tastkook", token);
        
            return RedirectToAction("Redirect");
        }

        
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("tastkook");
        logger.LogInformation("User logged out.");
        return RedirectToAction("Login", "Auth");
    }
    
    [HttpGet("loginWithQrCode")]
    public IActionResult LoginWithQrCode() => View();
    
    [HttpPost("loginWithQrCode")]
    public async Task<IActionResult> LoginWithQrCode(IFormFile  file)
    {
        byte[] imageData = await qrCodeService.ConvertImageToByteArrayAsync(file);
        var qrText = await qrCodeService.ReadQRCode(imageData);
        var token = await authService.LoginWithQr(qrText);
        if (token.IsNullOrEmpty())
        {
            ModelState.AddModelError(string.Empty, "Користувача не знайдено.");
            return View();
        }
        HttpContext.Response.Cookies.Append("tastkook", token);

        return RedirectToAction("Redirect");
    }
    
    [HttpPost("GenerateQrCode")]
    public async Task<IActionResult> GenerateQrCode(int id)
    {
        var s = await userService.GetUser(id);
        await userService.AddQrTokenToStudentAsync(s);
        var qr = qrCodeService.GenerateQRCode(s.QrCodeToken);
        var res = qrCodeService.ConvertBitmapToByteArray(qr);
        return File(res, "image/png", $"{s.LastName}.png");
    }
    
    [HttpGet("redirect")]
    public IActionResult Redirect()
    {
        if (User.IsInRole(Role.Student.ToString()))
        {
            return RedirectToAction("Index", "Student");
        }
        else if(User.IsInRole(Role.Admin.ToString()))
        {
            return RedirectToAction("Index", "Admin");
        }
        else if (User.IsInRole(Role.Teacher.ToString()))
        {
            return RedirectToAction("Index", "Teacher");
        }
        else
        {
            return RedirectToAction("Login", "Auth");
        }
    }
    
    [AuthorizeByRole(Role.Admin)]
    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    
    [HttpPost("register")]
    [AuthorizeByRole(Role.Admin)]
    public async Task<IActionResult> Register(RegisterUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        string token;

        if (model.Role == Role.Student)
        {
            await authService.RegisterStudent(model);
        }
        else if (model.Role == Role.Teacher)
        {
            await authService.RegisterTeacher(model);
        }
        else
        {
            token = await authService.Register(model);
        }

        return RedirectToAction("Index", "Admin");
    }

}