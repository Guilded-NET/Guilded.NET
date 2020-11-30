using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat {
    using Teams;
    /// <summary>
    /// Mention of a team channel.
    /// </summary>
    public class ChannelMention: ContainerNode<IMessageObject> {
        public ChannelMention() {
            Type = NodeType.Channel;
            Object = MsgObject.Inline;
        }
        /// <summary>
        /// Gets mention data.
        /// </summary>
        /// <value>Data of the mention</value>
        [JsonIgnore]
        public ChannelMentionData MentionData {
            get {;
                // Get mention data
                object data = GetDataProperty("channel");
                // If it's null, return null
                if(data == null) return null;
                // Check if it's JObject or ChannelMentionData
                if(data is ChannelMentionData mention) return mention;
                else if(data is JObject obj) return obj.ToObject<ChannelMentionData>();
                // If it's neither, return null
                else return null;
            }
        }
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="data">Mention data</param>
        /// <returns>Mention</returns>
        public static ChannelMention Generate(ChannelMentionData data) =>
            new ChannelMention {
                Data = new Dictionary<string, object> {
                    {"channel", data}
                },
                Type = NodeType.Mention,
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText($"#{data.Name}")
                }
            };
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="data">Mention data</param>
        /// <returns>Mention</returns>
        public static ChannelMention Generate(Channel channel) => Generate(ChannelMentionData.Generate(channel));
        /// <summary>
        /// Turns mention to string.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString() {
            ChannelMentionData data = MentionData;
            if(data == null) return "#???";
            else return data.Matcher;
        }
    }
}