using DbWebApplication.Models;
using DbWebApplication.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DbWebApplication.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    public DbSet<SubjectModel> Subjects { get; set; }
    public DbSet<LabModel> LabWorks { get; set; }
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<LabWorkGradeModel> LabWorkGrade { get; set; }
    public DbSet<SessionSubjects> SessionSubjects { get; set; }
    public DbSet<SessionGrades> SessionGrades { get; set; }
    public DbSet<SpecialtyModel> Specialties { get; set; }
    public DbSet<SpecialtyScheduleForWeek> SpecialtyScheduleForWeeks { get; set; }
    public DbSet<FacultyModel> Faculties { get; set; }
    public DbSet<AppUserModel> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Initializing initial admin user
        modelBuilder.Entity<AppUserModel>().HasData(
            new AppUserModel
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                FatherName = "Admin",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("12345Aa@"),
                Role = Enum.Role.Admin
            }
        );
        
        modelBuilder.Entity<AppUserModel>()
            .HasOne(a => a.Student)
            .WithOne(s => s.AppUser)
            .HasForeignKey<StudentModel>(s => s.AppUserId);
        

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}