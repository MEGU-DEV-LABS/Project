using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class LabModel
{
    [Key]
    public int LabWorkID { get; set; }
    
    public string LabWorkName { get; set; }
    public int SubjectID { get; set; }
    public SubjectModel Subject { get; set; }

    public ICollection<LabWorkGradeModel> LabWorkGrades { get; set; } = new List<LabWorkGradeModel>();
}