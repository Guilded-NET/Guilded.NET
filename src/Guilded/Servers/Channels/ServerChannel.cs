using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a navigatable item that contains content.
/// </summary>
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
//[JsonConverter(typeof(ServerChannelConverter))]
public class ServerChannel : ContentModel, IModelHasId<Guid>, ICreatableContent, IChannel, IServerBased, IArchivableContent
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

    #region Properties
    /// <inheritdoc />
    public Guid Id { get; }

    /// <summary>
    /// Gets the identifier of the parent channel of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <remarks>
    /// <para>This property is only present in threads. This can be used to determine if this is a thread, and as such, <see cref="IsThread" /> property exists.</para>
    /// </remarks>
    /// <value>The identifier of the parent channel of the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="RootId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public Guid? ParentId { get; }

    /// <summary>
    /// Gets the identifier of the ancestor channel of the <see cref="ServerChannel">channel</see> that exist at the <see cref="Group">group</see> level.
    /// </summary>
    /// <remarks>
    /// <para>This property is only present in threads. This can be used to determine if this is a thread, and as such, <see cref="IsThread" /> property exists.</para>
    /// </remarks>
    /// <value>The identifier of the ancestor channel of the <see cref="ServerChannel">channel</see> that exist at the <see cref="Group">group</see> level</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public Guid? RootId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Message">message</see> that hosts the thread.
    /// </summary>
    /// <remarks>
    /// <para>This property is only present in threads. This can be used to determine if this is a thread, and as such, <see cref="IsThread" /> property exists.</para>
    /// </remarks>
    /// <value>The identifier of the <see cref="Message">message</see> that hosts the thread</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public Guid? MessageId { get; }

    /// <summary>
    /// Gets the identifier of the parent category of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value>The identifier of the parent category of the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="GroupId" />
    /// <seealso cref="ServerId" />
    public uint? CategoryId { get; }

    /// <summary>
    /// Gets the identifier of the parent group of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value>The identifier of the parent group of the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="ServerId" />
    public HashId GroupId { get; }

    /// <summary>
    /// Gets the identifier of the parent server of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value>The identifier of the parent server of the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Id" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="GroupId" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the type of content the <see cref="ServerChannel">channel</see> holds.
    /// </summary>
    /// <value>The type of content the <see cref="ServerChannel">channel</see> holds</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="IsPublic" />
    public ChannelType Type { get; }

    /// <summary>
    /// Gets the name of the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value>The name of the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Topic" />
    public string Name { get; }

    /// <summary>
    /// Gets the topic describing what the <see cref="ServerChannel">channel</see> is about.
    /// </summary>
    /// <value>The topic describing what the <see cref="ServerChannel">channel</see> is about</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Name" />
    public string? Topic { get; }

    /// <summary>
    /// Gets whether the <see cref="ServerChannel">channel</see> is globally viewable.
    /// </summary>
    /// <value>Whether the <see cref="ServerChannel">channel</see> is globally viewable</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="Type" />
    public bool IsPublic { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value>The identifier of <see cref="User">user</see> that created the <see cref="ServerChannel">channel</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="ServerChannel">channel</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="ServerChannel">channel</see> was created</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date when the <see cref="ServerChannel">channel</see> was edited.
    /// </summary>
    /// <value>The date when the <see cref="ServerChannel">channel</see> was edited</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="ArchivedAt" />
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that archived the <see cref="ServerChannel">channel</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    public HashId? ArchivedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="ServerChannel">channel</see> was archived.
    /// </summary>
    /// <value>The date when the <see cref="ServerChannel">channel</see> was archived</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    public DateTime? ArchivedAt { get; }

    /// <summary>
    /// Gets whether the <see cref="ServerChannel">channel</see> is a thread of a <see cref="ChannelContent{TId, TServer}">channel content</see>.
    /// </summary>
    /// <value>Whether the <see cref="ServerChannel">channel</see> is a thread of a <see cref="ChannelContent{TId, TServer}">channel content</see></value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ParentId" />
    /// <seealso cref="IsArchived" />
    /// <seealso cref="IsCategorized" />
    [MemberNotNullWhen(true, nameof(ParentId))]
    public bool IsThread => ParentId is not null;

    /// <summary>
    /// Gets whether the <see cref="ServerChannel">channel</see> is in a category.
    /// </summary>
    /// <value>Whether the <see cref="ServerChannel">channel</see> is in a category</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="CategoryId" />
    /// <seealso cref="IsThread" />
    /// <seealso cref="IsArchived" />
    [MemberNotNullWhen(true, nameof(CategoryId))]
    public bool IsCategorized => CategoryId is not null;

    /// <summary>
    /// Gets whether the <see cref="ServerChannel">channel</see> is archived.
    /// </summary>
    /// <value>Whether the <see cref="ServerChannel">channel</see> is archived</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="IsThread" />
    /// <seealso cref="IsCategorized" />
    [MemberNotNullWhen(true, nameof(ArchivedAt), nameof(ArchivedBy))]
    public bool IsArchived => ArchivedAt is not null;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="ServerChannel">channel</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<ChannelEvent> Updated =>
        ParentClient
            .ChannelUpdated
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets removed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="ServerChannel">channel</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets removed</returns>
    /// <seealso cref="Updated" />
    public IObservable<ChannelEvent> Deleted =>
        ParentClient
            .ChannelDeleted
            .HasId(Id)
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ServerChannel" /> from the specified JSON properties.
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
        Guid? rootId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? messageId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) =>
        (Id, ParentId, RootId, MessageId, CategoryId, GroupId, ServerId, Type, Name, Topic, CreatedBy, CreatedAt, UpdatedAt, ArchivedBy, ArchivedAt) = (id, parentId, rootId, messageId, categoryId, groupId, serverId, type, name, topic, createdBy, createdAt, updatedAt, archivedBy, archivedAt);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.DeleteChannelAsync(Guid)" />
    public Task<ServerChannel> UpdateAsync(string? name = null, string? topic = null, bool? isPublic = null) =>
        ParentClient.UpdateChannelAsync(Id, name, topic, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteChannelAsync(Guid)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteChannelAsync(Id);

    /// <inheritdoc cref="AbstractGuildedClient.CreateWebhookAsync(HashId, Guid, string)" />
    /// <param name="name">The name of the <see cref="Webhook">webhook</see></param>
    public Task<Webhook> CreateWebhookAsync(string name) =>
        ParentClient.CreateWebhookAsync(ServerId, Id, name);
    #endregion
}