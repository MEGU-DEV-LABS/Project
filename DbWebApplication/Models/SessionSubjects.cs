using DbWebApplication.Enum;

namespace DbWebApplication.Models;

public class SessionSubjects
{
    public int Id { get; set; }
    public Faculty Faculty { get; set; }
    public string Subject { get; set; }
    
    
}
