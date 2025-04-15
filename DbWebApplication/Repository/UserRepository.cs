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
        var user = new AppUserModel
        {
            FirstName = userModel.FirstName,
            FatherName = userModel.FatherName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            Password = userModel.Password,
            Role = userModel.Role,
            PhoneNumber = userModel.PhoneNumber
        };
        
        await context.AppUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }


    public async Task Update(int id, string firstName, string fatherName, string lastName,
        string passwordHash, string email, string phoneNumber, Role role, Guid qrToken, DateTime tokenDateExpired)
    {
        await context.AppUsers
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.FirstName, firstName)
                .SetProperty(p => p.FatherName, fatherName)
                .SetProperty(p => p.LastName, lastName)
                .SetProperty(p => p.Password, passwordHash)
                .SetProperty(p => p.Email, email)
                .SetProperty(p=> p.PhoneNumber, phoneNumber)
                .SetProperty(p=> p.Role, role)
                .SetProperty(p=> p.QrCodeToken, qrToken)
                .SetProperty(p=> p.TokenDateExpired, tokenDateExpired)
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