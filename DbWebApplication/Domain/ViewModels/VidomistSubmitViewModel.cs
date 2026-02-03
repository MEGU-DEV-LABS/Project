namespace DbWebApplication.ViewModels;

public class VidomistSubmitViewModel
{
    public int SessionId { get; set; }
    public int SessionSubjectId { get; set; }
    public List<StudentGradeViewModel> StudentGrades { get; set; }
}

public class StudentGradeViewModel
{
    public int StudentId { get; set; }
    public int Grade { get; set; }
}