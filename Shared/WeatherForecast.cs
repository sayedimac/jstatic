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
    }
}
