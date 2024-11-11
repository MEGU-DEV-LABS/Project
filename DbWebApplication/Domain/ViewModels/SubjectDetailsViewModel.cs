namespace DbWebApplication.ViewModels;

public class SubjectDetailsViewModel
{
    public string SubjectName { get; set; }
    public int OverallGrade { get; set; }
    public string Picture { get; set; }
    public List<LabaratoryWorkViewModel> LabaratoryWorks { get; set; }
}