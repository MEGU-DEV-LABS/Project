namespace DbWebApplication.Models;

public class SpecialtyScheduleForWeek
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    
    public DateTime StartOfWeek { get; set; }
    
    public List<Pair> Monday { get; set; }
    public List<Pair> Tuesday { get; set; }
    public List<Pair> Wednesday { get; set; }
    public List<Pair> Thursday { get; set; }
    public List<Pair> Friday { get; set; }
    public List<Pair> Saturday { get; set; }
    public List<Pair> Sunday { get; set; }
}

public class Pair
{
    public int Id { get; set; }
    public int LessonNumber { get; set; }
    public int SubjectId { get; set; }
    public int RoomId { get; set; }
    public SubjectModel Subject { get; set; }
}
