using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherBot.Weather
{
    public sealed class WeatherModel
    {
        [JsonProperty("weather")]
        public List<WeatherDescription> Description { get; set; }

        [JsonProperty("main")]
        public WeatherData Data { get; set; }
    }

    public sealed class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public sealed class WeatherData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }

    public class OpenWeatherMapClient
    {
        public static OpenWeatherMapClient Create()
        {
            string apiKey = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            return new OpenWeatherMapClient(apiKey);
        }

        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather?q=";
        private readonly string _apiKey;

        public OpenWeatherMapClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> CurrentWeatherDescription(string town)
        {
            string requestUrl = BaseUrl + town;
            WeatherModel result = await SendRequest<WeatherModel>(HttpMethod.Get, requestUrl);
            return result.Description[0].Description;
        }

        private async Task<T> SendRequest<T>(HttpMethod httpMethod, string requestUrl)
        {
            requestUrl += "&units=metric&APPID=" + _apiKey;
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, requestUrl);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(content);
                return result;
            }
        }
    }
}