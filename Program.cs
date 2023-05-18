using System;
using HtmlAgilityPack;
using System.Net.Http;
using System.IO;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Send request to weather.com
            string url = "https://weather.com/weather/today/l/04868402b006cebe58ec0fd08536089ac8db8dfb6dc1c6ec98a14d2557ec75ad";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // Get the temperature
            var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--MHmYY']");
            var temperature = temperatureElement.InnerText;

            // Get the conditions
            var conditionElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='CurrentConditions--phraseValue--mZC_p']");
            var condition = conditionElement.InnerText;

            // Get the location 
            var locationElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='CurrentConditions--location--1YWj_']");
            var location = locationElement.InnerText;

            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Scraperdata.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(temperature);
                    writer.WriteLine(condition);
                    writer.WriteLine(location);
                }

                Console.WriteLine("Data has been written to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }
    }
}
