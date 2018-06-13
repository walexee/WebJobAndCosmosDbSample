using System;

namespace SampleWebJobs.Core
{
    public interface IMessage
    {
        Guid Id { get; }

        DateTime Created { get; }
    }
}
