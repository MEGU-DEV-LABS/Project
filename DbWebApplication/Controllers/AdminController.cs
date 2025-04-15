using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using DbWebApplication.Services;
using DbWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(
    StudentService studentService,
    UserManager<AppUserModel> userManager,
    AppDbContext context,
    SignInManager<AppUserModel> signInManager,
    UserService userService,
    QrCodeService qrCodeService,
    IAuthService authService)
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
    public async Task<IActionResult> AdminPanel()
    {
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
    
    //Registration of new user
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
    
    //Admin adding subject
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
            byte[] imageData = await qrCodeService.ConvertImageToByteArrayAsync(model.imageFile); 

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
    
    //Admin enrolls students to subjects
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
        var student = await studentService.GetStudentByModelAsync(model);
        var subject = await studentService.GetSubjectByModelAsync(model);

        if (student == null && subject == null)
        {
            return NotFound();
        }

        await studentService.AddStudentToSubjectAsync(student.Id, subject.SubjectID);
        
        return View(model);
    }

    //Admin receives list of students
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
        var s = await studentService.GetUserByStudentId(Id);
        await authService.AddQrTokenToUserAsync(s);
        var qr = qrCodeService.GenerateQRCode(s.QrCodeToken);
        var res = qrCodeService.ConvertBitmapToByteArray(qr);
        return File(res, "image/png", $"{s.LastName}.png");
    }

    //Admin adding session subjects
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

    //Admin edits student's zalikovka
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
            var sessionSubject = await studentService.GetSessionSubjectAsync(sub.SessionSubject, student.Specialty);

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
    
    //TODO: Implement the following methods and determine their affiliation to this controller
    //Admin deletes student
    
    //Admin edits user's information
    
    //Admin creates faculty
    
    //Admin creates specialty
    
    //Admin edits faculty
    
    //Admin edits specialty
    
    //Admin deletes faculty
    
    //Admin deletes specialty
    
    //Admin creates new schedule
    
    //Admin edits schedule
}