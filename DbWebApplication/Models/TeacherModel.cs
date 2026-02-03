namespace DbWebApplication.Models;

public class TeacherModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public int FacultyId { get; set; }
    public FacultyModel Faculty { get; set; }
    
    public int AppUserId { get; set; }
    public AppUserModel AppUser { get; set; }
    
    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
    public ICollection<SessionSubjects> SessionSubjects { get; set; } = new List<SessionSubjects>();
}