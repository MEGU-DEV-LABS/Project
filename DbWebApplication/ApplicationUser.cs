using Microsoft.AspNetCore.Identity;

namespace DbWebApplication;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}