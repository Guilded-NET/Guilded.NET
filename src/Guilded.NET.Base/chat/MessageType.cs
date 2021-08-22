using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// The type of the message created or updated.
    /// </summary>
    /// <seealso cref="BaseMessage"/>
    /// <seealso cref="Message"/>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MessageType
    {
        /// <summary>
        /// A normal message written by someone.
        /// </summary>
        Default,
        /// <summary>
        /// A message written by the system(e.g., when channel gets renamed).
        /// </summary>
        System
    }
}