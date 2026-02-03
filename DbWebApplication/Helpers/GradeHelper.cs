namespace DbWebApplication.Helpers;

public static class GradeHelper
{
    public static string ECTS_Grade(int points)
    {
        return points switch
        {
            >= 90 => "A",
            >= 82 => "B",
            >= 74 => "C",
            >= 64 => "D",
            >= 60 => "E",
            _ => ""
        };
    }
    
    public static string NationalGrade(int points)
    {
        return points switch
        {
            >= 90 => "5",
            >= 80 => "4.0",
            >= 70 => "3.0",
            _ => "2.0"
        };
    }
}