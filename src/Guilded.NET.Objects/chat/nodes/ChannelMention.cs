using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat
{
    using Teams;
    /// <summary>
    /// Mention of a team channel.
    /// </summary>
    public class ChannelMention : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Mention of a team channel.
        /// </summary>
        public ChannelMention() =>
            Type = NodeType.Channel;
        /// <summary>
        /// Gets mention data.
        /// </summary>
        /// <value>Data of the mention</value>
        [JsonIgnore]
        public ChannelMentionData MentionData
        {
            get => GetDataProperty<ChannelMentionData>("channel");
        }
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="data">Mention data</param>
        /// <returns>Mention</returns>
        public static ChannelMention Generate(ChannelMentionData data) =>
            new ChannelMention
            {
                Data = JObject.FromObject(new { channel = data }),
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText($"#{data.Name}")
                }
            };
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="channel">Mention data</param>
        /// <returns>Mention</returns>
        public static ChannelMention Generate(Channel channel) => Generate(ChannelMentionData.Generate(channel));
        /// <summary>
        /// Turns mention to string.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString()
        {
            ChannelMentionData data = MentionData;
            if (data == null) return "#???";
            else return data.Matcher;
        }
    }
}