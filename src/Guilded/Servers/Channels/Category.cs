using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a <see cref="ServerChannel">channel</see> category in a <see cref="Server">server</see>. 
/// </summary>
public class Category : ContentModel, IServerBased, IModelHasId<uint>
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Category">server category</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Category">server category</see></value>
    /// <seealso cref="Category"/> 
    /// <seealso cref="ServerId"/>
    /// <seealso cref="GroupId"/>
    /// <seealso cref="Name"/>
    public uint Id { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="Server">server</see> of the <see cref="Category">category</see>.
    /// </summary>
    /// <value>The identifier of the parent <see cref="Server">server</see> of the <see cref="Category">category</see></value>
    /// <seealso cref="Category"/>
    /// <seealso cref="Id"/>
    /// <seealso cref="GroupId"/>
    /// <seealso cref="Name"/>
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the parent <see cref="Group">group</see> of the <see cref="Category">category</see>.
    /// </summary>
    /// <value>The identifier of the parent <see cref="Group">group</see> of the <see cref="Category">category</see></value>
    /// <seealso cref="Category"/>
    /// <seealso cref="Id"/>
    /// <seealso cref="ServerId"/>
    /// <seealso cref="Name"/>
    public HashId GroupId { get; }

    /// <summary>
    /// Gets the displayed name of the <see cref="Category">category</see>.
    /// </summary>
    /// <value>The displayed name of the <see cref="Category">category</see></value>
    /// <seealso cref="Category"/>
    /// <seealso cref="Id"/>
    /// <seealso cref="ServerId"/>
    /// <seealso cref="GroupId"/>
    public string Name { get; }

    /// <summary>
    /// Gets the date when the <see cref="Category">category</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="Category">category</see> was created</value>
    /// <seealso cref="Category"/>
    /// <seealso cref="UpdatedAt"/>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date when the <see cref="Category">category</see> was edited.
    /// </summary>
    /// <value>The date when the <see cref="Category">category</see> was edited</value>
    /// <seealso cref="Category"/>
    /// <seealso cref="CreatedAt"/>
    public DateTime? UpdatedAt { get; }
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
    public IObservable<CategoryEvent> Updated =>
        ParentClient
            .CategoryUpdated
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets removed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="ServerChannel">channel</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="ServerChannel">channel</see> gets removed</returns>
    /// <seealso cref="Updated" />
    public IObservable<CategoryEvent> Deleted =>
        ParentClient
            .CategoryDeleted
            .HasId(Id)
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Category" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Category">server category</see></param>
    /// <param name="groupId">The identifier of the parent <see cref="Group">group</see> of the <see cref="Category">category</see></param>
    /// <param name="serverId">The identifier of the parent <see cref="Server">server</see> of the <see cref="Category">category</see></param>
    /// <param name="name">The displayed name of the <see cref="Category">category</see></param>
    /// <param name="createdAt">The date when the <see cref="Category">category</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="Category">category</see> was edited</param>
    /// <returns>New <see cref="Category" /> JSON instance</returns>
    /// <seealso cref="Category" />
    [JsonConstructor]
    public Category(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        HashId groupId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (Id, GroupId, ServerId, Name, CreatedAt, UpdatedAt) = (id, groupId, serverId, name, createdAt, updatedAt);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateCategoryAsync(HashId, uint, string?, int?)" />
    /// <param name="name">A new name of the <see cref="Category">category</see> (max — <c>100</c>)</param>
    /// <param name="priority">A new position of the <see cref="Category">category</see> (max — <c>100</c>)</param>
    public Task<Category> UpdateAsync(string? name = null, int? priority = null) =>
        ParentClient.UpdateCategoryAsync(ServerId, Id, name, priority);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteCategoryAsync(HashId, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteCategoryAsync(ServerId, Id);
    #endregion

    #region Channels
    /// <summary>
    /// Creates a new <see cref="ServerChannel">channel</see> in the <see cref="Category">category</see>.
    /// </summary>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that the <see cref="ServerChannel">channel</see> will hold</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <param name="parent">The identifier of the <see cref="ServerChannel">parent channel</see> where the <see cref="ServerChannel">thread</see> will be created</param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> from where the <see cref="ServerChannel">thread</see> will be created</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> CreateChannelAsync(string name, ChannelType type = ChannelType.Chat, string? topic = null, bool? isPublic = null, Guid? parent = null, Guid? message = null) =>
        ParentClient.CreateChannelAsync(ServerId, name, type, topic, isPublic, GroupId, Id, parent, message);
    #endregion
}