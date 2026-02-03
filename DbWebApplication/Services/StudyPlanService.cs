using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Models;

namespace DbWebApplication.Services;

public class StudyPlanService(IStudyPlanRepository repository, ISubjectRepository subjectRepository): IStudyPlanService
{
    public async Task CreateStudyPlanAsync(int specialtyId,
        List<int> subjectIds)
    {
        await repository.CreateStudyPlanAsync(specialtyId, subjectIds);
    }

    public async Task<StudyPlan> GetLastSemester(int specialtyId)
    {
        return await repository.GetLastSemester(specialtyId);
    }
}