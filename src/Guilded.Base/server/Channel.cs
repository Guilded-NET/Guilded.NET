using System;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents a navigatable item that contains content.
/// </summary>
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class ServerChannel : ClientObject, ICreatableContent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the channel.
    /// </summary>
    /// <value>Channel ID</value>
    public Guid Id { get; }
    /// <summary>
    /// Gets the identifier of the parent channel of this channel.
    /// </summary>
    /// <remarks>
    /// <para>This property is only present in threads. This can be used to determine if this is a thread, and as such, <see cref="IsThread" /> property exists.</para>
    /// </remarks>
    /// <value>Channel ID?</value>
    public Guid? ParentId { get; }
    /// <summary>
    /// Gets the identifier of the parent category of this channel.
    /// </summary>
    /// <value>Category ID?</value>
    public uint? CategoryId { get; }
    /// <summary>
    /// Gets the identifier of the parent group of this channel.
    /// </summary>
    /// <value>Group ID</value>
    public HashId GroupId { get; }
    /// <summary>
    /// Gets the identifier of the parent server of this channel.
    /// </summary>
    /// <value>Server ID</value>
    public HashId ServerId { get; }
    /// <summary>
    /// Gets the type of content the channel holds.
    /// </summary>
    /// <value>Channel type</value>
    public ChannelType Type { get; }
    /// <summary>
    /// Gets the name of the channel.
    /// </summary>
    /// <value>Name</value>
    public string Name { get; }
    /// <summary>
    /// Gets the topic describing what the channel is about.
    /// </summary>
    /// <value>Single-line description?</value>
    public string? Topic { get; }
    /// <summary>
    /// Gets whether the channel is globally viewable.
    /// </summary>
    /// <value>Channel is public?</value>
    public bool IsPublic { get; }
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the channel.
    /// </summary>
    /// <value>User ID</value>
    public HashId CreatedBy { get; }
    /// <summary>
    /// Gets the date when the channel was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }
    /// <summary>
    /// Gets the date when the channel was edited.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? UpdatedAt { get; }
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that archived the channel.
    /// </summary>
    /// <value>User ID</value>
    public HashId? ArchivedBy { get; }
    /// <summary>
    /// Gets the date when the channel was archived.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? ArchivedAt { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets whether the channel is a thread of <see cref="ChannelContent{TId, TServer}">a channel content</see>.
    /// </summary>
    /// <value>Channel is thread</value>
    public bool IsThread => ParentId is not null;
    /// <summary>
    /// Gets whether the channel has been archived.
    /// </summary>
    /// <value>Channel is archived</value>
    public bool IsArchived => ArchivedAt is not null;
    /// <summary>
    /// Gets whether the channel is in a category.
    /// </summary>
    /// <value>Channel is in a category</value>
    public bool IsCategorized => CategoryId is not null;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ServerChannel" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the channel</param>
    /// <param name="groupId">The identifier of the parent group of the channel</param>
    /// <param name="serverId">The identifier of the parentserver of the channel</param>
    /// <param name="type">The type of content channel holds</param>
    /// <param name="name">The name of the channel</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the channel</param>
    /// <param name="createdAt">The date when the channel was created</param>
    /// <param name="updatedAt">The date when the channel was edited</param>
    /// <param name="archivedBy">The identifier of <see cref="User">user</see> that archived the channel</param>
    /// <param name="archivedAt">The date when the channel was archived</param>
    /// <param name="topic">The topic describing what the channel is about</param>
    /// <param name="parentId">The identifier of the parent channel of the channel</param>
    /// <param name="categoryId">The identifier of the parent category of the channel</param>
    [JsonConstructor]
    public ServerChannel(
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
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) =>
        (Id, ParentId, CategoryId, GroupId, ServerId, Type, Name, Topic, CreatedBy, CreatedAt, UpdatedAt, ArchivedBy, ArchivedAt) = (id, parentId, categoryId, groupId, serverId, type, name, topic, createdBy, createdAt, updatedAt, archivedBy, archivedAt);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.DeleteChannelAsync(Guid)"/>
    public async Task DeleteAsync() =>
        await ParentClient.DeleteChannelAsync(Id);
    /// <inheritdoc cref="BaseGuildedClient.CreateWebhookAsync(HashId, Guid, string)" />
    /// <param name="name">The name of the webhook</param>
    public async Task<Webhook> CreateWebhookAsync(string name) =>
        await ParentClient.CreateWebhookAsync(ServerId, Id, name);
    #endregion
}