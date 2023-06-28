using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Client;
using Guilded.Users;
using Newtonsoft.Json;
using Guilded.Events;
using System.Reactive.Linq;

namespace Guilded.Servers;

/// <summary>
/// Represents a sub-server/sub-team/group of channels in Guilded.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="ServerType" />
public class Group : ContentModel, IModelHasId<HashId>, ICreatableContent, IUserCreated, IServerBased, IArchivableContent
{
    #region Properties Identification
    /// <summary>
    /// Gets the identifier of the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Name" />
    public HashId Id { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> is</value>
    /// <seealso cref="Group" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the displayed name of the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The displayed name of the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="Description" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Id" />
    public string Name { get; }

    /// <summary>
    /// Gets the description of the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The description of the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="Name" />
    /// <seealso cref="Avatar" />
    public string? Description { get; }

    /// <summary>
    /// Gets the <see cref="Uri">URL</see> to the icon image of the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The <see cref="Uri">URL</see> to the icon image of the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    public Uri? Avatar { get; }

    /// <summary>
    /// Gets the <see cref="Emote">emote</see> icon of the <see cref="Group">group</see>.
    /// </summary>
    /// <remarks>
    /// <para>This is displayed at the bottom right corner of the <see cref="Avatar">group's avatar</see>.</para>
    /// </remarks>
    /// <value>The <see cref="Emote">emote</see> icon of the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="Name" />
    /// <seealso cref="Description" />
    public uint? EmoteId { get; }
    #endregion

    #region Properties Attributes
    /// <summary>
    /// Gets whether the <see cref="Group">group</see> is globally viewable and doesn't need permissions.
    /// </summary>
    /// <value>Whether the <see cref="Group">group</see> is globally viewable and doesn't need permissions</value>
    /// <seealso cref="Group" />
    /// <seealso cref="IsHome" />
    public bool IsPublic { get; }

    /// <summary>
    /// Gets whether the <see cref="Group">group</see> is main group of the <see cref="Server">server</see>.
    /// </summary>
    /// <value>Whether the <see cref="Group">group</see> is main group of the <see cref="Server">server</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="IsPublic" />
    public bool IsHome { get; }
    #endregion

    #region Properties When, who
    /// <summary>
    /// Gets the date when the <see cref="Group">group</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="Group">group</see> was created</value>
    /// <seealso cref="Group" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedBy" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> who created the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> who created the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="CreatedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="Group">group</see> was last updated.
    /// </summary>
    /// <value>The date when the <see cref="Group">group</see> was last updated</value>
    /// <seealso cref="Group" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="CreatedBy" />
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> who last updated the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> who last updated the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedAt" />
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="Group">group</see> was archived.
    /// </summary>
    /// <value>The date when the <see cref="Group">group</see> was archived</value>
    /// <seealso cref="Group" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="ArchivedBy" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="CreatedBy" />
    public DateTime? ArchivedAt { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> who archived the <see cref="Group">group</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> who archived the <see cref="Group">group</see></value>
    /// <seealso cref="Group" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedBy" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="CreatedAt" />
    public HashId? ArchivedBy { get; }

    /// <summary>
    /// Gets whether the <see cref="Group">group</see> is archived.
    /// </summary>
    /// <value>Whether the <see cref="ServerChannel">channel</see> is archived</value>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ArchivedAt" />
    /// <seealso cref="ArchivedBy" />
    [MemberNotNullWhen(true, nameof(ArchivedAt), nameof(ArchivedBy))]
    public bool IsArchived => ArchivedAt is not null;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Group">group</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Group">group</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Group">group</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<GroupEvent> Updated =>
        ParentClient
            .GroupUpdated
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Group">group</see> gets removed.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Group">group</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Group">group</see> gets removed</returns>
    /// <seealso cref="Updated" />
    public IObservable<GroupEvent> Deleted =>
        ParentClient
            .GroupDeleted
            .HasId(Id)
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Group" /> from specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Group">group</see></param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> is</param>
    /// <param name="name">The displayed name of the <see cref="Group">group</see></param>
    /// <param name="createdAt">The date when the <see cref="Group">group</see> was created</param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> who created the <see cref="Group">group</see></param>
    /// <param name="updatedAt">The date when the <see cref="Group">group</see> was last updated</param>
    /// <param name="updatedBy">The <see cref="User">user</see> who last updated the <see cref="Group">group</see></param>
    /// <param name="archivedAt">The date when the <see cref="Group">group</see> was archived</param>
    /// <param name="archivedBy">The <see cref="User">user</see> who archived the <see cref="Group">group</see></param>
    /// <param name="description">The description of the <see cref="Server">server</see></param>
    /// <param name="avatar">The <see cref="Uri">URL</see> to the icon image of the <see cref="Group">group</see></param>
    /// <param name="emoteId">The <see cref="Emote">emote</see> icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether the <see cref="Group">group</see> is globally viewable and doesn't need permissions</param>
    /// <param name="isHome">Whether the <see cref="Group">group</see> is main group of the <see cref="Server">server</see></param>
    /// <returns><see cref="Group" /> from JSON</returns>
    [JsonConstructor]
    public Group(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? archivedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? archivedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? emoteId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isPublic = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isHome = false
    ) =>
        (Id, ServerId, Name, Avatar, EmoteId, Description, IsPublic, IsHome, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, ArchivedAt, ArchivedBy) = (id, serverId, name, avatar, emoteId, description, isPublic, isHome, createdAt, createdBy, updatedAt, updatedBy, archivedAt, archivedBy);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateGroupAsync(HashId, HashId, string?, string?, uint?, bool?)" />
    /// <param name="name">The new name of the <see cref="Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Group">group</see></param>
    public Task<Group> UpdateAsync(string? name = null, string? description = null, uint? emote = null, bool? isPublic = null) =>
        ParentClient.UpdateGroupAsync(ServerId, Id, name, description, emote, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateGroupAsync(HashId, HashId, string?, string?, uint?, bool?)" />
    /// <param name="name">The new name of the <see cref="Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Group">group</see></param>
    public Task<Group> UpdateAsync(string? name = null, string? description = null, Emote? emote = null, bool? isPublic = null) =>
        ParentClient.UpdateGroupAsync(ServerId, Id, name, description, emote, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteGroupAsync(HashId, HashId)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteGroupAsync(ServerId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddMembershipAsync(HashId, HashId)" />
    /// <param name="member">The identifier of the <see cref="Member">member</see> to add</param>
    public Task AddMembershipAsync(HashId member) =>
        ParentClient.AddMembershipAsync(Id, member);

    /// <inheritdoc cref="AbstractGuildedClient.AddMembershipAsync(HashId, HashId)" />
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to add</param>
    public Task AddMembershipAsync(UserReference memberReference) =>
        ParentClient.AddMembershipAsync(Id, memberReference);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMembershipAsync(HashId, HashId)" />
    /// <param name="member">The identifier of the <see cref="Member">member</see> to remove</param>
    public Task RemoveMembershipAsync(HashId member) =>
        ParentClient.RemoveMembershipAsync(Id, member);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMembershipAsync(HashId, HashId)" />
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to remove</param>
    public Task RemoveMembershipAsync(UserReference memberReference) =>
        ParentClient.RemoveMembershipAsync(Id, memberReference);
    #endregion
}