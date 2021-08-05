using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Chat;
    /// <summary>
    /// A list item in a list channel.
    /// </summary>
    public class ListItem : ChannelContent<Guid>
    {
        /// <summary>
        /// The content of this item's message.
        /// </summary>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Message
        {
            get; set;
        }
        /// <summary>
        /// The content of this item's note.
        /// </summary>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Note
        {
            get; set;
        }
    }
}