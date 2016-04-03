using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace WeatherBot.Bot
{
    public abstract class MessageHandler
    {
        protected Message Message { get; private set; }

        public async Task<Message> Act(Message message)
        {
            Message = message;

            switch (message.Type)
            {
                case MessageType.Ping:
                    return await Ping();
                case MessageType.BotAddedToConversation:
                    return await BotAddedToConversation();
                case MessageType.BotRemovedFromConversation:
                    return await BotRemovedFromConversation();
                case MessageType.DeleteUserData:
                    return await DeleteUserData();
                case MessageType.EndOfConversation:
                    return await EndOfConversation();
                case MessageType.Message:
                    return await OnMessage();
                case MessageType.UserAddedToConversation:
                    return await UserAddedToConversation();
                case MessageType.UserRemovedFromConversation:
                    return await UserRemovedFromConversation();
                default:
                    throw new ArgumentException("Unknown message type");
            }
        }

        public abstract Task<Message> OnMessage();
        public abstract Task<Message> Ping();
        public abstract Task<Message> DeleteUserData();
        public abstract Task<Message> BotAddedToConversation();
        public abstract Task<Message> BotRemovedFromConversation();
        public abstract Task<Message> UserAddedToConversation();
        public abstract Task<Message> UserRemovedFromConversation();
        public abstract Task<Message> EndOfConversation();
    }
}