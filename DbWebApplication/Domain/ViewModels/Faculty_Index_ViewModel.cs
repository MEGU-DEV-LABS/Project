using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class Faculty_Index_ViewModel
{
    public AppUserModel User { get; set; }
    public List<FacultyModel> Faculties { get; set; }
}