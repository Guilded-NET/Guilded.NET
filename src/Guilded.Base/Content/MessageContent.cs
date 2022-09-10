using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Embeds;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the complete contents of a message.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="MessageType" />
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
            ItemNullValueHandling = NullValueHandling.Ignore)]
public class MessageContent
{
    #region Fields
    private string? _content, _username;

    private IList<Embed>? _embeds;

    private IList<Guid>? _replyMessageIds;
    #endregion

    #region Properties
    /// <inheritdoc cref="Message.IsPrivate" />
    public bool? IsPrivate { get; set; }

    /// <inheritdoc cref="Message.IsSilent" />
    public bool? IsSilent { get; set; }

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

    /// <summary>
    /// Gets the displayed <see cref="UserSummary.Name">name</see> of the webhook.
    /// </summary>
    /// <value>The displayed <see cref="Servers.Webhook">webhook</see> <see cref="UserSummary.Name">name</see></value>
    /// <seealso cref="Avatar" />
    public string? Username
    {
        get => _username;
        set
        {
            if (value is not null && value?.Length > Webhook.NameLimit)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given message username override is over the 128 character limit");

            _username = value;
        }
    }

    /// <summary>
    /// Gets the displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook.
    /// </summary>
    /// <value>The displayed <see cref="Servers.Webhook">webhook</see> <see cref="UserSummary.Avatar">profile picture</see></value>
    /// <seealso cref="Username" />
    [JsonProperty("avatar_url")]
    public Uri? Avatar { get; set; }

    /// <summary>
    /// Gets whether the message is <see cref="Content">text-only</see> and has no other content.
    /// </summary>
    /// <returns>Message does not have embeds</returns>
    public bool OnlyText => Embeds is null || !Embeds.Any();
    #endregion

    #region Constructors
    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> with no content.
    /// </summary>
    public MessageContent() { }

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="Message">message</see></param>
    public MessageContent(string? content) =>
        Content = content;

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="Message">message</see></param>
    public MessageContent(object? content) : this(content?.ToString()) { }

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> from the JSON properties.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="Message">message</see></param>
    /// <param name="embeds">The list of <see cref="Embed">custom embeds</see> that <see cref="Message">the message</see> contains</param>
    /// <param name="replyMessageIds">The list of <see cref="Message">messages</see> being replied to</param>
    /// <param name="isPrivate">Whether the <see cref="Message.IsReply">reply</see> or mention is private</param>
    /// <param name="isSilent">Whether the <see cref="Message.IsReply">reply</see> or mention is silent and doesn't ping any user</param>
    /// <param name="username">The displayed <see cref="UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="UserSummary.Avatar">profile picture</see> of the webhook</param>
    [JsonConstructor]
    public MessageContent(
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? content = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Embed>? embeds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Guid>? replyMessageIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isPrivate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isSilent = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? username = null,

        [JsonProperty("avatar_url", NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null
    ) : this(content) =>
        (Embeds, ReplyMessageIds, IsPrivate, IsSilent, Username, Avatar) = (embeds, replyMessageIds, isPrivate, isSilent, username, avatar);
    #endregion
}