using System;

namespace SampleWebJobs.Core
{
    public class UpdateStudentMessage : IMessage
    {
        public UpdateStudentMessage(Guid id, string firstName, string lastName)
        {
            Id = id;
            Created = DateTime.UtcNow;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        //public int Age { get; private set; }
    }
}
