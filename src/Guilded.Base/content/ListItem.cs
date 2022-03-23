using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// A list item in a list channel.
/// </summary>
/// <remarks>
/// <para>A list item in a list channel with an optional <see cref="Note"/>.</para>
/// </remarks>
/// <seealso cref="Content.Message"/>
/// <seealso cref="ForumThread"/>
/// <seealso cref="ListItemNote"/>
/// <seealso cref="ListItemNoteSummary"/>
public class ListItem<T> : ChannelContent<Guid, HashId>, IUpdatableContent, IWebhookCreatable where T : ListItemNoteSummary
{
    #region JSON properties
    /// <summary>
    /// The contents of the message in the item.
    /// </summary>
    /// <remarks>
    /// <para>The contents of the list item formatted in Markdown. The contents must only be in a single line.</para>
    /// <para>Videos, images, code blocks and other block formatting is not supported.</para>
    /// </remarks>
    /// <value>Single-line markdown string</value>
    public string Message { get; }
    /// <summary>
    /// The note of the list item.
    /// </summary>
    /// <remarks>
    /// <para>The information about the list item's note.</para>
    /// </remarks>
    /// <value>List item note?</value>
    public T? Note { get; }
    /// <summary>
    /// The identifier of the webhook creator of the list item.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the webhook that created this list item.</para>
    /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
    /// </remarks>
    /// <value>Webhook ID?</value>
    public Guid? CreatedByWebhook { get; }
    /// <summary>
    /// The date of when the list item was updated.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the list item was updated/edited.</para>
    /// </remarks>
    /// <value>Date?</value>
    public DateTime? UpdatedAt { get; }
    /// <summary>
    /// The identifier of the member updater of the list item.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the user who updated this list item. Only includes the user who updated this list item most recently.</para>
    /// </remarks>
    /// <value>User ID?</value>
    public HashId? UpdatedBy { get; }
    /// <summary>
    /// The date of when the list item was completed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the list item was ticked off.</para>
    /// </remarks>
    /// <value>Date?</value>
    public DateTime? CompletedAt { get; }
    /// <summary>
    /// The identifier of the member completer of the list item.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the user who ticked off this list item. Only includes the user who completed this list item most recently.</para>
    /// </remarks>
    /// <value>User ID?</value>
    public HashId? CompletedBy { get; }
    /// <summary>
    /// The identifier of the parent list item of this list item.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the list item that adopts this list item as a child.</para>
    /// </remarks>
    /// <value>User ID?</value>
    public Guid? ParentListItemId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="ListItem{T}"/> with provided details.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of the server where the list item is</param>
    /// <param name="message">The contents of the message in list item</param>
    /// <param name="note">The note of the list item</param>
    /// <param name="createdBy">The identifier of the user creator of the list item</param>
    /// <param name="createdByWebhookId">The identifier of the webhook creator of the list item</param>
    /// <param name="createdAt">The date of when the list item was created</param>
    /// <param name="updatedAt">The date of when the list item was updated</param>
    /// <param name="updatedBy">The identifier of the user updater of the list item</param>
    /// <param name="completedAt">The date of when the list item was completed</param>
    /// <param name="completedBy">The identifier of the user completer of the list item</param>
    /// <param name="parentListItemId">The identifier of the parent list item of this list item</param>
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

        [JsonProperty]
        T? note,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty]
        Guid? createdByWebhookId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty]
        DateTime? updatedAt,

        [JsonProperty]
        HashId? updatedBy,

        [JsonProperty]
        DateTime? completedAt,

        [JsonProperty]
        HashId? completedBy,

        [JsonProperty]
        Guid? parentListItemId
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Message, Note, CreatedByWebhook, UpdatedAt, UpdatedBy, CompletedAt, CompletedBy, ParentListItemId) = (message, note, createdByWebhookId, updatedAt, updatedBy, completedAt, completedBy, parentListItemId);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.UpdateMessageAsync(Guid, Guid, string)"/>
    /// <param name="message">The new contents of the list item's message in Markdown plain text</param>
    /// <param name="note">The new contents of the list item's note in Markdown plain text</param>
    public async Task<ListItem<ListItemNote>> UpdateAsync(string? message = null, string? note = null) =>
        await ParentClient.UpdateListItemAsync(ChannelId, Id, message, note).ConfigureAwait(false);
    /// <inheritdoc cref="BaseGuildedClient.DeleteListItemAsync(Guid, Guid)"/>
    public async Task DeleteAsync() =>
        await ParentClient.DeleteListItemAsync(ChannelId, Id).ConfigureAwait(false);
    #endregion
}
/// <summary>
/// The summary of the list item's note.
/// </summary>
/// <remarks>
/// <para>The minimal information about the list item's note.</para>
/// </remarks>
public class ListItemNoteSummary : BaseObject, ICreatableContent
{
    #region Who, when
    /// <summary>
    /// The identifier of the user creator of the note.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the user that created this note.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId CreatedBy { get; }
    /// <summary>
    /// The date of when the note was created.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="DateTime"/> of when the note was created.</para>
    /// </remarks>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="ListItemNoteSummary"/> with provided details.
    /// </summary>
    /// <param name="createdBy">The identifier of the user creator of the list item's node</param>
    /// <param name="createdAt">The date of when the list item's note was created</param>
    [JsonConstructor]
    public ListItemNoteSummary(
        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt
    ) =>
        (CreatedAt, CreatedBy) = (createdAt, createdBy);
    #endregion
}
/// <summary>
/// The information about the list item's note.
/// </summary>
/// <remarks>
/// <para>The full information about the list item's note.</para>
/// </remarks>
public class ListItemNote : ListItemNoteSummary
{
    #region JSON properties
    /// <summary>
    /// The contents of the note in the item.
    /// </summary>
    /// <remarks>
    /// <para>The contents of the list item's note formatted in Markdown.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    public string Content { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="ListItemNote"/> with provided details.
    /// </summary>
    /// <param name="content">The contents of the note</param>
    /// <param name="createdBy">The identifier of the user creator of the list item's note</param>
    /// <param name="createdAt">The date of when the list item's note was created</param>
    [JsonConstructor]
    public ListItemNote(
        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt
    ) : base(createdBy, createdAt) =>
        Content = content;
    #endregion
}