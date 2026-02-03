using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class StudyPlan
{
    [Key]
    public int Id { get; set; }
    public int SpecialtyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public int SemesterNumber { get; set; }  

    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
}