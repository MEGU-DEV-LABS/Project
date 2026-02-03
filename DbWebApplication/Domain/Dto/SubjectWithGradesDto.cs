using System.ComponentModel.DataAnnotations.Schema;

namespace DbWebApplication.Dto;

public class SubjectWithGradesDto
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public byte[] ImageData { get; set; }
    [NotMapped] 
    public string ImageBase64 { get; set; }
    public int? SubjectGrade { get; set; }
    public List<LabWithGradeDto> LabWorks { get; set; } = new();
}