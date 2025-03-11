using DbWebApplication.Interfaces;
using DbWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

[Route("auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService,
    StudentService studentService,
    QrCodeService qrCodeService
) : Controller
{
    [HttpGet("login")]
    public IActionResult Login() => View();
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequests request)
    {
        if (!ModelState.IsValid)
        {
            return Ok(BadRequest(ModelState));
        }

        var token = await authService.Login(request.Email, request.Password);
        HttpContext.Response.Cookies.Append("tastkook", token);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete("tastkook");
        logger.LogInformation("User logged out.");
        return RedirectToAction("Index", "Student");
    }
    
    [HttpGet("loginWithQrCode")]
    public IActionResult LoginWithQrCode() => View();
    
    [HttpPost("loginWithQrCode")]
    public async Task<IActionResult> LoginWithQrCode(IFormFile  file)
    {
        byte[] imageData = await studentService.ConvertImageToByteArrayAsync(file);
        var qrText = await qrCodeService.ReadQRCode(imageData);
        var student = await studentService.GetStudentByQrToken(qrText);
        var token = await authService.Login(student.Email, student.Password);
        HttpContext.Response.Cookies.Append("tastkook", token);
    
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Користувача не знайдено.");
            return View();
        }
        
        await signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Student");
    }
}