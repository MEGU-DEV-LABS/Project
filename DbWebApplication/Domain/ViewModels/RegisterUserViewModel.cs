using System.ComponentModel.DataAnnotations;
using DbWebApplication.Enum;

namespace DbWebApplication.ViewModels;

public class RegisterUserViewModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string FatherName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    public Role Role { get; set; }
    
    public int? SpecialtyId { get; set; }
}