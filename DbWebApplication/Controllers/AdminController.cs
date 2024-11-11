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


namespace DbWebApplication.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly StudentService _studentService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserService _userService;
    private readonly QrCodeService _qrCodeService;
    
    public AdminController(
        StudentService studentService, 
        UserManager<ApplicationUser> userManager, 
        AppDbContext context, 
        SignInManager<ApplicationUser> signInManager, UserService userService, QrCodeService qrCodeService)
    {
        _studentService = studentService;
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _userService = userService;
        _qrCodeService = qrCodeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> AdminPanel()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        var user = await _userManager.GetUserAsync(User);

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
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid || model.Role == Role.Admin)
            {
                var user = _userService.CreateUser();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;
                user.Email = model.Email;
                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userService.GetRolesAsync(user,model);
                    await _userService.CreateStudentIfNotAdmin(model, user);
                    
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
            byte[] imageData = await _studentService.ConvertImageToByteArrayAsync(model.imageFile); 

            var subject = new SubjectModel()
            {
                SubjectName = model.SubjectName,
                ImageData = imageData
            };

            _context.Subjects.Add(subject); 
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminPanel");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EnrollStudent()
    {
        var model = new EnrollStudentViewModel
        {
            Subjects = _studentService.GetSubjectsNames()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EnrollStudent(EnrollStudentViewModel model)
    {
        var student = await _studentService.GetByIdAsync<StudentModel>(model);
        var subject = await _studentService.GetByIdAsync<SubjectModel>(model);

        if (student == null && subject == null)
        {
            return NotFound();
        }

        await _studentService.AddStudentToSubjectAsync(student.Id, subject.SubjectID);
        
        return View(model);
    }

    [HttpGet]
    public IActionResult StudentsList()
    {
        var list = _context.Students.ToList();
        return View(list);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentsList(string FirstName, string lastName, string fatherName, Faculty faculty)
    {
        var student = new EnrollStudentViewModel()
        {
            FirstName = FirstName,
            LastName = lastName,
            FatherName = fatherName,
            Faculty = faculty
        };

        var s = await _studentService.GetByIdAsync<StudentModel>(student);
        await _studentService.AddQrTokenToStudentAsync(s);
        var qr = _qrCodeService.GenerateQRCode(s.QrCodeToken);
        var res = _qrCodeService.ConvertBitmapToByteArray(qr);
        return File(res, "image/png", $"{student.LastName}.png");
        return View();
    }

    
    
}