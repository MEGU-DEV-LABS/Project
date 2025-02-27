using DbWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRoleId = "2276bcf0-f16a-4786-8a26-a3cc41dfd27d";
        var userRoleId = "ce3f1c01-b0a3-47f7-8872-8502cac17779";
        var teacherRoleId = "f4e95dc8-e69f-4e45-a904-ded224a7b9e3";
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = teacherRoleId,
                Name = "Teacher",
                NormalizedName = "TEACHER"
            }
        );
    }
}