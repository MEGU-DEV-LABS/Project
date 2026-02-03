using DbWebApplication.Enum;

namespace DbWebApplication.Dto;

public class SessionSubjectDto
{
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public Zalik_Ispit Type { get; set; }
}