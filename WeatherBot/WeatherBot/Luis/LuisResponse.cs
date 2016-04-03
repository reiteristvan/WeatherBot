using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherBot.Luis
{
    public class LuisResponse
    {
        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; } 

        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; } 
    }

    public sealed class Intent
    {
        [JsonProperty("intent")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        public bool IsNone()
        {
            return Name.Equals("None", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public sealed class Entity
    {
        [JsonProperty("entity")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}