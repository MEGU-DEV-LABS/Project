using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IStudyPlanService
{
    Task CreateStudyPlanAsync(int specialtyId,
        List<int> subjectIds);

    Task<StudyPlan> GetLastSemester(int specialtyId);
}