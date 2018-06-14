using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebJobs.Core
{
    public interface IStudentRepository
    {
        Task<Guid> AddStudent(Student student);
        Task DeleteStudent(Guid studentId);
        Task<Student> GetStudent(Guid studentId);
        Task<IList<Student>> GetStudents();
        Task UpdateStudent(Student student);
    }
}