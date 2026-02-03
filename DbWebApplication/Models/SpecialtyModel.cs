using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class SpecialtyModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Вкажіть назву спеціальності")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Не вказано номер факультету")]
    public int FacultyId { get; set; }
    public FacultyModel Faculty { get; set; }
    public List<StudentModel> Students { get; set; } = new List<StudentModel>();
    public ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<SpecialtyScheduleForWeek> SpecialtyScheduleForWeeks { get; set; } =
        new List<SpecialtyScheduleForWeek>(2);
}