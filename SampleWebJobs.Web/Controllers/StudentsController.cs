using SampleWebJobs.Core;
using SampleWebJobs.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleWebJobs.Web.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IBus _bus;

        public StudentsController()
        {
            _studentRepository = new StudentRepository();
            _bus = new MessageBus();
        }

        public async Task<IEnumerable<Student>> Get()
        {
            return await _studentRepository.GetStudents();
        }

        public Task<Student> Get(Guid id)
        {
            return _studentRepository.GetStudent(id);
        }

        [HttpPost, Route("api/students/getall")]
        public Task<IList<StudentSummary>> GetAll([FromBody]StudentQuery query)
        {
            return _studentRepository.SearchStudent(query);
        }

        public async Task<Guid> Post(Student student)
        {
            var message = new AddStudentMessage(student.FirstName, student.LastName, student.Age);
            await _bus.PublishAsync(message);

            return message.Id;
        }

        [Route("api/students/save")]
        public async Task<Guid> DirectPost(Student student)
        {
            await _studentRepository.AddStudent(student);

            return student.Id;
        }

        public Task Put(Guid id, SaveStudentViewModel model)
        {
            var message = new UpdateStudentMessage(id, model.FirstName, model.LastName);

            return _bus.PublishAsync(message);
        }

        public Task Delete(Guid id)
        {
            return _studentRepository.DeleteStudent(id);
        }
    }
}
