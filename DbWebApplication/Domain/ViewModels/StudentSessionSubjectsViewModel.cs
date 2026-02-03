using DbWebApplication.Dto;
using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class StudentSessionSubjectsViewModel
{
    public StudentModel Student { get; set; }
    public List<SessionSubjectWithGradesDto> Subjects { get; set; }
}