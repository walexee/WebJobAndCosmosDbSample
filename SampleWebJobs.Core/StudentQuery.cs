using System;
using System.Collections.Generic;

namespace SampleWebJobs.Core
{
    public class StudentQuery
    {
        public IEnumerable<Guid> UserIds { get; set; }

        public IEnumerable<string> LastNames { get; set; }
    }
}
