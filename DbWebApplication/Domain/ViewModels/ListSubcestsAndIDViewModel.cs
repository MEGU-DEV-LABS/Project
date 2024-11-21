using DbWebApplication.Enum;

namespace DbWebApplication.ViewModels;

public class ListSubcestsAndIDViewModel
{
    public Faculty Faculty { get; set; }
    public List<DetermineSessionSubjectsViewModel> Subjects { get; set; }

}