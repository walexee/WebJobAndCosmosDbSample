using Microsoft.Azure.WebJobs;
using System.Configuration;

namespace SampleWebJobs.BackgroundProcessor
{
    public class QueueNameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
