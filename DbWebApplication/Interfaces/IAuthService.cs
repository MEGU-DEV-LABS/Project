namespace DbWebApplication.Interfaces;

public interface IAuthService
{
    public Task Register(string firstName, string fatherName, string secondName,
        string email, string password, string phoneNumber);

    public Task<string> Login(string email, string password);
}