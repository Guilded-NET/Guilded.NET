using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A list item in a list channel.
    /// </summary>
    /// <seealso cref="ForumThread"/>
    public class ListItem : ChannelContent<Guid>
    {
        /// <summary>
        /// The content of this item's message.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the list item in Markdown.</para>
        /// </remarks>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Message
        {
            get; set;
        }
        /// <summary>
        /// The content of this item's note.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the list item's note in Markdown.</para>
        /// </remarks>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Note
        {
            get; set;
        }
    }
}