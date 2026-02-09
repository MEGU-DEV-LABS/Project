using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.ViewModels;

public class CreateSpecialtyViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int FacultyId { get; set; }
}