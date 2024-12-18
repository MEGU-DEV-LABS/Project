﻿using DbWebApplication.Data;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Services;

public class UserService(AppDbContext context, UserManager<ApplicationUser> userManager)
{
    public ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }
    
    public async Task GetRolesAsync(ApplicationUser user, RegisterViewModel model)
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
    
    public async Task CreateStudentIfNotAdmin(RegisterViewModel model, ApplicationUser user)
    {
        if (model.Role != Role.Admin)
        {
            var student = new StudentModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                ApplicationUserId = user.Id 
            };
            
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }
    }
    
    
}