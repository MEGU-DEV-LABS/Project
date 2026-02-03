using DbWebApplication.Enum;
using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IUserRepository
{
    Task Create(AppUserModel userModel);

    Task Update(AppUserModel userModel);
    Task UpdateOneProperty<TProperty>(
        int id,
        Func<AppUserModel, TProperty> propertyExpression,
        TProperty newValue);
    Task Delete(int id);
    Task<List<AppUserModel>> GetUsers();
    Task<AppUserModel?> GetUserByIdAsync(int id);
    Task<AppUserModel?> GetUserByEmail(string email);
    Task<AppUserModel?> GetUserByStudentId(int id);
    Task AddQrTokenToStudentAsync(AppUserModel user);
    Task<AppUserModel> GetStudentByQrToken(string token);
}