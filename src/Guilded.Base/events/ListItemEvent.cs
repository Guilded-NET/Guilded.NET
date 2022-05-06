using System;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>ListItemCreated</c>, <c>ListItemUpdated</c>, <c>ListItemCompleted</c>, <c>ListItemUncompleted</c> or <c>ListItemDeleted</c> and opcode <c>0</c> that occurs once someone posts, edits or deletes a <see cref="DocEvent.Doc">doc</see> in <see cref="ChannelId">a channel</see>.
/// </summary>
/// <seealso cref="ListItemBase{T}"/>
public class ListItemEvent : BaseObject, IServerEvent
{
    #region JSON properties
    /// <summary>
    /// Gets the list item received from the event.
    /// </summary>
    /// <value>List item</value>
    public ListItem ListItem { get; }
    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId"/>
    public Guid ChannelId => ListItem.ChannelId;
    /// <inheritdoc cref="ListItemBase{T}.Message"/>
    public string Message => ListItem.Message;
    /// <inheritdoc cref="ListItemBase{T}.Note"/>
    public ListItemNote? Note => ListItem.Note;
    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy"/>
    public HashId CreatedBy => ListItem.CreatedBy;
    /// <inheritdoc cref="ListItemBase{T}.CreatedByWebhook"/>
    public Guid? CreatedByWebhook => ListItem.CreatedByWebhook;
    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt"/>
    public DateTime CreatedAt => ListItem.CreatedAt;
    /// <inheritdoc cref="ListItemBase{T}.UpdatedAt"/>
    public DateTime? UpdatedAt => ListItem.UpdatedAt;
    /// <inheritdoc cref="ListItemBase{T}.CompletedBy"/>
    public HashId? CompletedBy => ListItem.CompletedBy;
    /// <inheritdoc cref="ListItemBase{T}.CompletedAt"/>
    public DateTime? CompletedAt => ListItem.CompletedAt;
    /// <inheritdoc cref="ListItemBase{T}.IsCompleted"/>
    public bool IsCompleted => ListItem.IsCompleted;
    /// <inheritdoc cref="ListItemBase{T}.ParentId"/>
    public Guid? ParentId => ListItem.ParentId;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListItemEvent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the list item event occurred</param>
    /// <param name="listItem">The list item received from the event</param>
    [JsonConstructor]
    public ListItemEvent(
        [JsonProperty(Required = Required.Always)]
        ListItem listItem,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, ListItem) = (serverId, listItem);
    #endregion

    #region Additional
    /// <inheritdoc cref="ListItemBase{T}.UpdateAsync(string, string?)"/>
    public async Task<ListItem> UpdateAsync(string message, string? note) =>
        await ListItem.UpdateAsync(message, note).ConfigureAwait(false);
    /// <inheritdoc cref="ListItemBase{T}.DeleteAsync"/>
    public async Task DeleteAsync() =>
        await ListItem.DeleteAsync().ConfigureAwait(false);
    /// <inheritdoc cref="ListItemBase{T}.CompleteAsync"/>
    public async Task CompleteAsync() =>
        await ListItem.CompleteAsync().ConfigureAwait(false);
    /// <inheritdoc cref="ListItemBase{T}.UncompleteAsync"/>
    public async Task UncompleteAsync() =>
        await ListItem.UncompleteAsync().ConfigureAwait(false);
    #endregion
}