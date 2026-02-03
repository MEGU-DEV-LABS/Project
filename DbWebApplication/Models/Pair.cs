namespace DbWebApplication.Models;

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