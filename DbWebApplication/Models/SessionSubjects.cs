using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class SessionSubjects
{
    public int Id { get; set; }
    public string SubjectName { get; set; }
    public Zalik_Ispit Type { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public int? TeacherId { get; set; }
    public TeacherModel? Teacher { get; set; }
    
    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; }
    
    public int Hours { get; set; }
    public int Credits { get; set; }
    
    public ICollection<SessionGrades> SessionGrades { get; set; } = new List<SessionGrades>();
}
