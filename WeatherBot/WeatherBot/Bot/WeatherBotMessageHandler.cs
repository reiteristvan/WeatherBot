using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using WeatherBot.Luis;
using WeatherBot.Weather;

namespace WeatherBot.Bot
{

    public class WeatherBotMessageHandler : MessageHandler
    {
        private const string Hi = "Hi";
        private const string Sad = "Unfortunately I cannot understand you :(";
        private const string Sample = "You can ask me the weather in a town.Ex.: How is the weather in Budapest?";

        private const string WeatherInTownIntent = "WeatherInTown";

        private const string TownEntity = "town";

        private readonly LuisClient _luisClient;

        public WeatherBotMessageHandler()
        {
            _luisClient = LuisClient.Create();
        }

        public override async Task<Message> OnMessage()
        {
            if (Message.Text.Equals(Hi, StringComparison.InvariantCultureIgnoreCase))
            {
                return Message.CreateReplyMessage(Sample);
            }

            LuisResponse luisResponse = await _luisClient.SendQuery(Message.Text);

            string result = await ParseLuisResponse(luisResponse);

            return Message.CreateReplyMessage(result);
        }

        public override Task<Message> Ping()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> DeleteUserData()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> BotAddedToConversation()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> BotRemovedFromConversation()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> UserAddedToConversation()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> UserRemovedFromConversation()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> EndOfConversation()
        {
            throw new NotImplementedException();
        }

        private async Task<string> ParseLuisResponse(LuisResponse luisResponse)
        {
            Intent winner = luisResponse.Winner();

            if (winner == null || winner.IsNone())
            {
                return Sad;
            }

            if (winner.Name.Equals(WeatherInTownIntent))
            {
                Entity townEntity = luisResponse.Entities
                    .First(e => e.Type.Equals(TownEntity, StringComparison.InvariantCultureIgnoreCase));

                OpenWeatherMapClient client = OpenWeatherMapClient.Create();
                return await client.CurrentWeatherDescription(townEntity.Value);
            }

            return Sad;
        }
    }
}