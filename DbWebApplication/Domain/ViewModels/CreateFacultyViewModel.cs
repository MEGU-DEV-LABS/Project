using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.ViewModels;

public class CreateFacultyViewModel
{
    [Required]
    public string Name { get; set; }
}