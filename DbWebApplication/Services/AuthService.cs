using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Services;

public class AuthService(
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository,
    IStudentRepository studentRepository,
    IFacultyRepository facultyRepository,
    ITeacherRepository teacherRepository,
    AppDbContext context) : IAuthService
{
    public async Task<string> Register(RegisterUserViewModel regModel)
    {
        var emailProb = await userRepository.GetUserByEmail(regModel.Email);
        if (emailProb != null)
        {
            throw new Exception("Email is already in use.");
        }
        string passwordHash = passwordHasher.Generate(regModel.Password);
        var model = new AppUserModel
        {
            FirstName = regModel.FirstName,
            FatherName = regModel.FatherName,
            LastName = regModel.LastName,
            Email = regModel.Email,
            Password = passwordHash,
            Role = regModel.Role,
            PhoneNumber = regModel.PhoneNumber
        };
        await userRepository.Create(model);
        
        var token = jwtProvider.Created(model);
        return token;
    }
    
    public async Task RegisterStudent(RegisterUserViewModel model)
    {
        var existing = await userRepository.GetUserByEmail(model.Email);
        if (existing != null)
            throw new Exception("Email already exists.");

        var hashedPassword = passwordHasher.Generate(model.Password);

        var user = new AppUserModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FatherName = model.FatherName,
            Email = model.Email,
            Password = hashedPassword,
            PhoneNumber = model.PhoneNumber,
            Role = Role.Student
        };

        await userRepository.Create(user); 

        var createdUser = await userRepository.GetUserByEmail(model.Email);

        if (createdUser == null)
            throw new Exception("User creation failed.");

        var student = new StudentModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FatherName = model.FatherName,
            AppUserId = createdUser.Id,
            SpecialtyId = model.SpecialtyId.GetValueOrDefault()
        };

        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
        
        var createdStudent = await studentRepository.GetByAppUserIdAsync(createdUser.Id);
        
        createdUser.StudentId = createdStudent.Id;
        
        await userRepository.Update(createdUser);
    }


    public async Task<string> Login(string email, string password)
    {
        var token = "";
        var user = await userRepository.GetUserByEmail(email);
        var result = passwordHasher.Verify(password, user.Password);
        if (result)
        {
            token = jwtProvider.Created(user);
        }
        return token;
    }
    
    //для входу через QR-код
    public async Task<string> LoginWithQr(string qrToken)
    {
        var user = await GetUserByQrToken(qrToken);
        if (user == null)
        {
            throw new Exception("No such user or token expired.");
        }
        var token = jwtProvider.Created(user);
        return token;
    }
    
    public async Task AddQrTokenToUserAsync(AppUserModel user)
    {
        Guid token = Guid.NewGuid();
        user.QrCodeToken = token;
        DateTime now = DateTime.Now;
        DateTime time = now.AddDays(7);
        user.TokenDateExpired = time;
        await userRepository.UpdateOneProperty(user.Id, s => s.QrCodeToken, token);
        await userRepository.UpdateOneProperty(user.Id, s => s.TokenDateExpired, time);
    }

    private async Task<AppUserModel> GetUserByQrToken(string token)
    {
        Guid userToken = Guid.Parse(token);
        return await context.AppUsers.FirstOrDefaultAsync(s =>
            s.QrCodeToken == userToken && s.TokenDateExpired >= DateTime.Now);
    }

    public async Task RegisterTeacher(RegisterUserViewModel model)
    {
        var existing = await userRepository.GetUserByEmail(model.Email);
        if (existing != null)
            throw new Exception("Email already exists.");

        var hashedPassword = passwordHasher.Generate(model.Password);

        var user = new AppUserModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FatherName = model.FatherName,
            Email = model.Email,
            Password = hashedPassword,
            PhoneNumber = model.PhoneNumber,
            Role = Role.Teacher
        };

        await userRepository.Create(user); 

        var createdUser = await userRepository.GetUserByEmail(model.Email);

        if (createdUser == null)
            throw new Exception("User creation failed.");

        var teacher = new TeacherModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FatherName = model.FatherName,
            AppUserId = createdUser.Id,
            FacultyId = facultyRepository.GetFacultyBySpecialty(model.SpecialtyId.GetValueOrDefault()).Id
        };

        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();
        
        var createdTeacher = await teacherRepository.GetByAppUserIdAsync(createdUser.Id);
        
        createdUser.StudentId = createdTeacher.Id;
        
        await userRepository.Update(createdUser);
    }
}