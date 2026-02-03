using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class VidomistTypeViewModel
{
    public FacultyModel Faculty { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public Session Session { get; set; }
    public SessionSubjects Subject { get; set; }
    public List<VidomistStudentViewModel> Students { get; set; }
    public TeacherModel Teacher { get; set; }
}

public class VidomistStudentViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public int? Grade { get; set; }
}
