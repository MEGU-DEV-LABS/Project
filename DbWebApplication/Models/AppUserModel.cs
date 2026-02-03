using System;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class AppUserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    
    public int? StudentId { get; set; }
    public StudentModel? Student { get; set; }
    public int? TeacherId { get; set; }
    public TeacherModel? TeacherModel { get; set; }
    
    //QR Code fields
    public Guid? QrCodeToken { get; set; }
    public DateTime? TokenDateExpired { get; set; }
}