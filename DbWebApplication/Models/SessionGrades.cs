namespace DbWebApplication.Models;

public class SessionGrades
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SessionId { get; set; } // Refers to Subject in Session
    public float Grade { get; set; }
    public StudentModel Student { get; set; }
    public SessionSubjects SessionSubjects { get; set; }
    
}