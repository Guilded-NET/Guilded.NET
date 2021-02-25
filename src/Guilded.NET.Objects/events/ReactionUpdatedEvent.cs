using System;

using Guilded.NET.Objects.Teams;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Events
{
    /// <summary>
    /// When reaction is added to or remove from the message.
    /// </summary>
    public class ReactionUpdatedEvent : CommonEvent
    {
        /// <summary>
        /// Type of the channel this reaction is in.
        /// </summary>
        /// <value>Channel type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType
        {
            get; set;
        }
        /// <summary>
        /// Reaction's emote.
        /// </summary>
        /// <value>Reaction emote</value>
        [JsonProperty("reaction", Required = Required.Always)]
        public ChatEmote Emote
        {
            get; set;
        }
        /// <summary>
        /// Message object where reaction was updated. Only has property `id`
        /// </summary>
        /// <value>Object(id: Guid)</value>
        [JsonProperty("message", Required = Required.Always)]
        public JObject MessageObject
        {
            get; set;
        }
        /// <summary>
        /// Gets ID of the message where reactions were updated.
        /// </summary>
        /// <value>Message ID</value>
        public Guid MessageId
        {
            get => MessageObject["id"].Value<Guid>();
        }
        /// <summary>
        /// Whether or not this reaction was removed.
        /// </summary>
        /// <value>Removed</value>
        [JsonIgnore]
        public bool WasRemoved
        {
            get => EventType == "ChatMessageReactionDeleted";
        }
        /// <summary>
        /// Whether or not this reaction was added.
        /// </summary>
        /// <value>Added</value>
        [JsonIgnore]
        public bool WasAdded
        {
            get => EventType == "ChatMessageReactionCreated";
        }
    }
}