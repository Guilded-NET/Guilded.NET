using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Objects.Users
{
    /// <summary>
    /// A status of the game user is playing.
    /// </summary>
    public class GameStatus : ClientObject
    {
        /// <summary>
        /// ID of the status.
        /// </summary>
        /// <value>Status ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// ID of the game user is playing.
        /// </summary>
        /// <value>Game ID</value>
        [JsonProperty("gameId", Required = Required.Always)]
        public uint GameId
        {
            get; set;
        }
        /// <summary>
        /// A type of game presence.
        /// </summary>
        /// <value>"gamepresence"</value>
        [JsonProperty("type", Required = Required.Always)]
        public string Type
        {
            get; set;
        }
        /// <summary>
        /// When the game started.
        /// </summary>
        /// <value>Game started at</value>
        [JsonProperty("startedAt", Required = Required.Always)]
        public DateTime StartedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the Guilded client this user is using.
        /// </summary>
        /// <value>Guilded client ID</value>
        [JsonProperty("guildedClientId", Required = Required.Always)]
        public Guid GuildedClientId
        {
            get; set;
        }
    }
}