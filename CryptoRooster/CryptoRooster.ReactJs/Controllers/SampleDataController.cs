using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoRooster.ReactJs.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Coin>> GetCoins()
        {
            string url = "https://api.coinmarketcap.com/v1/ticker/?start=0&limit=500";
            HttpClient client = new HttpClient();
            var jsonContent = await client.GetStringAsync(url);
            var coins = JsonConvert.DeserializeObject<List<Coin>>(jsonContent);

            return coins;
        }

        public class Coin
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("price_usd")]
            public string PriceUsd { get; set; }

            [JsonProperty("rank")]
            public string Rank { get; set; }

            [JsonProperty("price_btc")]
            public string PriceBtc { get; set; }

            [JsonProperty("percent_change_24h")]
            public string PercentChange24 { get; set; }
        }
    }
}
