using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SampleWebJobs.Core
{
    public class StudentRepository : IStudentRepository
    {
        public async Task<Guid> AddStudent(Student student)
        {
            student.Id = Guid.NewGuid();

            var collection = GetCollection();

            await collection.InsertOneAsync(student);

            return student.Id;
        }

        public async Task<Student> GetStudent(Guid studentId)
        {
            var collection = GetCollection();
            var student = await collection.FindAsync(x => x.Id == studentId);
           
            return student.FirstOrDefault();
        }

        public async Task<IList<Student>> GetStudents()
        {
            var collection = GetCollection();
            var students = await collection.FindAsync(FilterDefinition<Student>.Empty);

            return students.ToList();
        }

        public Task DeleteStudent(Guid studentId)
        {
            var collection = GetCollection();

            return collection.FindOneAndDeleteAsync(x => x.Id == studentId);
        }

        private MongoClient GetDbClient()
        {
            var connectionString = ConfigurationManager.AppSettings["dbConnectionString"];
            var connectionUrl = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(connectionUrl);

            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12,
            };

            return new MongoClient(settings);
        }

        private IMongoCollection<Student> GetCollection()
        {
            var dbName = ConfigurationManager.AppSettings["dbName"];
            var collectionName = ConfigurationManager.AppSettings["collectionName"];
            var client = GetDbClient();

            var database = client.GetDatabase(dbName);

            return database.GetCollection<Student>(collectionName);
        }
    }
}
