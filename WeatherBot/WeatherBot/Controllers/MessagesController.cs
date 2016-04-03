using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using WeatherBot.Bot;

namespace WeatherBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly WeatherBotMessageHandler _messageHandler;

        public MessagesController()
        {
            _messageHandler = new WeatherBotMessageHandler();
        }

        /// <summary>
        /// POST: api/Messages
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            return await _messageHandler.Act(message);
        }
    }
}