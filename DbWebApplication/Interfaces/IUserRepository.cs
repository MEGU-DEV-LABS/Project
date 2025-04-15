using DbWebApplication.Enum;
using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IUserRepository
{
    Task Create(AppUserModel userModel);

    Task Update(int id, string firstName, string fatherName, string lastName,
        string passwordHash, string email, string phoneNumber, Role role,
        Guid qrToken, DateTime tokenDateExpired);
    Task UpdateOneProperty<TProperty>(
        int id,
        Func<AppUserModel, TProperty> propertyExpression,
        TProperty newValue);
    Task Delete(int id);
    Task<List<AppUserModel>> GetUsers();
    Task<AppUserModel?> GetUserByEmail(string email);
}