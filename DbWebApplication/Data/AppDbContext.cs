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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentModel>()
            .HasMany(s => s.Subjects)
            .WithMany(sub => sub.Students)
            .UsingEntity(j => j.ToTable("StudentSubject"));
        
        base.OnModelCreating(modelBuilder);

        var adminRoleId = "2276bcf0-f16a-4786-8a26-a3cc41dfd27d";
        var userRoleId = "ce3f1c01-b0a3-47f7-8872-8502cac17779";
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            },
            new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER".ToUpper()
            }
        );

        modelBuilder.Entity<StudentModel>()
            .HasOne(s => s.ApplicationUser)
            .WithMany()
            .HasForeignKey(s => s.ApplicationUserId)
            .IsRequired();
        
        modelBuilder.Entity<SessionGrades>()
            .HasOne(sg => sg.Student)
            .WithMany(s => s.SessionGrades)
            .HasForeignKey(sg => sg.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // SessionGrades to Session relationship (Many-to-One)
        modelBuilder.Entity<SessionGrades>()
            .HasOne(sg => sg.SessionSubjects)
            .WithMany()
            .HasForeignKey(sg => sg.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}