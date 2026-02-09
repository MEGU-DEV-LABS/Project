using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IStudyPlanRepository
{
    Task CreateStudyPlanAsync(int specialtyId,
        List<int> subjectIds);

    Task<StudyPlan> GetLastSemester(int specialtyId);
    Task<bool> StudyPlanLasNew(int specialtyId);
}