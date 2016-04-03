using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherBot.Luis
{
    public class LuisClient
    {
        public static LuisClient Create()
        {
            string applicationId = ConfigurationManager.AppSettings["LuisApplicationId"];
            string subscriptionKey = ConfigurationManager.AppSettings["LuisSubscriptionKey"];

            return new LuisClient(applicationId, subscriptionKey);
        }

        private const string ApiBaseUrl = "https://api.projectoxford.ai/luis/v1/application?";

        private readonly string _applicationId;
        private readonly string _subscriptionKey;

        public LuisClient(string applicationId, string subscriptionKey)
        {
            _applicationId = applicationId;
            _subscriptionKey = subscriptionKey;
        }

        public async Task<LuisResponse> SendQuery(string query)
        {
            string requestUrl = $"{ApiBaseUrl}id={_applicationId}&subscription-key={_subscriptionKey}&q={query}";

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                HttpResponseMessage response = await client.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();

                LuisResponse result = JsonConvert.DeserializeObject<LuisResponse>(content);
                return result;
            }
        }
    }
}