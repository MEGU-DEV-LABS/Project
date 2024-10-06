using DbWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Data;

public class AppDbContext : DbContext
{
    

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<SubjectModel> Subjects { get; set; }
    public DbSet<LabModel> LabWorks { get; set; }
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<LabWorkGradeModel> LabWorkGrade { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentModel>()
            .HasMany(s => s.Subjects)
            .WithMany(sub => sub.Students)
            .UsingEntity(j => j.ToTable("StudentSubject"));
    }
}