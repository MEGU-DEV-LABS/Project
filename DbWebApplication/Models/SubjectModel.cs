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
    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    
    public ICollection<LabModel> LabWorks { get; set; } = new List<LabModel>();
}