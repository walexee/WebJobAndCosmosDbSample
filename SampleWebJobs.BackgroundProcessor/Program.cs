using Microsoft.Azure.WebJobs;
using SampleWebJobs.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebJobs.BackgroundProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            JobHostConfiguration config = new JobHostConfiguration();
            //FilesConfiguration filesConfig = new FilesConfiguration();
            // When running locally, set this to a valid directory. 
            // Remove this when running in Azure.
            // filesConfig.RootPath = @"c:\temp\files";

            // optional
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(1);

            // Add Triggers and Binders for Files and Timer Trigger.
            //config.UseFiles(filesConfig);
            //config.UseUseTimers();

            JobHost host = new JobHost(config);
            host.RunAndBlock();
        }

        public async static Task ProcessQueueMessageAsync([QueueTrigger("studentsMessageQueue")] AddStudentMessage addStudentMessage, TextWriter logger)
        {
            await logger.WriteLineAsync(addStudentMessage.FirstName);
        }
    }
}
