using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents an item in a <see cref="ListChannel">list channel</see>.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached <see cref="Item">list item</see>.</para>
/// </remarks>
/// <typeparam name="T">The type of the <see cref="Item">list item's</see> note</typeparam>
/// <seealso cref="Item" />
/// <seealso cref="ItemSummary" />
/// <seealso cref="ItemNote" />
/// <seealso cref="ItemNoteSummary" />
/// <seealso cref="Content.Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public abstract class ItemBase<T> : ChannelContent<Guid, HashId>, IUpdatableContent, IWebhookCreatable, IContentMarkdown, IServerBased where T : ItemNoteSummary
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the message in the <see cref="Item">list item</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents of the list item are formatted in Markdown. The contents must only be in a single line.</para>
    /// <para>Videos, images, code blocks and other block formatting is not supported.</para>
    /// </remarks>
    /// <value>Single-line markdown string</value>
    public string Message { get; }

    /// <summary>
    /// Gets the <see cref="Mentions">mentions</see> found in <see cref="Message">the content</see>.
    /// </summary>
    /// <value><see cref="Mentions" />?</value>
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets the note of the <see cref="Item">list item</see>.
    /// </summary>
    /// <value>List item note?</value>
    public T? Note { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Webhook">webhook</see> that created the list item.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when the <see cref="Item">list item</see> was edited.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> who updated the <see cref="Item">list item</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="Item">list item</see> was completed.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? CompletedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> who ticked off the <see cref="Item">list item</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    public HashId? CompletedBy { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="Item">list item</see> of the <see cref="Item">list item</see>.
    /// </summary>
    /// <value><see cref="ChannelContent{TId, TServer}.Id">List item ID</see>?</value>
    public Guid? ParentId { get; }

    /// <summary>
    /// Gets whether the <see cref="Item">list item</see> was ticked off
    /// </summary>
    /// <returns><see cref="Item">List item</see> is completed</returns>
    public bool IsCompleted => CompletedAt is not null;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Item">list item</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    /// <seealso cref="Completed" />
    /// <seealso cref="Uncompleted" />
    public IObservable<ItemEvent> Updated =>
        ParentClient
            .ItemUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Item.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Item">list item</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Completed" />
    /// <seealso cref="Uncompleted" />
    public IObservable<ItemEvent> Deleted =>
        ParentClient
            .ItemDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Item.Id == Id
            )
            .Take(1);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> is set as completed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Item">list item</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> is set as completed</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="Uncompleted" />
    public IObservable<ItemEvent> Completed =>
        ParentClient
            .ItemCompleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Item.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> is set as non-completed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Item">list item</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Item">list item</see> is set as non-completed</returns>
    /// <seealso cref="Updated" />
    /// <seealso cref="Deleted" />
    /// <seealso cref="Completed" />
    public IObservable<ItemEvent> Uncompleted =>
        ParentClient
            .ItemUncompleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Item.Id == Id
            );
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemBase{T}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the list item is</param>
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
    /// <returns>New <see cref="ItemBase{T}" /> JSON instance</returns>
    /// <seealso cref="ItemBase{T}" />
    [JsonConstructor]
    protected ItemBase(
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
    /// <inheritdoc cref="AbstractGuildedClient.UpdateItemAsync(Guid, Guid, string, string?)" />
    /// <param name="message">The new contents of the list item's message in Markdown plain text</param>
    /// <param name="note">The new contents of the list item's note in Markdown plain text</param>
    public Task<Item> UpdateAsync(string message, string? note = null) =>
        ParentClient.UpdateItemAsync(ChannelId, Id, message, note);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteItemAsync(Guid, Guid)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteItemAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.CompleteItemAsync(Guid, Guid)" />
    public Task CompleteAsync() =>
        ParentClient.CompleteItemAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.UncompleteItemAsync(Guid, Guid)" />
    public Task UncompleteAsync() =>
        ParentClient.UncompleteItemAsync(ChannelId, Id);
    #endregion
}

/// <summary>
/// Represents an item in a <see cref="ListChannel">list channel</see>.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached <see cref="Item">list item</see>.</para>
/// </remarks>
/// <seealso cref="ItemSummary" />
/// <seealso cref="ItemNote" />
/// <seealso cref="ItemNoteSummary" />
/// <seealso cref="ItemBase{T}" />
/// <seealso cref="Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public class Item : ItemBase<ItemNote>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Item" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the list item is</param>
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
    /// <returns>New <see cref="Item" /> JSON instance</returns>
    /// <seealso cref="Item" />
    [JsonConstructor]
    public Item(
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
        ItemNote? note = null,

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
/// Represents an item in a <see cref="ListChannel">list channel</see>.
/// </summary>
/// <remarks>
/// <para>Either an existing or a cached <see cref="Item">list item</see>.</para>
/// </remarks>
/// <seealso cref="Item" />
/// <seealso cref="ItemNote" />
/// <seealso cref="ItemNoteSummary" />
/// <seealso cref="ItemBase{T}" />
/// <seealso cref="Message" />
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
public class ItemSummary : ItemBase<ItemNote>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the list item</param>
    /// <param name="channelId">The identifier of the channel where the list item is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the list item is</param>
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
    /// <returns>New <see cref="ItemSummary" /> JSON instance</returns>
    /// <seealso cref="ItemSummary" />
    [JsonConstructor]
    public ItemSummary(
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
        ItemNote? note = null,

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