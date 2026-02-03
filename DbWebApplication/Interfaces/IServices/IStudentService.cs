using DbWebApplication.Dto;
using DbWebApplication.Models;

namespace DbWebApplication.Interfaces.IServices;

public interface IStudentService
{
    Task<StudentModel> GetStudentByUserId(int applicationUserId);
    Task<StudentModel> GetStudentById(int studentId);
    Task<List<StudentModel>> GetAllStudents();
    Task UpdateStudent(StudentModel student);
    Task<List<SubjectWithGradesDto>> GetSubjectsWithGrades(int studentId);
    Task<List<SessionSubjectWithGradesDto>> GetSessionSubjectsWithGrades(int studentId);
}