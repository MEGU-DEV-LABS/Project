using System.ComponentModel.DataAnnotations.Schema;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class SessionSubjects
{
    public int Id { get; set; }
    public string SubjectName { get; set; }
    public byte[] ImageData { get; set; }
    [NotMapped] 
    public string ImageBase64 { get; set; }
    
    public ICollection<SpecialtyModel> Specialties { get; set; } = new List<SpecialtyModel>();
    public ICollection<SessionGrades> SessionGrades { get; set; } = new List<SessionGrades>();
}
