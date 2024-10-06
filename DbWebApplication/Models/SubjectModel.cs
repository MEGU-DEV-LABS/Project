using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class SubjectModel
{
    [Key]
    public int SubjectID { get; set; }
    
    public string SubjectName { get; set; }
    
    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();

    // Лабораторні роботи
    public ICollection<LabModel> LabWorks { get; set; } = new List<LabModel>();
}