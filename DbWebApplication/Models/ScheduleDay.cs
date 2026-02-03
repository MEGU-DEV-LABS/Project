using System;
using System.Collections.Generic;

namespace DbWebApplication.Models;

public class ScheduleDay
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; } 
    public int SpecialtyScheduleForWeekId { get; set; }
    public SpecialtyScheduleForWeek SpecialtyScheduleForWeek { get; set; }
    public List<Pair> Pairs { get; set; } = new();
}