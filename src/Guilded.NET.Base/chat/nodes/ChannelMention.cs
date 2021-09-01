using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A mention of a team channel.
    /// </summary>
    /// <seealso cref="MemberMention"/>
    public class ChannelMention : ContainerNode<TextContainer, ChannelMention>
    {
        #region Properties
        /// <summary>
        /// Represents data of this mention.
        /// </summary>
        /// <value>Mention data?</value>
        [JsonIgnore]
        public ChannelMentionData MentionData => Data.Channel;
        #endregion

        #region Constructors
        /// <summary>
        /// A mention of a team channel.
        /// </summary>
        /// <param name="data">Information about the mention</param>
        public ChannelMention(ChannelMentionData data) : base(NodeType.Channel, ElementType.Inline, new TextContainer($"#{data.Name}")) =>
            Data.Channel = data;
        // /// <summary>
        // /// A mention of a team channel.
        // /// </summary>
        // /// <param name="channel">Channel to mention</param>
        // public ChannelMention(TeamChannel channel) : this(new ChannelMentionData(channel)) { }
        #endregion

        #region Additional
        /// <summary>
        /// Converts mention to its string equivalent.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString() =>
            MentionData?.Matcher ?? "#???";
        #endregion
    }
}