using System;
using System.Collections.Generic;
using Guilded.Base.Embeds;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the complete contents of a message.
/// </summary>
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
            ItemNullValueHandling = NullValueHandling.Ignore)]
public class MessageContent : BaseObject
{
    #region JSON properties
    /// <inheritdoc cref="Message.Content" />
    public string? Content { get; set; }
    /// <summary>
    /// The list of embeds in the message.
    /// </summary>
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
    /// <summary>
    /// Creates an instance of <see cref="MessageContent"/> with no content.
    /// </summary>
    [JsonConstructor]
    public MessageContent(
        [JsonProperty]
        IList<Embed>? embeds,

        [JsonProperty]
        IList<Guid>? replyMessageIds,

        [JsonProperty]
        bool? isPrivate
    ) =>
        (Embeds, ReplyMessageIds, IsPrivate) = (embeds, replyMessageIds, isPrivate);
    #endregion
}