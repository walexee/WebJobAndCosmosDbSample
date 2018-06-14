using SampleWebJobs.Core;
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

        public async Task<Guid> Post(Student student)
        {
            var message = new AddStudentMessage(student.FirstName, student.LastName, student.Age);

            await _bus.PublishAsync(message);

            return message.Id;
        }

        public void Put(int id, [FromBody]Student student)
        {
        }

        public Task Delete(Guid id)
        {
            return _studentRepository.DeleteStudent(id);
        }
    }
}
