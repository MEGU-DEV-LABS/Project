using System.ComponentModel.DataAnnotations;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public Faculty Faculty { get; set; }
    
    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
}