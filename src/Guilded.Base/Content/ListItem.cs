using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents an item in <see cref="Servers.ChannelType.List">a list channel</see>.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached list item.</para>
/// </remarks>
/// <typeparam name="T">The type of the list item's note</typeparam>
/// <seealso cref="ListItem" />
/// <seealso cref="ListItemSummary" />
/// <seealso cref="ListItemNote" />
/// <seealso cref="ListItemNoteSummary" />
/// <seealso cref="Content.Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public abstract class ListItemBase<T> : ChannelContent<Guid, HashId>, IUpdatableContent, IWebhookCreatable, IContentMarkdown, IServerBased where T : ListItemNoteSummary
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the message in <see cref="ListItem">the item</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents of the list item are formatted in Markdown. The contents must only be in a single line.</para>
    /// <para>Videos, images, code blocks and other block formatting is not supported.</para>
    /// </remarks>
    /// <value>Single-line markdown string</value>
    public string Message { get; }

    /// <summary>
    /// Gets <see cref="Mentions">the mentions</see> found in <see cref="Message">the content</see>.
    /// </summary>
    /// <value><see cref="Mentions" />?</value>
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets the note of <see cref="ListItem">the item</see>.
    /// </summary>
    /// <value>List item note?</value>
    public T? Note { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Servers.Webhook">the webhook</see> that created the list item.
    /// </summary>
    /// <value><see cref="Servers.Webhook.Id">Webhook ID</see>?</value>
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when <see cref="ListItem">the item</see> was edited.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> who updated <see cref="ListItem">the list item</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ListItem">the item</see> was completed.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? CompletedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> who ticked off <see cref="ListItem">the item</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    public HashId? CompletedBy { get; }

    /// <summary>
    /// Gets the identifier of <see cref="ListItem">the parent item</see> of <see cref="ListItem">the item</see>.
    /// </summary>
    /// <value><see cref="ChannelContent{TId, TServer}.Id">List item ID</see>?</value>
    public Guid? ParentId { get; }

    /// <summary>
    /// Gets whether <see cref="ListItem">the list item</see> was ticked off
    /// </summary>
    /// <returns><see cref="ListItem">List item</see> is completed</returns>
    public bool IsCompleted => CompletedAt is not null;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemBase{T}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the list item is</param>
    /// <param name="message">The text contents of the message in list item</param>
    /// <param name="note">The note of the list item</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the list item</param>
    /// <param name="createdByWebhookId">The identifier of <see cref="Servers.Webhook">the webhook</see> creator of the list item</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Message">the content</see></param>
    /// <param name="createdAt">the date when the list item was created</param>
    /// <param name="updatedAt">the date when the list item was edited</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> updater of the list item</param>
    /// <param name="completedAt">the date when the list item was completed</param>
    /// <param name="completedBy">The identifier of <see cref="User">user</see> completer of the list item</param>
    /// <param name="parentListItemId">The identifier of the parent list item of this list item</param>
    /// <returns>New <see cref="ListItemBase{T}" /> JSON instance</returns>
    /// <seealso cref="ListItemBase{T}" />
    [JsonConstructor]
    protected ListItemBase(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string message,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        T? note = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? completedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? completedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentListItemId = null
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Message, Note, Mentions, CreatedByWebhook, UpdatedAt, UpdatedBy, CompletedAt, CompletedBy, ParentId) = (message, note, mentions, createdByWebhookId, updatedAt, updatedBy, completedAt, completedBy, parentListItemId);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string)" />
    /// <param name="message">The new contents of the list item's message in Markdown plain text</param>
    /// <param name="note">The new contents of the list item's note in Markdown plain text</param>
    public Task<ListItem> UpdateAsync(string message, string? note = null) =>
        ParentClient.UpdateListItemAsync(ChannelId, Id, message, note);

    /// <inheritdoc cref="BaseGuildedClient.DeleteListItemAsync(Guid, Guid)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteListItemAsync(ChannelId, Id);

    /// <inheritdoc cref="BaseGuildedClient.CompleteListItemAsync(Guid, Guid)" />
    public Task CompleteAsync() =>
        ParentClient.CompleteListItemAsync(ChannelId, Id);

    /// <inheritdoc cref="BaseGuildedClient.UncompleteListItemAsync(Guid, Guid)" />
    public Task UncompleteAsync() =>
        ParentClient.UncompleteListItemAsync(ChannelId, Id);
    #endregion
}

