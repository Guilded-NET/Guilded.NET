using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Used emote information.
    /// </summary>
    public class EmoteUse : BaseObject
    {
        /// <summary>
        /// ID of the emote.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// Total amount of how much this emoji has been used.
        /// </summary>
        /// <value>Count</value>
        [JsonProperty(Required = Required.Always)]
        public uint Total
        {
            get; set;
        }
    }
}