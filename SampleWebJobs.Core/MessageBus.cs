using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SampleWebJobs.Core
{
    public class MessageBus : IBus
    {
        public Task PublishAsync(IMessage message)
        {
            var queue = GetQueue();
            var qMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message));

            return queue.AddMessageAsync(qMessage);
        }

        private CloudQueue GetQueue()
        {
            var queueName = "studentsMessageQueue";
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var queueClient = storageAccount.CreateCloudQueueClient();

            var queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();

            return queue;
        }
    }
}
