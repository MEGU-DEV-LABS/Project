namespace DbWebApplication.Models;

public class SpecialtyScheduleForWeek
{
    public int Id { get; set; }
    public int SpecialtyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public DateTime StartOfWeek { get; set; }
    public List<ScheduleDay> Days { get; set; } = new();
}

public class ScheduleDay
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; } 
    public int SpecialtyScheduleForWeekId { get; set; }
    public SpecialtyScheduleForWeek SpecialtyScheduleForWeek { get; set; }
    public List<Pair> Pairs { get; set; } = new();
}

public class Pair
{
    public int Id { get; set; }
    public int LessonNumber { get; set; }
    public int SubjectId { get; set; }
    public int RoomId { get; set; }
    public SubjectModel Subject { get; set; }

    public int ScheduleDayId { get; set; }
    public ScheduleDay ScheduleDay { get; set; }
}


