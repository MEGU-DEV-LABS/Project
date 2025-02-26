namespace DbWebApplication.Models;

public class Faculty
{
    public int Id { get; set; }
    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    public ICollection<SessionSubjects> SessionSubjects { get; set; } = new List<SessionSubjects>();
}