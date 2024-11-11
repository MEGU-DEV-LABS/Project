using DbWebApplication.Enum;

namespace DbWebApplication.ViewModels;

public class EnrollStudentViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public Faculty Faculty { get; set; }
    public string SubjectName { get; set; }
    
    public List<string> Subjects { get; set; } = new List<string>();
}