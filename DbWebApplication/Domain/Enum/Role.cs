using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.Enum;

public enum Role
{
    [Display(Name = "Юзер")]
    User = 1,
    [Display(Name = "Адмін")]
    Admin = 2,
    [Display(Name = "Викладач")]
    Teacher = 3
}