using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Enum;

public enum Role
{
    [Display(Name = "Адмін")]
    Admin = 2,
    [Display(Name = "Викладач")]
    Teacher = 3,
    [Display(Name = "Студент")]
    Student = 4
}