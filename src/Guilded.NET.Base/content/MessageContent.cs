using System;
using System.Collections.Generic;
using Guilded.NET.Base.Embeds;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// Represents the contents of the message.
    /// </summary>
    /// <remarks>
    /// <para>Defines the contents of the message with which a chat message can be created.</para>
    /// </remarks>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
                ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MessageContent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the message in Markdown format.</para>
        /// </remarks>
        /// <value>Markdown string</value>
        public string? Content { get; set; }
        /// <summary>
        /// The list of embeds in the message.
        /// </summary>
        /// <remarks>
        /// <para>The list of embeds that will be present in the created message.</para>
        /// </remarks>
        /// <value>List of embeds?</value>
        public IList<Embed>? Embeds { get; set; }
        /// <inheritdoc cref="Message.ReplyMessageIds"/>
        public IList<Guid>? ReplyMessageIds { get; set; }
        /// <inheritdoc cref="Message.IsPrivate"/>
        public bool? IsPrivate { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of <see cref="MessageContent"/>.
        /// </summary>
        /// <param name="content">The content of the message</param>
        public MessageContent(string? content) =>
            Content = content;
        /// <summary>
        /// Creates an instance of <see cref="MessageContent"/> with no content.
        /// </summary>
        public MessageContent() { }
        #endregion
    }
}
