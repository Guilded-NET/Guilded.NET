using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="ServerChannel">channel</see>.
/// </summary>
/// <seealso cref="ChannelEvent" />
/// <seealso cref="GroupEvent" />
/// <seealso cref="RoleEvent" />
/// <seealso cref="ServerEvent" />
public class CategoryEvent : IModelHasId<uint>, ICreationDated, IUpdatableContent, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="ServerChannel">channel</see> received from the event.
    /// </summary>
    /// <value><see cref="ServerChannel">Channel</see></value>
    /// <seealso cref="ChannelEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServerId" />
    public Category Category { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="Category.Id" />
    public uint Id => Category.Id;

    /// <inheritdoc cref="Category.ServerId" />
    public HashId ServerId { get; }

    /// <inheritdoc cref="Category.GroupId" />
    public HashId GroupId => Category.GroupId;

    /// <inheritdoc cref="Category.Name" />
    public string Name => Category.Name;

    /// <inheritdoc cref="Category.CreatedAt" />
    public DateTime CreatedAt => Category.CreatedAt;

    /// <inheritdoc cref="Category.UpdatedAt" />
    public DateTime? UpdatedAt => Category.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Category.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Category.Updated" />
    public IObservable<CategoryEvent> Updated =>
        Category.Updated;

    /// <inheritdoc cref="Category.Deleted" />
    public IObservable<CategoryEvent> Deleted =>
        Category.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="CategoryEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the category event occurred</param>
    /// <param name="category">The <see cref="Servers.Category">category</see> received from the event</param>
    /// <returns>New <see cref="CategoryEvent" /> JSON instance</returns>
    /// <seealso cref="CategoryEvent" />
    [JsonConstructor]
    public CategoryEvent(
        [JsonProperty(Required = Required.Always)]
        Category category,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Category) = (serverId, category);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateCategoryAsync(HashId, uint, string)" />
    /// <param name="name">A new name of the <see cref="Servers.Category">category</see> (max — <c>100</c>)</param>
    public Task<Category> UpdateAsync(string name) =>
        Category.UpdateAsync(name);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteCategoryAsync(HashId, uint)" />
    public Task DeleteAsync() =>
        Category.DeleteAsync();

    /// <inheritdoc cref="Category.CreateChannelAsync(string, ChannelType, string?, bool?, Guid?, Guid?)" />
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that the <see cref="ServerChannel">channel</see> will hold</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <param name="parent">The identifier of the <see cref="ServerChannel">parent channel</see> where the <see cref="ServerChannel">thread</see> will be created</param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> from where the <see cref="ServerChannel">thread</see> will be created</param>
    public Task<ServerChannel> CreateChannelAsync(string name, ChannelType type = ChannelType.Chat, string? topic = null, bool? isPublic = null, Guid? parent = null, Guid? message = null) =>
        ParentClient.CreateChannelAsync(ServerId, name, type, topic, isPublic, GroupId, Id, parent, message);
    #endregion
}