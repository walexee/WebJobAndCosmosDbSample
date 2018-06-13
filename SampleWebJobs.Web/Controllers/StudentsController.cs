using SampleWebJobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleWebJobs.Web.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController()
        {
            _studentRepository = new StudentRepository();
        }

        public async Task<IEnumerable<Student>> Get()
        {
            return await _studentRepository.GetStudents();
        }

        public Task<Student> Get(Guid id)
        {
            return _studentRepository.GetStudent(id);
        }

        public Task<Guid> Post([FromBody]Student student)
        {
            try
            {
                return _studentRepository.AddStudent(student);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public Task Delete(Guid id)
        {
            return _studentRepository.DeleteStudent(id);
        }
    }
}
