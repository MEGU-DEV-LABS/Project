using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IUserRepository
{
    Task Create(AppUserModel userModel);
    Task Update(int id, string firstName, string fatherName, string lastName, string passwordHash, string email);
    Task Delete(int id);
    Task<List<AppUserModel>> GetUsers();
    Task<AppUserModel?> GetUserByEmail(string email);
}