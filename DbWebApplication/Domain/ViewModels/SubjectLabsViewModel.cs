using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class SubjectLabsViewModel
{
    public SubjectModel Subject { get; set; }
    public List<LabViewModel> Labs { get; set; }
}

public class LabViewModel
{
    public LabModel Lab { get; set; }
    public List<StudentLabGradeViewModel> Students { get; set; }
}

public class StudentLabGradeViewModel
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public int? GradeValue { get; set; }
}