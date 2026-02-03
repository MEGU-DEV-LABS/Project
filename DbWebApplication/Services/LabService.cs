using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.Repository;

namespace DbWebApplication.Services;

public class LabService(ILabRepository repository) : ILabService
{
    public async Task<LabModel> GetLabById(int labId)
    {
        return await repository.GetLabByIdAsync(labId);
    }

    public async Task CreateAsync(LabModel lab)
    {
        await repository.CreateLab(lab);
    }

    public async Task UpdateAsync(LabModel lab)
    {
        await repository.UpdateLab(lab);
    }

    public async Task DeleteAsync(int labId)
    {
        await repository.DeleteLab(labId);
    }
    
    public async Task SetLabGradeAsync(int studentId, int labId, int grade)
    {
        await repository.SetLabGradeAsync(studentId, labId, grade);
    }
}