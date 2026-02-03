using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface ILabService
{
    Task<LabModel> GetLabById(int labId);
    Task CreateAsync(LabModel lab);
    Task UpdateAsync(LabModel lab);
    Task DeleteAsync(int labId);
    Task SetLabGradeAsync(int studentId, int labId, int grade);
}