using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates, completes, uncompletes or deletes <see cref="Content.Item">a list item</see>.
/// </summary>
/// <seealso cref="ItemBase{T}" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class ItemEvent : IModelHasId<Guid>, ICreatableContent, IWebhookCreated, IUpdatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the list item received from the event.
    /// </summary>
    /// <value>List item</value>
    /// <seealso cref="ItemEvent" />
    /// <seealso cref="Message" />
    /// <seealso cref="ServerId" />
    public Item Item { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public Guid Id => Item.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Item.ChannelId;

    /// <inheritdoc cref="ItemBase{T}.Message" />
    public string Message => Item.Message;

    /// <inheritdoc cref="ItemBase{T}.Mentions" />
    public Mentions? Mentions => Item.Mentions;

    /// <inheritdoc cref="ItemBase{T}.Note" />
    public ItemNote? Note => Item.Note;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Item.CreatedBy;

    /// <inheritdoc cref="ItemBase{T}.CreatedByWebhook" />
    public Guid? CreatedByWebhook => Item.CreatedByWebhook;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Item.CreatedAt;

    /// <inheritdoc cref="ItemBase{T}.UpdatedAt" />
    public DateTime? UpdatedAt => Item.UpdatedAt;

    /// <inheritdoc cref="ItemBase{T}.CompletedBy" />
    public HashId? CompletedBy => Item.CompletedBy;

    /// <inheritdoc cref="ItemBase{T}.CompletedAt" />
    public DateTime? CompletedAt => Item.CompletedAt;

    /// <inheritdoc cref="ItemBase{T}.IsCompleted" />
    public bool IsCompleted => Item.IsCompleted;

    /// <inheritdoc cref="ItemBase{T}.ParentId" />
    public Guid? ParentId => Item.ParentId;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Item.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="ItemBase{T}.Updated" />
    public IObservable<ItemEvent> Updated =>
        Item.Updated;

    /// <inheritdoc cref="ItemBase{T}.Deleted" />
    public IObservable<ItemEvent> Deleted =>
        Item.Deleted;

    /// <inheritdoc cref="ItemBase{T}.Completed" />
    public IObservable<ItemEvent> Completed =>
        Item.Completed;

    /// <inheritdoc cref="ItemBase{T}.Uncompleted" />
    public IObservable<ItemEvent> Uncompleted =>
        Item.Uncompleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the list item event occurred</param>
    /// <param name="listItem">The list item received from the event</param>
    /// <returns>New <see cref="ItemEvent" /> JSON instance</returns>
    /// <seealso cref="ItemEvent" />
    [JsonConstructor]
    public ItemEvent(
        [JsonProperty(Required = Required.Always)]
        Item listItem,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Item) = (serverId, listItem);
    #endregion

    #region Methods
    /// <inheritdoc cref="ItemBase{T}.UpdateAsync(string, string?)" />
    public Task<Item> UpdateAsync(string message, string? note) =>
        Item.UpdateAsync(message, note);

    /// <inheritdoc cref="ItemBase{T}.DeleteAsync" />
    public Task DeleteAsync() =>
        Item.DeleteAsync();

    /// <inheritdoc cref="ItemBase{T}.CompleteAsync" />
    public Task CompleteAsync() =>
        Item.CompleteAsync();

    /// <inheritdoc cref="ItemBase{T}.UncompleteAsync" />
    public Task UncompleteAsync() =>
        Item.UncompleteAsync();
    #endregion
}