using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Servers.Group">group</see>.
/// </summary>
/// <seealso cref="ChannelEvent" />
/// <seealso cref="RoleEvent" />
/// <seealso cref="ServerEvent" />
public class GroupEvent : IModelHasId<HashId>, ICreatableContent, IUpdatableContent, IArchivableContent, IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Servers.Group">group</see> received from the event.
    /// </summary>
    /// <value>The <see cref="Servers.Group">group</see> received from the event</value>
    /// <seealso cref="GroupEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="ServerId" />
    public Group Group { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="Group.Id" />
    public HashId Id => Group.Id;

    /// <inheritdoc />
    public HashId ServerId { get; }

    /// <inheritdoc cref="Group.Name" />
    public string Name => Group.Name;

    /// <inheritdoc cref="Group.Description" />
    public string? Description => Group.Description;

    /// <inheritdoc cref="Group.Avatar" />
    public Uri? Avatar => Group.Avatar;

    /// <inheritdoc cref="Group.EmoteId" />
    public uint? EmoteId => Group.EmoteId;

    /// <inheritdoc cref="Group.CreatedBy" />
    public HashId CreatedBy => Group.CreatedBy;

    /// <inheritdoc cref="Group.CreatedAt" />
    public DateTime CreatedAt => Group.CreatedAt;

    /// <inheritdoc cref="Group.UpdatedAt" />
    public DateTime? UpdatedAt => Group.UpdatedAt;

    /// <inheritdoc cref="Group.ArchivedBy" />
    public HashId? ArchivedBy => Group.ArchivedBy;

    /// <inheritdoc cref="Group.ArchivedAt" />
    public DateTime? ArchivedAt => Group.ArchivedAt;

    /// <inheritdoc cref="Group.IsArchived" />
    public bool IsArchived => Group.IsArchived;

    /// <inheritdoc cref="Group.IsPublic" />
    public bool IsPublic => Group.IsPublic;

    /// <inheritdoc cref="Group.IsHome" />
    public bool IsHome => Group.IsHome;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Group.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Group.Updated" />
    public IObservable<GroupEvent> Updated =>
        Group.Updated;

    /// <inheritdoc cref="Group.Deleted" />
    public IObservable<GroupEvent> Deleted =>
        Group.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GroupEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="GroupEvent">group event</see> occurred</param>
    /// <param name="group">The <see cref="Servers.Group">group</see> received from the event</param>
    /// <returns>New <see cref="GroupEvent" /> JSON instance</returns>
    /// <seealso cref="GroupEvent" />
    [JsonConstructor]
    public GroupEvent(
        [JsonProperty(Required = Required.Always)]
        Group group,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Group) = (serverId, group);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateGroupAsync(HashId, HashId, string?, string?, uint?, bool?)" />
    /// <param name="name">The new name of the <see cref="Servers.Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Servers.Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Servers.Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Servers.Group">group</see></param>
    public Task<Group> UpdateAsync(string? name = null, string? description = null, uint? emote = null, bool? isPublic = null) =>
        Group.UpdateAsync(name, description, emote, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateGroupAsync(HashId, HashId, string?, string?, uint?, bool?)" />
    /// <param name="name">The new name of the <see cref="Servers.Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Servers.Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Servers.Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Servers.Group">group</see></param>
    public Task<Group> UpdateAsync(string? name = null, string? description = null, Emote? emote = null, bool? isPublic = null) =>
        Group.UpdateAsync(name, description, emote, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteGroupAsync(HashId, HashId)" />
    public Task DeleteAsync() =>
        Group.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.AddMembershipAsync(HashId, HashId)" />
    /// <param name="member">The identifier of the <see cref="Member">member</see> to add</param>
    public Task AddMembershipAsync(HashId member) =>
        Group.AddMembershipAsync(member);

    /// <inheritdoc cref="AbstractGuildedClient.AddMembershipAsync(HashId, HashId)" />
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to add</param>
    public Task AddMembershipAsync(UserReference memberReference) =>
        Group.AddMembershipAsync(memberReference);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMembershipAsync(HashId, HashId)" />
    /// <param name="member">The identifier of the <see cref="Member">member</see> to remove</param>
    public Task RemoveMembershipAsync(HashId member) =>
        Group.RemoveMembershipAsync(member);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMembershipAsync(HashId, HashId)" />
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to remove</param>
    public Task RemoveMembershipAsync(UserReference memberReference) =>
        Group.RemoveMembershipAsync(memberReference);
    #endregion
}