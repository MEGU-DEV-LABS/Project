using System;
using System.Collections.Generic;

namespace DbWebApplication.Models;

public class SpecialtyScheduleForWeek
{
    public int Id { get; set; }
    public int SpecialtyId { get; set; }
    public SpecialtyModel Specialty { get; set; }
    public DateTime StartOfWeek { get; set; }
    public List<ScheduleDay> Days { get; set; } = new();
}