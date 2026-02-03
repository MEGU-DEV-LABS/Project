using System.ComponentModel.DataAnnotations;

namespace DbWebApplication.ViewModels.Requests;

public class LoginRequests
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}