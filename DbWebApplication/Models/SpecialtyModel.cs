namespace DbWebApplication.Models;

public class SpecialtyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int FacultyId { get; set; }
    public FacultyModel Faculty { get; set; }
    
    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
    public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    public ICollection<SessionSubjects> SessionSubjects { get; set; } = new List<SessionSubjects>();
    public ICollection<SpecialtyScheduleForWeek> SpecialtyScheduleForWeeks { get; set; } =
        new List<SpecialtyScheduleForWeek>(2);
}