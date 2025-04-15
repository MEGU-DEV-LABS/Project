using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IAuthService
{
    public Task<string> Register(string firstName, string fatherName, string secondName,
        string email, string password, string phoneNumber);
    public Task<string> Login(string email, string password);
    Task AddQrTokenToUserAsync(AppUserModel user);
    Task<string> LoginWithQr(string qrToken);
}