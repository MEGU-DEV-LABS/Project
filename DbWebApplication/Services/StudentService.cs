using DbWebApplication.Data;
using DbWebApplication.Dto;
using DbWebApplication.Interfaces;
using DbWebApplication.Interfaces.IRepositories;
using DbWebApplication.Interfaces.IServices;
using DbWebApplication.Models;
using DbWebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DbWebApplication.Services;

public class StudentService(IStudentRepository studentRepository,
    ISubjectRepository subjectRepository, IUserRepository userRepository) : IStudentService
{
    public async Task<StudentModel> GetStudentByUserId(int applicationUserId)
    {
        var student = await studentRepository.GetByAppUserIdAsync(applicationUserId);
        
        if (student == null)
        {
            throw new NullReferenceException("Student not found.");
        }
            
        return student;
    }
    
    public async Task<StudentModel> GetStudentById(int studentId)
    {
        var user = await userRepository.GetUserByIdAsync(studentId);
        if (user == null)
        {
            throw new NullReferenceException("Student not found.");
        }
        var student = user.Student;
        return student;
    }
    
    public async Task<List<StudentModel>> GetAllStudents()
    {
        var students = await studentRepository.GetAllAsync();
        if (students == null || !students.Any())
        {
            throw new NullReferenceException("No students found.");
        }
        return students;
    }
    
    public async Task UpdateStudent(StudentModel student)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");
        }
        
        var existingStudent = await studentRepository.GetByIdAsync(student.Id);
        if (existingStudent == null)
        {
            throw new NullReferenceException("Student not found.");
        }
        
        await studentRepository.UpdateAsync(student);
    }
    
    public async Task<List<SubjectWithGradesDto>> GetSubjectsWithGrades(int studentId)
    {
        return await studentRepository.GetSubjectsWithLabGradesAsync(studentId);

    }
    
    public async Task<List<SessionSubjectWithGradesDto>> GetSessionSubjectsWithGrades(int studentId)
    {
        return await studentRepository.GetSessionSubjectsWithGradesAsync(studentId);
    }
}