using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Write("Enter city: ");
                string city = Console.ReadLine();
                if (city.ToLower() != "exit")
                {
                    string link = "http://api.openweathermap.org/data/2.5/forecast/city?q=" + city + "&APPID=c18b3cbfeda8ece0f998e49636a665db";

                    using (WebClient client = new WebClient())
                    {
                        link = client.DownloadString(link);
                    }

                    JsonString mainForeCast = JsonConvert.DeserializeObject<JsonString>(link);

                    Console.WriteLine("City: {0}", mainForeCast.city.name);
                    foreach (var item in mainForeCast.forecast)
                    {
                        Console.WriteLine("Date: {0}", item.date.ToString());
                        Console.WriteLine("Temperature: {0:#} C", item.main.temp - 273.15);
                        Console.WriteLine("Main: {0}\t{1}", item.weather[0].main, item.weather[0].description);
                    }
                }
                else
                    break;
            }
            while (true);
        }
    }

    public class JsonString
    {
        [JsonProperty("city")]
        public City city { get; set; }

        [JsonProperty("list")]
        public Forecast[] forecast { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("dt")]
        public string dt { get; set; }

        [JsonProperty("main")]
        public Main main { get; set; }

        [JsonProperty("weather")]
        public Weather[] weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds clouds { get; set; }

        [JsonProperty("wind")]
        public Wind wind { get; set; }

        [JsonProperty("rain")]
        public Rain rain { get; set; }

        [JsonProperty("sys")]
        public Sys sys { get; set; }

        [JsonProperty("dt_txt")]
        public DateTime date { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double temp { get; set; }

        [JsonProperty("temp_min")]
        public double temp_min { get; set; }

        [JsonProperty("temp_max")]
        public double temp_max { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("main")]
        public string main { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("icon")]
        public string icon { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public string all { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double speed { get; set; }

        [JsonProperty("deg")]
        public double deg { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double _3h { get; set; }
    }

    public class Sys
    {
        [JsonProperty("pod")]
        public string pod { get; set; }
    }
}