using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using SampleWebJobs.Core;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace SampleWebJobs.BackgroundProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new JobHostConfiguration();
            var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

            config.StorageConnectionString = connectionString;
            config.DashboardConnectionString = connectionString;

            config.NameResolver = new QueueNameResolver();
            //FilesConfiguration filesConfig = new FilesConfiguration();
            // When running locally, set this to a valid directory. 
            // Remove this when running in Azure.
            // filesConfig.RootPath = @"c:\temp\files";

            // optional
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 4;
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(1);

            // Add Triggers and Binders for Files and Timer Trigger.
            //config.UseFiles(filesConfig);
            //config.UseUseTimers();
            
            JobHost host = new JobHost(config);

            host.RunAndBlock();
        }

        public async static Task ProcessQueueMessageAsync([QueueTrigger("studentsmessagequeue")] AddStudentMessage message, TextWriter logger)
        {
            try
            {
                var repository = new StudentRepository();
                var student = new Student
                {
                    Id = message.Id,
                    Age = message.Age,
                    FirstName = message.FirstName,
                    LastName = message.LastName
                };

                await repository.AddStudent(student);
            }
            catch(Exception ex)
            {
                var baseException = ex.GetBaseException();
                var messageJson = JsonConvert.SerializeObject(message);
                var error = $"Error Message: '{baseException.Message}'; Stack Trace: '{baseException.StackTrace}'; Queue Message: {messageJson}";
                await logger.WriteAsync(error);
            }
            await logger.WriteLineAsync(message.FirstName);
        }
    }
}