/// <summary>
/// Represents an item in a list channel.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached list item.</para>
/// </remarks>
/// <seealso cref="ListItemSummary" />
/// <seealso cref="ListItemNote" />
/// <seealso cref="ListItemNoteSummary" />
/// <seealso cref="ListItemBase{T}" />
/// <seealso cref="Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public class ListItem : ListItemBase<ListItemNote>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItem" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the list item is</param>
    /// <param name="message">The text contents of the message in list item</param>
    /// <param name="note">The note of the list item</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Message">the content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the list item</param>
    /// <param name="createdByWebhookId">The identifier of <see cref="Servers.Webhook">the webhook</see> creator of the list item</param>
    /// <param name="createdAt">the date when the list item was created</param>
    /// <param name="updatedAt">the date when the list item was edited</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> updater of the list item</param>
    /// <param name="completedAt">the date when the list item was completed</param>
    /// <param name="completedBy">The identifier of <see cref="User">user</see> completer of the list item</param>
    /// <param name="parentListItemId">The identifier of the parent list item of this list item</param>
    /// <returns>New <see cref="ListItem" /> JSON instance</returns>
    /// <seealso cref="ListItem" />
    [JsonConstructor]
    public ListItem(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string message,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ListItemNote? note = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? completedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? completedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentListItemId = null
    ) : base(id, channelId, serverId, message, createdBy, createdAt, createdByWebhookId, mentions, note, updatedAt, updatedBy, completedAt, completedBy, parentListItemId) { }
    #endregion
}

/// <summary>
/// Represents an item in a list channel.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached list item.</para>
/// </remarks>
/// <seealso cref="ListItem" />
/// <seealso cref="ListItemNote" />
/// <seealso cref="ListItemNoteSummary" />
/// <seealso cref="ListItemBase{T}" />
/// <seealso cref="Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public class ListItemSummary : ListItemBase<ListItemNote>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the list item is</param>
    /// <param name="message">The text contents of the message in list item</param>
    /// <param name="note">The note of the list item</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Message">the content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the list item</param>
    /// <param name="createdByWebhookId">The identifier of <see cref="Servers.Webhook">the webhook</see> creator of the list item</param>
    /// <param name="createdAt">the date when the list item was created</param>
    /// <param name="updatedAt">the date when the list item was edited</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> updater of the list item</param>
    /// <param name="completedAt">the date when the list item was completed</param>
    /// <param name="completedBy">The identifier of <see cref="User">user</see> completer of the list item</param>
    /// <param name="parentListItemId">The identifier of the parent list item of this list item</param>
    /// <returns>New <see cref="ListItemSummary" /> JSON instance</returns>
    /// <seealso cref="ListItemSummary" />
    [JsonConstructor]
    public ListItemSummary(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string message,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ListItemNote? note = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? completedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? completedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentListItemId = null
    ) : base(id, channelId, serverId, message, createdBy, createdAt, createdByWebhookId, mentions, note, updatedAt, updatedBy, completedAt, completedBy, parentListItemId) { }
    #endregion
}
/// <summary>
/// Represents the summary of <see cref="ListItemSummary">the list item's</see> note.
/// </summary>
public class ListItemNoteSummary : ICreatableContent, IUpdatableContent
{
    #region Properties

    #region Who, when
    /// <summary>
    /// Gets the identifier of <see cref="User">the user</see> who created <see cref="ListItemNote">the note</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ListItemNote">the note</see> was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// The identifier of <see cref="User">the user</see> who updated <see cref="ListItemNote">the note</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ListItemNote">the note</see> was edited.
    /// </summary>
    /// <value>Date</value>
    public DateTime? UpdatedAt { get; }
    #endregion

    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemNoteSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the note</param>
    /// <param name="createdAt">the date when the note was created</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> that updated the note</param>
    /// <param name="updatedAt">the date when the note was edited</param>
    /// <returns>New <see cref="ListItemNoteSummary" /> JSON instance</returns>
    /// <seealso cref="ListItemNoteSummary" />
    [JsonConstructor]
    public ListItemNoteSummary(
        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) = (createdAt, createdBy, updatedAt, updatedBy);
    #endregion
}
/// <summary>
/// Represents the full information about <see cref="ListItem">the list item's</see> note.
/// </summary>
public class ListItemNote : ListItemNoteSummary
{
    #region Properties
    /// <summary>
    /// Gets the contents of <see cref="ListItemNote">the note</see> in <see cref="ListItem">the item</see>.
    /// </summary>
    /// <value>Markdown string</value>
    public string Content { get; set; }

    /// <summary>
    /// Gets <see cref="Content.Mentions">the mentions</see> found in <see cref="Content">the content</see>.
    /// </summary>
    /// <value><see cref="Content.Mentions" />?</value>
    public Mentions? Mentions { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemNote" /> from the specified JSON properties.
    /// </summary>
    /// <param name="content">The contents of the note</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the list item's note</param>
    /// <param name="createdAt">the date when the list item's note was created</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> that updated the note</param>
    /// <param name="updatedAt">the date when the note was edited</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Message">the content</see></param>
    /// <returns>New <see cref="ListItemNote" /> JSON instance</returns>
    /// <seealso cref="ListItemNote" />
    [JsonConstructor]
    public ListItemNote(
        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null
    ) : base(createdBy, createdAt, updatedBy, updatedAt) =>
        (Content, Mentions) = (content, mentions);
    #endregion
}