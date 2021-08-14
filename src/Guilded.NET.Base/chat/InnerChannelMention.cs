using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Teams;
    /// <summary>
    /// The information about <see cref="ChannelMention"/>.
    /// </summary>
    /// <seealso cref="MemberMentionData"/>
    public class ChannelMentionData : BaseMention
    {
        /// <summary>
        /// Information about the channel mentioned.
        /// </summary>
        /// <param name="name">The name of the channel to mention</param>
        /// <param name="channelId">The ID of the channel to mention</param>
        public ChannelMentionData(string name, Guid channelId) =>
            (Name, Matcher, ChannelId) = (name, $"#{name.ToLower()}", channelId);
        /// <summary>
        /// Information about the channel mentioned.
        /// </summary>
        /// <param name="channel">Channel to mention</param>
        public ChannelMentionData(TeamChannel channel) : this(channel.Name, channel.Id) { }
        /// <summary>
        /// ID of the channel mentioned.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
    }
}