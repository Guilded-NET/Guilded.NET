using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Guilded.Base.Embeds;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the complete contents of a message.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageType" />
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
            ItemNullValueHandling = NullValueHandling.Ignore)]
public class MessageContent : BaseObject
{
    #region Fields
    private string? _content;
    private IList<Embed>? _embeds;
    private IList<Guid>? _replyMessageIds;
    #endregion

    #region JSON properties
    /// <inheritdoc cref="Message.Content" />
    public string? Content
    {
        get => _content;
        set
        {
            if (value is not null && value?.Length > Message.TextLimit)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Contents of the message exceed 4000 character limit");

            _content = value;
        }
    }
    /// <inheritdoc cref="Message.Embeds" />
    public IList<Embed>? Embeds
    {
        get => _embeds;
        set
        {
            if (value is not null && value?.Count > Message.EmbedLimit)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given embed list exceeds the 1 embed per message limit");

            _embeds = value;
        }
    }
    /// <inheritdoc cref="Message.ReplyMessageIds" />
    public IList<Guid>? ReplyMessageIds
    {
        get => _replyMessageIds;
        set
        {
            if (value is not null && value?.Count > Message.ReplyLimit)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given reply list exceeds 5 replies per message limit");

            _replyMessageIds = value;
        }
    }
    /// <inheritdoc cref="Message.IsPrivate" />
    public bool? IsPrivate { get; set; }
    /// <summary>
    /// Gets whether <see cref="Message.IsReply">the reply</see> or the mention is silent and does not ping anyone.
    /// </summary>
    /// <value><see cref="Message" /> is silent</value>
    /// <seealso cref="MessageContent" />
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="Content" />
    /// <seealso cref="ReplyMessageIds" />
    /// <seealso cref="Embeds" />
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
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="content">The content of the message</param>
    public MessageContent(string? content) =>
        Content = content;
    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> with no content.
    /// </summary>
    public MessageContent() { }
    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> with no content.
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