using DbWebApplication.Dto;
using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class StudentSessionSubjectsViewModel
{
    public StudentModel Student { get; set; }
    public List<SemesterSubjectsViewModel> Semesters { get; set; }
}

public class SemesterSubjectsViewModel
{
    public int SemesterNumber { get; set; }
    public List<SessionSubjectWithGradesDto> ZalikSubjects { get; set; } = new();
    public List<SessionSubjectWithGradesDto> ExamSubjects { get; set; } = new();
}
