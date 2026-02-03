using System.Collections.Generic;

namespace DbWebApplication.Models;

public class FacultyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<SpecialtyModel> Specialties { get; set; }
}