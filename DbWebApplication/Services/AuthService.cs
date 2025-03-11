using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class AuthService(
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository) : IAuthService
{
    public async Task Register(string firstName, string fatherName, string lastName,
        string email, string password, string phoneNumber)
    {
        var emailProb = await userRepository.GetUserByEmail(email);
        if (emailProb != null)
        {
            throw new Exception("Email is already in use.");
        }
        string passwordHash = passwordHasher.Generate(password);
        var model = new AppUserModel
        {
            FirstName = firstName,
            FatherName = fatherName,
            LastName = lastName,
            Email = email,
            Password = passwordHash,
            PhoneNumber = phoneNumber
        };
        await userRepository.Create(model);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await userRepository.GetUserByEmail(email);
        if (user != null)
        {
            throw new Exception("Email is already in use.");
        }
        var result = passwordHasher.Verify(password, user.Password);
        var token = jwtProvider.Created(user);
        return token;
    }
    
    //для входу через QR-код
    public async Task<string> Login(string qrToken)
    {
        var user = await userRepository.GetUserByEmail(email);
        if (user != null)
        {
            throw new Exception("Email is already in use.");
        }
        var token = jwtProvider.Created(user);
        return token;
    }
    
    public async Task AddQrTokenToUserAsync(AppUserModel user)
    {
        Guid token = System.Guid.NewGuid();
        user.QrCodeToken = token;
        DateTime now = DateTime.Now;
        DateTime time = now.AddDays(7);
        user.TokenDateExpired = time;
        await userRepository.Update(user.Id,
            user.FirstName, user.FatherName,
            user.LastName, user.Password, user.Email);
    }

    public async Task<AppUserModel> GetUserByQrToken(string token)
    {
        int userToken = int.Parse(token);
        return await context.AppUsers.FirstOrDefaultAsync(s =>
            s.QrCodeToken == userToken && s.TokenDateExpired >= DateTime.Now);
    }
}