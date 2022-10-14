using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates, completes, uncompletes or deletes <see cref="Content.ListItem">a list item</see>.
/// </summary>
/// <seealso cref="ListItemBase{T}" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="ChannelEvent" />
public class ListItemEvent : IModelHasId<Guid>, ICreatableContent, IWebhookCreatable, IUpdatableContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the list item received from the event.
    /// </summary>
    /// <value>List item</value>
    /// <seealso cref="ListItemEvent" />
    /// <seealso cref="Message" />
    /// <seealso cref="ServerId" />
    public ListItem Item { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public Guid Id => Item.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Item.ChannelId;

    /// <inheritdoc cref="ListItemBase{T}.Message" />
    public string Message => Item.Message;

    /// <inheritdoc cref="ListItemBase{T}.Mentions" />
    public Mentions? Mentions => Item.Mentions;

    /// <inheritdoc cref="ListItemBase{T}.Note" />
    public ListItemNote? Note => Item.Note;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Item.CreatedBy;

    /// <inheritdoc cref="ListItemBase{T}.CreatedByWebhook" />
    public Guid? CreatedByWebhook => Item.CreatedByWebhook;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Item.CreatedAt;

    /// <inheritdoc cref="ListItemBase{T}.UpdatedAt" />
    public DateTime? UpdatedAt => Item.UpdatedAt;

    /// <inheritdoc cref="ListItemBase{T}.CompletedBy" />
    public HashId? CompletedBy => Item.CompletedBy;

    /// <inheritdoc cref="ListItemBase{T}.CompletedAt" />
    public DateTime? CompletedAt => Item.CompletedAt;

    /// <inheritdoc cref="ListItemBase{T}.IsCompleted" />
    public bool IsCompleted => Item.IsCompleted;

    /// <inheritdoc cref="ListItemBase{T}.ParentId" />
    public Guid? ParentId => Item.ParentId;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Item.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the list item event occurred</param>
    /// <param name="listItem">The list item received from the event</param>
    /// <returns>New <see cref="ListItemEvent" /> JSON instance</returns>
    /// <seealso cref="ListItemEvent" />
    [JsonConstructor]
    public ListItemEvent(
        [JsonProperty(Required = Required.Always)]
        ListItem listItem,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Item) = (serverId, listItem);
    #endregion

    #region Methods
    /// <inheritdoc cref="ListItemBase{T}.UpdateAsync(string, string?)" />
    public Task<ListItem> UpdateAsync(string message, string? note) =>
        Item.UpdateAsync(message, note);

    /// <inheritdoc cref="ListItemBase{T}.DeleteAsync" />
    public Task DeleteAsync() =>
        Item.DeleteAsync();

    /// <inheritdoc cref="ListItemBase{T}.CompleteAsync" />
    public Task CompleteAsync() =>
        Item.CompleteAsync();

    /// <inheritdoc cref="ListItemBase{T}.UncompleteAsync" />
    public Task UncompleteAsync() =>
        Item.UncompleteAsync();
    #endregion
}