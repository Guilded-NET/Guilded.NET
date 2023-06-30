using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a <see cref="ChannelType.List">list</see>-type channel.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="ChatChannel" />
/// <seealso cref="VoiceChannel" />
/// <seealso cref="StreamChannel" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="DocChannel" />
/// <seealso cref="MediaChannel" />
/// <seealso cref="SchedulingChannel" />
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class ListChannel : ServerChannel
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ListChannel" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="ServerChannel">channel</see></param>
    /// <param name="groupId">The identifier of the parent group of the <see cref="ServerChannel">channel</see></param>
    /// <param name="serverId">The identifier of the parent <see cref="Server">server</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="type">The type of content <see cref="ServerChannel">channel</see> holds</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdAt">The date when the <see cref="ServerChannel">channel</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="ServerChannel">channel</see> was edited</param>
    /// <param name="archivedBy">The identifier of <see cref="User">user</see> that archived the <see cref="ServerChannel">channel</see></param>
    /// <param name="archivedAt">The date when the <see cref="ServerChannel">channel</see> was archived</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about</param>
    /// <param name="rootId">The identifier of the ancestor channel of the <see cref="ServerChannel">channel</see> that exist at the <see cref="Group">group</see> level</param>
    /// <param name="parentId">The identifier of the parent <see cref="ServerChannel">channel</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="messageId">The identifier of the <see cref="Message">message</see> that hosts the thread</param>
    /// <param name="categoryId">The identifier of the parent category of the <see cref="ServerChannel">channel</see></param>
    /// <returns>New <see cref="ListChannel" /> JSON instance</returns>
    /// <seealso cref="ListChannel" />
    [JsonConstructor]
    public ListChannel(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        HashId groupId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        ChannelType type,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? archivedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? archivedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? topic = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? rootId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? messageId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) : base(id, groupId, serverId, type, name, createdBy, createdAt, updatedAt, archivedBy, archivedAt, topic, rootId, parentId, messageId, categoryId) { }
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetItemsAsync(Guid)" />
    public Task<IList<ItemSummary>> GetItemsAsync() =>
        ParentClient.GetItemsAsync(Id);

    /// <inheritdoc cref="AbstractGuildedClient.GetItemAsync(Guid, Guid)" />
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to get</param>
    public Task<Item> GetItemAsync(Guid listItem) =>
        ParentClient.GetItemAsync(Id, listItem);

    /// <inheritdoc cref="AbstractGuildedClient.CreateItemAsync(Guid, string, string)" />
    /// <param name="message">The text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The text content of an <see cref="ItemNote">optional note</see> in the <see cref="Item">list item</see></param>
    public Task<Item> CreateItemAsync(string message, string? note = null) =>
        ParentClient.CreateItemAsync(Id, message, note);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateItemAsync(Guid, Guid, string, string)" />
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to edit</param>
    /// <param name="message">The new text content of the <see cref="Item">list item</see></param>
    /// <param name="note">The new text content of the note in the <see cref="Item">list item</see></param>
    public Task<Item> UpdateItemAsync(Guid listItem, string message, string? note = null) =>
        ParentClient.UpdateItemAsync(Id, listItem, message, note);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteItemAsync(Guid, Guid)" />
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to delete</param>
    public Task DeleteItemAsync(Guid listItem) =>
        ParentClient.DeleteItemAsync(Id, listItem);

    /// <inheritdoc cref="AbstractGuildedClient.CompleteItemAsync(Guid, Guid)" />
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    public Task CompleteItemAsync(Guid listItem) =>
        ParentClient.CompleteItemAsync(Id, listItem);

    /// <inheritdoc cref="AbstractGuildedClient.CompleteItemAsync(Guid, Guid)" />
    /// <param name="listItem">The identifier of the <see cref="Item">list item</see> to complete</param>
    public Task UncompleteItemAsync(Guid listItem) =>
        ParentClient.UncompleteItemAsync(Id, listItem);
    #endregion
}