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
    #region Constants
    /// <summary>
    /// The count of how many <see cref="char">characters</see> there can be in <see cref="Name">channel's name</see>.
    /// </summary>
    /// <value>Limit</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Name" />
    /// <seealso cref="TopicLimit" />
    public const short NameLimit = 4000;
    /// <summary>
    /// The count of how many <see cref="char">characters</see> there can be in <see cref="Topic">channel's topic</see>.
    /// </summary>
    /// <value>Limit</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Topic" />
    /// <seealso cref="NameLimit" />
    public const short TopicLimit = 4000;
    #endregion

    #region JSON properties
    /// <summary>
    /// Gets the identifier of the channel.
    /// </summary>
    /// <value><see cref="Id">Channel ID</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public Guid Id { get; }
    /// <summary>
    /// Gets the identifier of the parent channel of this channel.
    /// </summary>
    /// <remarks>
    /// <para>This property is only present in threads. This can be used to determine if this is a thread, and as such, <see cref="IsThread" /> property exists.</para>
    /// </remarks>
    /// <value><see cref="Id">Channel ID</see>?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public Guid? ParentId { get; }
    /// <summary>
    /// Gets the identifier of the parent category of this channel.
    /// </summary>
    /// <value>Category ID?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public uint? CategoryId { get; }
    /// <summary>
    /// Gets the identifier of the parent group of this channel.
    /// </summary>
    /// <value>Group ID</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="ServerId" />
    public HashId GroupId { get; }
    /// <summary>
    /// Gets the identifier of the parent server of this channel.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    public HashId ServerId { get; }
    /// <summary>
    /// Gets the type of content the channel holds.
    /// </summary>
    /// <value>Channel type</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="IsPublic" />
    public ChannelType Type { get; }
    /// <summary>
    /// Gets the name of the channel.
    /// </summary>
    /// <value>Name</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Topic" />
    public string Name { get; }
    /// <summary>
    /// Gets the topic describing what the channel is about.
    /// </summary>
    /// <value>Single-line description?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Name" />
    public string? Topic { get; }
    /// <summary>
    /// Gets whether the channel is globally viewable.
    /// </summary>
    /// <value>Channel is public?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Type" />
    public bool IsPublic { get; }
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the channel.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public HashId CreatedBy { get; }
    /// <summary>
    /// Gets the date when the channel was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public DateTime CreatedAt { get; }
    /// <summary>
    /// Gets the date when the channel was edited.
    /// </summary>
    /// <value>Date?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public DateTime? UpdatedAt { get; }
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that archived the channel.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    public HashId? ArchivedBy { get; }
    /// <summary>
    /// Gets the date when the channel was archived.
    /// </summary>
    /// <value>Date?</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    public DateTime? ArchivedAt { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets whether the channel is a thread of <see cref="ChannelContent{TId, TServer}">a channel content</see>.
    /// </summary>
    /// <value>Channel is thread</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="IsArchived" />
    /// <seealso cref="IsCategorized" />
    public bool IsThread => ParentId is not null;
    /// <summary>
    /// Gets whether the channel has been archived.
    /// </summary>
    /// <value>Channel is archived</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="IsThread" />
    /// <seealso cref="IsCategorized" />
    public bool IsArchived => ArchivedAt is not null;
    /// <summary>
    /// Gets whether the channel is in a category.
    /// </summary>
    /// <value>Channel is in a category</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="IsThread" />
    /// <seealso cref="IsArchived" />
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
    /// <returns>New <see cref="ServerChannel" /> JSON instance</returns>
    /// <seealso cref="ServerChannel" />
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
    /// <inheritdoc cref="BaseGuildedClient.DeleteChannelAsync(Guid)" />
    public async Task DeleteAsync() =>
        await ParentClient.DeleteChannelAsync(Id);
    /// <inheritdoc cref="BaseGuildedClient.CreateWebhookAsync(HashId, Guid, string)" />
    /// <param name="name">The name of <see cref="Servers.Webhook">the webhook</see></param>
    public async Task<Webhook> CreateWebhookAsync(string name) =>
        await ParentClient.CreateWebhookAsync(ServerId, Id, name);
    #endregion
}