using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbWebApplication.Models;

public class SubjectModel
{
    [Key]
    public int SubjectID { get; set; }
    public string SubjectName { get; set; }
    public byte[] ImageData { get; set; }
    [NotMapped] 
    public string ImageBase64 { get; set; }
    public int Hours { get; set; }
    public int Credits { get; set; }
    
    public int? TeacherId { get; set; }
    public TeacherModel? Teacher { get; set; }
    public ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    public ICollection<LabModel> LabWorks { get; set; } = new List<LabModel>();
    public ICollection<SubjectGrade> SubjectGrades { get; set; } = new List<SubjectGrade>();
    public ICollection<SessionSubjects> SessionSubjects { get; set; } = new List<SessionSubjects>();
}