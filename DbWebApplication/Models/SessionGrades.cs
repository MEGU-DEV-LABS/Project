namespace DbWebApplication.Models;

public class SessionGrades
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int Grade { get; set; }
    public int StudentId { get; set; }
    public StudentModel Student { get; set; }
    public int SessionSubjectsId { get; set; }
    public SessionSubjects SessionSubject { get; set; }
    
}