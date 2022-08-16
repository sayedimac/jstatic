using System;

namespace JStatic.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
    public class BlobObject
    {
        public BlobObject(string name, string url, string customer)
        {
            blobName = name;
            blobUrl = url;
            blobCustomer = customer;
        }
        public BlobObject(string name, string url)
        {
            blobName = name;
            blobUrl = url;
        }
        public BlobObject()
        {

        }
        public string blobName { get; set; }
        public string blobUrl { get; set; }
        public string blobCustomer { get; set; }
    }
    public class EventObject
    {
        public EventObject(string name, string url, string customer)
        {
            eventName = name;
            eventUrl = url;
            eventCustomer = customer;
        }
        public EventObject(string name, string url)
        {
            eventName = name;
            eventUrl = url;
        }
        public EventObject()
        {

        }
        public string eventName { get; set; }
        public string eventUrl { get; set; }
        public string eventCustomer { get; set; }
    }
}
