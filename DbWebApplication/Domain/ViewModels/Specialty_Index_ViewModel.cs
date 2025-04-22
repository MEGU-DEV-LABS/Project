using DbWebApplication.Models;

namespace DbWebApplication.ViewModels;

public class Specialty_Index_ViewModel
{
    public AppUserModel User { get; set; }
    
    public List<SpecialtyModel> Specialties { get; set; }
}