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
    public DbSet<SubjectGrade> SubjectsGrades { get; set; }
    public DbSet<StudyPlan> StudyPlans { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<TeacherModel> Teachers { get; set; }

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
        
        modelBuilder.Entity<AppUserModel>()
            .HasOne(a => a.Student)
            .WithOne(s => s.AppUser)
            .HasForeignKey<StudentModel>(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SubjectGrade>()
            .HasOne(sg => sg.Student)
            .WithMany(s => s.SubjectGrades)
            .HasForeignKey(sg => sg.StudentId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<SubjectGrade>()
            .HasOne(sg => sg.Subject)
            .WithMany()
            .HasForeignKey(sg => sg.SubjectId)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<LabWorkGradeModel>()
            .HasOne(g => g.Student)
            .WithMany(s => s.LabWorkGrades)
            .HasForeignKey(g => g.StudentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LabWorkGradeModel>()
            .HasOne(g => g.LabWork)
            .WithMany(l => l.LabWorkGrades)
            .HasForeignKey(g => g.LabWorkID)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SessionSubjects>()
            .HasOne(ss => ss.Session)
            .WithMany(s => s.SessionSubjects)
            .HasForeignKey(ss => ss.SessionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<StudyPlan>()
            .HasOne(sp => sp.Specialty)
            .WithMany(s => s.StudyPlans)
            .HasForeignKey(sp => sp.SpecialtyId)
            .OnDelete(DeleteBehavior.Restrict);




        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}