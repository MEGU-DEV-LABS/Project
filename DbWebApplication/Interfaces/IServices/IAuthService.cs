using DbWebApplication.Models;
using DbWebApplication.ViewModels;

namespace DbWebApplication.Interfaces;

public interface IAuthService
{
    public Task<string> Register(RegisterUserViewModel regModel);
    public Task RegisterStudent(RegisterUserViewModel model);
    public Task<string> Login(string email, string password);
    Task AddQrTokenToUserAsync(AppUserModel user);
    Task<string> LoginWithQr(string qrToken);
    public Task RegisterTeacher(RegisterUserViewModel model);
}