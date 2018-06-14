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

            config.NameResolver = new AppSettingsResolver();
            
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 4;
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(1);
            
            JobHost host = new JobHost(config);

            host.RunAndBlock();
        }

        public async static Task ProcessAddStudentMessageAsync([QueueTrigger("%queueName%")] AddStudentMessage message, TextWriter logger)
        {
            await Console.Out.WriteAsync($"Starting processing request for user: '{message.FirstName} {message.LastName}'");
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

            await Console.Out.WriteAsync($"Completed processsing request for user: '{message.FirstName} {message.LastName}'");
        }

        public async static Task ProcessUpdateStudentMessageAsync([QueueTrigger("%queueName%")] UpdateStudentMessage message, TextWriter logger)
        {
            await Console.Out.WriteAsync($"Starting processing request for user: '{message.FirstName} {message.LastName}'");
            try
            {
                var repository = new StudentRepository();
                var student = new Student
                {
                    Id = message.Id,
                    FirstName = message.FirstName,
                    LastName = message.LastName
                };

                await repository.AddStudent(student);
            }
            catch (Exception ex)
            {
                var baseException = ex.GetBaseException();
                var messageJson = JsonConvert.SerializeObject(message);
                var error = $"Error Message: '{baseException.Message}'; Stack Trace: '{baseException.StackTrace}'; Queue Message: {messageJson}";
                await logger.WriteAsync(error);
            }

            await Console.Out.WriteAsync($"Completed processsing request for user: '{message.FirstName} {message.LastName}'");
        }
    }
}
