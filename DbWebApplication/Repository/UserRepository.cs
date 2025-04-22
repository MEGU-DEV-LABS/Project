using System.Linq.Expressions;
using DbWebApplication.Data;
using DbWebApplication.Enum;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Repository;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task Create(AppUserModel userModel)
    {
        await context.AppUsers.AddAsync(userModel);
        await context.SaveChangesAsync();
    }
    
    public async Task Update(AppUserModel userModel)
    {
        await context.AppUsers
            .Where(b => b.Id == userModel.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.FirstName, userModel.FirstName)
                .SetProperty(p => p.FatherName, userModel.FatherName)
                .SetProperty(p => p.LastName, userModel.LastName)
                .SetProperty(p => p.Password, userModel.Password)
                .SetProperty(p => p.Email, userModel.Email)
                .SetProperty(p => p.PhoneNumber, userModel.PhoneNumber)
                .SetProperty(p => p.Role, userModel.Role)
                .SetProperty(p => p.QrCodeToken, userModel.QrCodeToken)
                .SetProperty(p => p.TokenDateExpired, userModel.TokenDateExpired)
            );
    }


    public async Task UpdateOneProperty<TProperty>(
        int id,
        Func<AppUserModel, TProperty> propertyExpression,
        TProperty newValue)
    {
        await context.AppUsers
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(set => set.SetProperty(propertyExpression, newValue));
    }

    public async Task Delete(int id)
    {
        await context.AppUsers
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }
    
    public async Task<List<AppUserModel>> GetUsers()
    {
        var users = await context.AppUsers
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task<AppUserModel?> GetUserByIdAsync(int id)
    {
        return await context.AppUsers
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUserModel?> GetUserByEmail(string email)
    {
        var user = await context.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        
        return user; 
    }
    
    public async Task<AppUserModel?> GetUserByStudentId(int id)
    {
        var user = await context.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.StudentId == id);
        
        return user; 
    }
}