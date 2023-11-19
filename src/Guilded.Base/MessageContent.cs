using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Embeds;
using Newtonsoft.Json;

namespace Guilded.Base;

/// <summary>
/// Represents the contents of a message for creation and updating.
/// </summary>
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
    /// <summary>
    /// Gets or sets whether the <see cref="MessageContent">message</see> being sent that is a <see cref="ReplyMessageIds">replying message</see> or a user mention is private.
    /// </summary>
    /// <remarks>
    /// <para>When <see cref="IsPrivate"/> is <see langword="true" />, either <see cref="ReplyMessageIds"/> must have at least 1 message ID or <see cref="Content" /> or <see cref="Embeds" /> must contain at least 1 user mention.</para>
    /// </remarks>
    /// <value>Whether the <see cref="MessageContent">message</see> being sent that is a <see cref="ReplyMessageIds">replying message</see> or a user mention is private</value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="IsSilent" />
    /// <seealso cref="Content"/> 
    /// <seealso cref="ReplyMessageIds"/> 
    public bool? IsPrivate { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="MessageContent">message</see> being sent that is a <see cref="ReplyMessageIds">replying message</see> or a user mention does not ping anyone.
    /// </summary>
    /// <remarks>
    /// <para>This does not require <see cref="ReplyMessageIds"/> or user mentions. However, if both of them are absent, it will do not result in any effect, as either <see cref="ReplyMessageIds"/> or user mentions causes one to be pinged.</para>
    /// </remarks>
    /// <value>Whether the <see cref="MessageContent">message</see> being sent that is a <see cref="ReplyMessageIds">replying message</see> or a user mention does not ping anyone</value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="IsPrivate" />
    /// <seealso cref="Content"/> 
    /// <seealso cref="ReplyMessageIds"/> 
    public bool? IsSilent { get; set; }

    /// <summary>
    /// Gets or sets the main text content of the <see cref="MessageContent">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Full Guilded-flavoured Markdown is allowed.</para>
    /// <note type="warning">
    /// <para>As of now, Markdown parser of Guilded API is very limited and therefore may cause a crash when using specific block formatting, such as:</para>
    /// <list type="unordered">
    ///     <item>code blocks</item>
    ///     <item>quote blocks</item>
    ///     <item>ordered/numbered lists</item>
    ///     <item>unordered/bulleted lists.</item>
    /// </list>
    /// <para>Some of the inline Markdown formatting may also not apply at all:</para>
    /// <list type="unordered">
    ///     <item>spoilers</item>
    ///     <item>images.</item>
    /// </list>
    /// </note>
    /// </remarks>
    /// <value>The main text content of the <see cref="MessageContent">message</see></value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="Embeds" />
    /// <seealso cref="ReplyMessageIds"/> 
    /// <seealso cref="HiddenUrls"/> 
    public string? Content
    {
        get => _content;
        set
        {
            if (value is not null && value?.Length > 4000)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Contents of the message exceed 4000 character limit");

            _content = value;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Embed">embeds</see> of the <see cref="MessageContent">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Full Guilded-flavoured Markdown is allowed.</para>
    /// <note type="warning">
    /// <para>As of now, only <c>1</c> embed is allowed per message.</para>
    /// </note>
    /// </remarks>
    /// <value>The <see cref="Embed">embeds</see> of the <see cref="MessageContent">message</see></value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="Content" />
    /// <seealso cref="ReplyMessageIds"/> 
    /// <seealso cref="HiddenUrls"/> 
    public IList<Embed>? Embeds
    {
        get => _embeds;
        set
        {
            if (value is not null && value?.Count > 1)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given embed list exceeds the 1 embed per message limit");

            _embeds = value;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="MessageContent">messages</see> that are being replied to.
    /// </summary>
    /// <remarks>
    /// <para>The maximum amount of messages that can be replied to is <c>5</c>.</para>
    /// </remarks>
    /// <value>The <see cref="MessageContent">messages</see> that are being replied to</value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds"/> 
    /// <seealso cref="HiddenUrls"/> 
    public IList<Guid>? ReplyMessageIds
    {
        get => _replyMessageIds;
        set
        {
            if (value is not null && value?.Count > 5)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given reply list exceeds 5 replies per message limit");

            _replyMessageIds = value;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see>.
    /// </summary>
    /// <value>The <see cref="Uri">URLs</see> that will not have <see cref="Embed">link embeds</see></value>
    /// <seealso cref="MessageContent"/> 
    /// <seealso cref="Content" />
    /// <seealso cref="Embeds"/> 
    /// <seealso cref="ReplyMessageIds"/> 
    [JsonProperty("hiddenLinkPreviewUrls")]
    public ISet<Uri>? HiddenUrls { get; set; }

    /// <summary>
    /// Gets the displayed <see cref="T:Guilded.Users.UserSummary.Name">name</see> of the webhook.
    /// </summary>
    /// <value>The displayed <see cref="T:Guilded.Servers.Webhook">webhook</see> <see cref="T:Guilded.Users.UserSummary.Name">name</see></value>
    /// <seealso cref="MessageContent" />
    /// <seealso cref="Avatar" />
    public string? Username
    {
        get => _username;
        set
        {
            if (value is not null && value?.Length > 128)
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Given message username override is over the 128 character limit");

            _username = value;
        }
    }

    /// <summary>
    /// Gets the displayed <see cref="T:Guilded.Users.UserSummary.Avatar">profile picture</see> of the webhook.
    /// </summary>
    /// <value>The displayed <see cref="T:Guilded.Servers.Webhook">webhook</see> <see cref="T:Guilded.Users.UserSummary.Avatar">profile picture</see></value>
    /// <seealso cref="MessageContent" />
    /// <seealso cref="Username" />
    [JsonProperty("avatar_url")]
    public Uri? Avatar { get; set; }

    /// <summary>
    /// Gets whether the <see cref="MessageContent">message</see> is <see cref="Content">text-only</see> and has no <see cref="Embeds">other content</see>.
    /// </summary>
    /// <returns>Whether the <see cref="MessageContent">message</see> is <see cref="Content">text-only</see> and has no <see cref="Embeds">other content</see></returns>
    [JsonIgnore]
    public bool OnlyText => Embeds is null || !Embeds.Any();
    #endregion

    #region Constructors
    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> with no content.
    /// </summary>
    public MessageContent() { }

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" /> from the JSON properties.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="T:Guilded.Content.Message">message</see></param>
    /// <param name="embeds">The list of <see cref="Embed">custom embeds</see> that the <see cref="T:Guilded.Content.Message">message</see> contains</param>
    /// <param name="replyMessageIds">The list of <see cref="T:Guilded.Content.Message">messages</see> being replied to</param>
    /// <param name="hiddenLinkPreviewUrls">The list of URLs to not show embeds of that the <paramref name="content" /> holds</param>
    /// <param name="isPrivate">Whether the <see cref="T:Guilded.Content.Message.IsReply">reply</see> or mention is private</param>
    /// <param name="isSilent">Whether the <see cref="T:Guilded.Content.Message.IsReply">reply</see> or mention is silent and doesn't ping any user</param>
    /// <param name="username">The displayed <see cref="T:Guilded.Users.UserSummary.Name">name</see> of the webhook</param>
    /// <param name="avatar">The displayed <see cref="T:Guilded.Users.UserSummary.Avatar">profile picture</see> of the webhook</param>
    [JsonConstructor]
    public MessageContent(
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? content = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Embed>? embeds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Guid>? replyMessageIds = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ISet<Uri>? hiddenLinkPreviewUrls = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isPrivate = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool? isSilent = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? username = null,

        [JsonProperty("avatar_url", NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null
    ) : this(content) =>
        (Embeds, ReplyMessageIds, HiddenUrls, IsPrivate, IsSilent, Username, Avatar) = (embeds, replyMessageIds, hiddenLinkPreviewUrls, isPrivate, isSilent, username, avatar);

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="T:Guilded.Content.Message">message</see></param>
    public MessageContent(string? content) =>
        Content = content;

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="embeds">The list of <see cref="Embed">custom embeds</see> that the <see cref="T:Guilded.Content.Message">message</see> contains</param>
    public MessageContent(params Embed[] embeds) =>
        Embeds = embeds;

    /// <summary>
    /// Creates an instance of <see cref="MessageContent" />.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="T:Guilded.Content.Message">message</see></param>
    public MessageContent(object? content) : this(content?.ToString()) { }
    #endregion
}