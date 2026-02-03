using DbWebApplication.Data;
using DbWebApplication.Dto;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Repository;
using Microsoft.AspNetCore.Identity;

namespace DbWebApplication.Services;

public class UserService(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IStudentRepository studentRepository) : IUserService
{
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
    
    public async Task<List<AppUserModel>> GetUsers()
    {
        var users = await userRepository.GetUsers();
        if (users == null || !users.Any())
        {
            throw new NullReferenceException("No users found");
        }
        return users;
    }

    public async Task<StudentModel> GetStudentByUserId(int userId)
    {
        var student = await studentRepository.GetByIdAsync(userId);
        if (student == null)
        {
            throw new NullReferenceException("User not found");
        }
        return student;
    }
    
    public async Task UpdateUser(AppUserModel userModel)
    {
        if (userModel == null)
        {
            throw new ArgumentNullException(nameof(userModel), "User model cannot be null");
        }

        await userRepository.Update(userModel);
    }

    public async Task DeleteUser(int userId)
    {
        await userRepository.Delete(userId);
    }

    public async Task AddQrTokenToStudentAsync(AppUserModel user)
    {
        await userRepository.AddQrTokenToStudentAsync(user);
    }

    public async Task<AppUserModel> GetStudentByQrToken(string token)
    {
        return await userRepository.GetStudentByQrToken(token);
    }
}