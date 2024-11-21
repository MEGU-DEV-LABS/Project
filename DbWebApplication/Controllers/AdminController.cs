using System.Text;
using System.Text.Encodings.Web;
using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Python.Runtime;


namespace DbWebApplication.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(
    StudentService studentService,
    UserManager<ApplicationUser> userManager,
    AppDbContext context,
    SignInManager<ApplicationUser> signInManager,
    UserService userService,
    QrCodeService qrCodeService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> AdminPanel()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound("Помилка");
        }

        var model = new AdminViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
            var externalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid || model.Role == Role.Admin)
            {
                var user = userService.CreateUser();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;
                user.Email = model.Email;
                
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userService.GetRolesAsync(user,model);
                    await userService.CreateStudentIfNotAdmin(model, user);
                    
                    return RedirectToAction(nameof(Register));
                }
            }
            
            return View(new RegisterViewModel());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            byte[] imageData = await studentService.ConvertImageToByteArrayAsync(model.imageFile); 

            var subject = new SubjectModel()
            {
                SubjectName = model.SubjectName,
                ImageData = imageData
            };

            context.Subjects.Add(subject); 
            await context.SaveChangesAsync();
            return RedirectToAction("AdminPanel");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EnrollStudent()
    {
        var model = new EnrollStudentViewModel
        {
            Subjects = studentService.GetSubjectsNames()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EnrollStudent(EnrollStudentViewModel model)
    {
        var student = await studentService.GetByIdAsync<StudentModel>(model);
        var subject = await studentService.GetByIdAsync<SubjectModel>(model);

        if (student == null && subject == null)
        {
            return NotFound();
        }

        await studentService.AddStudentToSubjectAsync(student.Id, subject.SubjectID);
        
        return View(model);
    }

    [HttpGet]
    public IActionResult StudentsList()
    {
        var list = context.Students.ToList();
        return View(list);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentsList(int Id)
    {
        var s = await studentService.GetStudentByIdAsync(Id);
        await studentService.AddQrTokenToStudentAsync(s);
        var qr = qrCodeService.GenerateQRCode(s.QrCodeToken);
        var res = qrCodeService.ConvertBitmapToByteArray(qr);
        return File(res, "image/png", $"{s.LastName}.png");
    }

    [HttpGet]
    public async Task<IActionResult> DetermineSessionSubjects()
    {
        var subjects = await studentService.GetSubjectsWithIdAsync();
        return View(subjects);
    }
    
    [HttpPost]
    public async Task<IActionResult> DetermineSessionSubjects(ListSubcestsAndIDViewModel model)
    {
        await studentService.AddSubjectToSessionAsync(model);
        return RedirectToAction("StudentsList");
    }

    [HttpGet]
    public async Task<IActionResult> EditStudentZalikovka(int Id)
    {
        var student = await studentService.GetStudentByIdAsync(Id);
        ViewBag.Student = student;
        var sessionSubject = await studentService.PrepareSessionSubjectViewModelAsync(student);
        return View(sessionSubject);
    }

    [HttpPost]
    public async Task<IActionResult> EditStudentZalikovka(List<SessionSubjectViewModel> list, int studentId)
    {
        var student = await studentService.GetStudentByIdAsync(studentId);

        foreach (var sub in list)
        {
            var sessionSubject = await studentService.GetSessionSubjectAsync(sub.SessionSubject, student.Faculty);

            if (sessionSubject != null)
            {
                var grade = await studentService.GetSessionGradeAsync(studentId, sessionSubject.Id);

                if (grade != null)
                {
                    await studentService.UpdateSessionGradeAsync(grade, sub.Grade);
                }
            }
        }

        return RedirectToAction("StudentsList");
    }
}