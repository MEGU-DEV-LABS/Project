using DbWebApplication.Data;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Providers;
using DbWebApplication.Repository;
using DbWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Extensions;

public static class AddServicesToProgram
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<ISpecialtyService, SpecialtyService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQrCodeService, QrCodeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ILabService, LabService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IStudyPlanService, StudyPlanService>();
        return services;
    }
    
    public static IServiceCollection AddStandardServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSession();
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        
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
    
    public static IServiceCollection AddDbCustomContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ILabRepository, LabRepository>();
        services.AddScoped<IStudyPlanRepository, StudyPlanRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ISessionSubjectRepository, SessionSubjectRepository>();
        return services;
    }
    
    public static IServiceCollection AddAuthExtensions(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtTokenProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}