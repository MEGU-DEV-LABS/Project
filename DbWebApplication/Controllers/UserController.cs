using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using DbWebApplication.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode;

namespace DbWebApplication;

public class UserController(
    ILogger<UserController> logger,
    StudentService studentService,
    QrCodeService qrCodeService)
    : Controller
{
    private int GetUserId()
    {
        if (HttpContext.Items["UserId"] is int userId)
        {
            return userId;
        }

        throw new UnauthorizedAccessException("UserId not found in the context.");
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
        return RedirectToAction("Index", "Student");
    }
    
}