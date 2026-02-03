using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface ILabRepository
{
    Task<LabModel> GetLabByIdAsync(int labId);
    Task CreateLab(LabModel model);
    Task UpdateLab(LabModel model);
    Task DeleteLab(int id);
    Task SetLabGradeAsync(int studentId, int labId, int grade);
}