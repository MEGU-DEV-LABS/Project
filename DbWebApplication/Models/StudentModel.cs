using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class StudentModel
{

    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    [ForeignKey("ApplicationUser")] // ForeignKey для акаунта
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } // Навігаційна властивість
    public Faculty Faculty { get; set; }
    public Guid? QrCodeToken { get; set; }
    public DateTime? TokenDateExpired { get; set; }
    
    public ICollection<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
}