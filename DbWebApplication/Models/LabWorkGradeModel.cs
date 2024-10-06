using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class LabWorkGradeModel
{
    [Key]
    public int LabWorkGradeID { get; set; }
    
    public int StudentID { get; set; } 

    public int LabWorkID { get; set; }
    
    public float GradeValue { get; set; }

    public StudentModel Student { get; set; }
    
    public LabModel LabWork { get; set; }
}