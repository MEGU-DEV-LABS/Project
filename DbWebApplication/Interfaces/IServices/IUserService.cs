using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface IUserService
{
    Task<AppUserModel> GetUser(int userId);
    Task<List<AppUserModel>> GetUsers();
    Task<StudentModel> GetStudentByUserId(int userId);
    Task UpdateUser(AppUserModel userModel);
    Task DeleteUser(int userId);
    Task AddQrTokenToStudentAsync(AppUserModel user);
    Task<AppUserModel> GetStudentByQrToken(string token);
}