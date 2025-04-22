using DbWebApplication.Data;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using DbWebApplication.Enum;
using DbWebApplication.Repository;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Services;

public class UserService(AppDbContext context,
    UserManager<AppUserModel> userManager,
    IHttpContextAccessor httpContextAccessor,
    UserRepository userRepository)
{
    public AppUserModel CreateUser()
    {
        try
        {
            return Activator.CreateInstance<AppUserModel>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUserModel)}'. " +
                                                $"Ensure that '{nameof(AppUserModel)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }
    
    public async Task GetRolesAsync(AppUserModel user, RegisterViewModel model)
    {
        if (model.Role == Role.Admin)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
        else
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }
    
    public async Task CreateStudentIfNotAdmin(RegisterViewModel model, AppUserModel user)
    {
        if (model.Role != Role.Admin)
        {
            var student = new StudentModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                Id = user.Id 
            };
            
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }
    }
    
    public int GetUserId()
    {
        if (httpContextAccessor.HttpContext?.Items["UserId"] is int userId)
        {
            return userId;
        }

        throw new UnauthorizedAccessException("UserId not found in the context.");
    }
    
    public async Task<AppUserModel> GetUser(int userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }
        
        return user;
    }
}