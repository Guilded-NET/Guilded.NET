using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects
{
    /// <summary>
    /// Game's information in a profile.
    /// </summary>
    public class GameAlias : BaseObject
    {
        /// <summary>
        /// Name of this user in that game.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Discriminator (#0000) of this user's tag.
        /// </summary>
        /// <value>Discriminator</value>
        [JsonProperty("discriminator", Required = Required.AllowNull)]
        public string Discriminator
        {
            get; set;
        }
        /// <summary>
        /// ID of this game.
        /// </summary>
        /// <value>Game ID</value>
        [JsonProperty("gameId", Required = Required.Always)]
        public uint GameId
        {
            get; set;
        }
        /// <summary>
        /// Additional information related to this game.
        /// </summary>
        /// <value>Additional info</value>
        [JsonProperty("additionalInfo", Required = Required.Always)]
        public IDictionary<string, string> AdditionalInfo
        {
            get; set;
        }
    }
}