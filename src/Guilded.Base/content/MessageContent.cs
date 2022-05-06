using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <inheritdoc cref="Message.Embeds" />
    public IList<Embed>? Embeds { get; set; }
    /// <inheritdoc cref="Message.ReplyMessageIds"/>
    public IList<Guid>? ReplyMessageIds { get; set; }
    /// <inheritdoc cref="Message.IsPrivate"/>
    public bool? IsPrivate { get; set; }
    /// <summary>
    /// Gets whether <see cref="Message.IsReply">the reply</see> or the mention is silent and does not ping anyone.
    /// </summary>
    /// <value>Message is silent</value>
    public bool? IsSilent { get; set; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets whether the message is <see cref="Content">text-only</see> and has no other content.
    /// </summary>
    /// <returns>Message does not have embeds</returns>
    public bool OnlyText => Embeds is null || !Embeds.Any();
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Embed>? embeds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Guid>? replyMessageIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isPrivate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isSilent = null
    ) =>
        (Embeds, ReplyMessageIds, IsPrivate, IsSilent) = (embeds, replyMessageIds, isPrivate, isSilent);
    #endregion
}