using DbWebApplication.Interfaces;
using DbWebApplication.Services;
using DbWebApplication.ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DbWebApplication.Controllers;

[Route("auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService,
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
        byte[] imageData = await qrCodeService.ConvertImageToByteArrayAsync(file);
        var qrText = await qrCodeService.ReadQRCode(imageData);
        var token = await authService.LoginWithQr(qrText);
        if (token.IsNullOrEmpty())
        {
            ModelState.AddModelError(string.Empty, "Користувача не знайдено.");
            return View();
        }
        HttpContext.Response.Cookies.Append("tastkook", token);

        return RedirectToAction("Index", "Student");
    }
}