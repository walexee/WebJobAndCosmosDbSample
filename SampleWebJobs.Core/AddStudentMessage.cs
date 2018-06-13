using System;

namespace SampleWebJobs.Core
{
    public class AddStudentMessage : IMessage
    {
        public AddStudentMessage(string firstName, string lastName, int age)
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Age { get; private set; }
    }
}
