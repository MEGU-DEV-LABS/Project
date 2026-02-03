using DbWebApplication.Dto;
using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class StudentSubjectsViewModel
{
    public StudentModel Student { get; set; }
    public List<SubjectWithGradesDto> Subjects { get; set; }
}