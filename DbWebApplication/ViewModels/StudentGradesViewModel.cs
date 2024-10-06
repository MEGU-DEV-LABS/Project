namespace DbWebApplication.ViewModels;

public class StudentGradesViewModel
{
    public string StudentFullName { get; set; } // Повне ім'я студента
    public string Faculty { get; set; } // Факультет
    public List<SubjectViewModel> Subjects { get; set; } // Список предметів з лабораторними роботами
}