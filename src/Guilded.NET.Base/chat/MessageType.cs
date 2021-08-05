using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// If the message is a normal message or a system message.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MessageType
    {
        /// <summary>
        /// A normal message written by someone.
        /// </summary>
        Default,
        /// <summary>
        /// A message written by system(when channel gets renamed).
        /// </summary>
        System
    }
}