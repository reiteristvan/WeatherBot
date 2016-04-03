namespace WeatherBot.Bot
{
    public static class MessageType
    {
        public const string Message = "Message";
        public const string Ping = "Ping";
        public const string DeleteUserData = "DeleteUserData";
        public const string BotAddedToConversation = "BotAddedToConversation";
        public const string BotRemovedFromConversation = "BotRemovedFromConversation";
        public const string UserAddedToConversation = "UserAddedToConversation";
        public const string UserRemovedFromConversation = "UserRemovedFromConversation";
        public const string EndOfConversation = "EndOfConversation";
    }
}