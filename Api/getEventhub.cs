using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace api
{
    public static class getEventhub
    {
        private const string ehubNamespaceConnectionString = "Endpoint=sb://jaz204.servicebus.windows.net/;SharedAccessKeyName=all;SharedAccessKey=WA2RsXA/5EJB3hitx8j2heqNELFTQId0HrmtwGg8TEY=";
        private const string eventHubName = "hub23";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=jaz204;AccountKey=fhN48LJ25KEaJ63sBsrMkLcv5cKMF/vTolR/BsLlzy4FIyfZA13qxCgn3KENvO0/Bg6hm59H4sey+AStx4du4A==;EndpointSuffix=core.windows.net";
        private const string blobContainerName = "eventhub-listen";
        static BlobContainerClient storageClient;
        static EventProcessorClient processor;


        [FunctionName("getEventhub")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            processor = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);

            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            await processor.StartProcessingAsync();

            // Wait for 30 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(30));

            // Stop the processing
            await processor.StopProcessingAsync();

            return new OkObjectResult(processor);
        }
        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }
        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }

}
