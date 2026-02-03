using DbWebApplication.Enum;

namespace DbWebApplication.ViewModels;

public class RegisterUserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    
    public int? SpecialtyId { get; set; }
}