using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using DbWebApplication.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode;

namespace DbWebApplication;

public class UserController(
    ILogger<HomeController> logger,
    StudentService studentService,
    SignInManager<ApplicationUser> signInManager,
    QrCodeService qrCodeService,
    UserManager<ApplicationUser> userManager)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        logger.LogInformation("User logged out.");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult LoginWithQrCode()
    {
        return View();
    }   
    [HttpPost]
    public async Task<IActionResult> LoginWithQrCode(IFormFile  file)
    {
        byte[] imageData = await studentService.ConvertImageToByteArrayAsync(file);
        var a = await qrCodeService.ReadQRCode(imageData);
        var student = await studentService.GetStudentByQrToken(a);
        var user = await userManager.FindByIdAsync(student.ApplicationUserId);
    
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Користувача не знайдено.");
            return View();
        }
        
        await signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }
    
}