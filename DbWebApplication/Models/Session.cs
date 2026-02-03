using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Models;

public class Session
{
    [Key]
    public int Id { get; set; }
    public int SpecialtyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public int SemesterNumber { get; set; }  

    public ICollection<SessionSubjects> SessionSubjects { get; set; } = new List<SessionSubjects>();
}