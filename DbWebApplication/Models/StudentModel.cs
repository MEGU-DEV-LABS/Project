using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    [ForeignKey("ApplicationUser")]
    public int AppUserId { get; set; }
    public AppUserModel AppUser { get; set; }
    public int SpecialtyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public ICollection<SessionGrades> SessionGrades { get; set; } = new List<SessionGrades>();
    public ICollection<LabWorkGradeModel> LabWorkGrades { get; set; } = new List<LabWorkGradeModel>();
    public ICollection<SubjectGrade> SubjectGrades { get; set; } = new List<SubjectGrade>();
}