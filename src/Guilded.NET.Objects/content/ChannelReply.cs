using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    /// <summary>
    /// A reply to a forum post, media, a document or an announcement.
    /// </summary>
    public class ChannelReply : Reply
    {
        /// <summary>
        /// A reply to a forum post, media, a document or an announcement.
        /// </summary>
        protected ChannelReply() =>
            Reactions = new List<Reaction>();
        /// <summary>
        /// Reactions in this reply.
        /// </summary>
        /// <value>Reactions</value>
        [JsonProperty("reactions", Required = Required.AllowNull)]
        public IList<Reaction> Reactions
        {
            get; set;
        }
        /// <summary>
        /// A team this channel reply is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
    }
}