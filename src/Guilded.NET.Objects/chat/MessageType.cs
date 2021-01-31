using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// If the message is a normal message or a system message.
    /// </summary>
    public enum MessageType {
        /// <summary>
        /// A normal message written by someone.
        /// </summary>
        [EnumMember(Value = "default")]
        Default,
        /// <summary>
        /// A message written by system(when channel gets renamed).
        /// </summary>
        [EnumMember(Value = "system")]
        System
    }
}