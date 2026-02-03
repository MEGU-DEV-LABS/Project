using DbWebApplication.Enum;
using DbWebApplication.Models;

namespace DbWebApplication.Dto;

public class SessionSubjectWithGradesDto
{
    public int Id { get; set; }
    public string SubjectName { get; set; }
    public Zalik_Ispit Type { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public int? TeacherId { get; set; }
    public TeacherModel? Teacher { get; set; }
    public int Hours { get; set; }
    public int Credits { get; set; }
    public int? Points { get; set; }
}