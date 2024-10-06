namespace DbWebApplication.ViewModels;

public class SubjectViewModel
{
    public string SubjectName { get; set; } // Назва предмета
    public List<LabWorkViewModel> LabWorks { get; set; } // Лабораторні роботи
}