using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.ViewModels;

public class CreateSubjectViewModel
{
    [Required]
    [Display(Name = "Назва")]
    public string SubjectName { get; set; }

    [Required]
    [Display(Name = "Години")]
    public int Hours { get; set; }

    [Required]
    [Display(Name = "Кредити")]
    public int Credits { get; set; }

    [Display(Name = "Зображення")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "Викладач")]
    public int? TeacherId { get; set; }

    public int SpecialtyId { get; set; }
}