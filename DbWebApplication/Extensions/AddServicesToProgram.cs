using DbWebApplication.Data;
using DbWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Extensions;

public static class AddServicesToProgram
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        /*services.AddScoped<StudentService>();
        services.AddScoped<UserService>();*/
        services.AddScoped<QrCodeService>();
        return services;
    }
    
    public static IServiceCollection AddStandardServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSession();
        services.AddControllersWithViews();
        return services;
    }
    
    public static IServiceCollection AddAppCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/Login";
            options.AccessDeniedPath = "/User/AccessDenied";
        });
        return services;
    }
    
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
}