namespace DbWebApplication.Dto;

public class RegisterStudentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public int SpecialtyId { get; set; }
}
