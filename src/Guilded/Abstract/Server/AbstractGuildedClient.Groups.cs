using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Group">group</see> is created.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupUpdated" />
    /// <seealso cref="GroupDeleted" />
    public IObservable<GroupEvent> GroupCreated => ((IEventInfo<GroupEvent>)GuildedEvents["GroupCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Group">group</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupCreated" />
    /// <seealso cref="GroupDeleted" />
    public IObservable<GroupEvent> GroupUpdated => ((IEventInfo<GroupEvent>)GuildedEvents["GroupUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Group">group</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>GroupDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="GroupCreated" />
    /// <seealso cref="GroupUpdated" />
    public IObservable<GroupEvent> GroupDeleted => ((IEventInfo<GroupEvent>)GuildedEvents["GroupDeleted"]).Observable;
    #endregion

    #region Methods REST
    /// <summary>
    /// Gets a list of <see cref="Group">groups</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Group">groups</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Group">group</see> in the specified <paramref name="server" /></returns>
    public Task<IList<Group>> GetGroupsAsync(HashId server) =>
        GetResponsePropertyAsync<IList<Group>>(new RestRequest($"servers/{server}/groups", Method.Get), "groups");

    /// <summary>
    /// Gets the specified <paramref name="group" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> is</param>
    /// <param name="group">The identifier of the <see cref="Group">group</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Group">group</see> that was specified in the arguments</returns>
    public Task<Group> GetGroupAsync(HashId server, HashId group) =>
        GetResponsePropertyAsync<Group>(new RestRequest($"servers/{server}/groups/{group}", Method.Get), "group");

    /// <summary>
    /// Creates a new <see cref="Group">group</see> in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be created</param>
    /// <param name="name">The name of the <see cref="Group">group</see></param>
    /// <param name="description">The description of the <see cref="Group">group</see></param>
    /// <param name="emote">The emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone can join the <see cref="Group">group</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageGroups" />
    /// <returns>The <see cref="Group">group</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Group> CreateGroupAsync(HashId server, string name, string? description = null, uint? emote = null, bool isPublic = false) =>
        GetResponsePropertyAsync<Group>(new RestRequest($"servers/{server}/groups", Method.Post)
            .AddBody(new
            {
                name,
                description,
                emote,
                isPublic
            })
        , "group");

    /// <inheritdoc cref="CreateGroupAsync(HashId, string, string, uint?, bool)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be created</param>
    /// <param name="name">The name of the <see cref="Group">group</see></param>
    /// <param name="description">The description of the <see cref="Group">group</see></param>
    /// <param name="emote">The emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone can join the <see cref="Group">group</see></param>
    public Task<Group> CreateGroupAsync(HashId server, string name, string? description = null, Emote? emote = null, bool isPublic = false) =>
        CreateGroupAsync(server, name, description, emote?.Id, isPublic);

    /// <summary>
    /// Updates the specified <paramref name="group" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="group">The identifier of the <see cref="Group">group</see> to update</param>
    /// <param name="name">The new name of the <see cref="Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Group">group</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    /// <returns>The <see cref="Group">group</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Group> UpdateGroupAsync(HashId server, HashId group, string? name = null, string? description = null, uint? emote = null, bool? isPublic = null)
    {
        return GetResponsePropertyAsync<Group>(new RestRequest($"servers/{server}/groups/{group}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                description,
                emoteId = emote,
                isPublic
            })
        , "group");
    }

    /// <inheritdoc cref="UpdateGroupAsync(HashId, HashId, string?, string?, uint?, bool?)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="group">The identifier of the <see cref="Group">group</see> to update</param>
    /// <param name="name">The new name of the <see cref="Group">group</see></param>
    /// <param name="description">The new description of the <see cref="Group">group</see></param>
    /// <param name="emote">The new emote icon of the <see cref="Group">group</see></param>
    /// <param name="isPublic">Whether anyone should be able to join the <see cref="Group">group</see></param>
    public Task<Group> UpdateGroupAsync(HashId server, HashId group, string? name = null, string? description = null, Emote? emote = null, bool? isPublic = null) =>
        UpdateGroupAsync(server, group, name, description, emote?.Id, isPublic);

    /// <summary>
    /// Deletes the specified <paramref name="group" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be deleted</param>
    /// <param name="group">The identifier of the <see cref="Group">group</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    public Task DeleteGroupAsync(HashId server, HashId group) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/groups/{group}", Method.Delete));

    /// <summary>
    /// Adds the <paramref name="member" /> to a <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This allows the <paramref name="member" /> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    public Task AddMembershipAsync(HashId group, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Put));

    /// <summary>
    /// Adds the <paramref name="memberReference">referenced member</paramref> to a <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This allows the <paramref name="memberReference">referenced member</paramref> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    public Task AddMembershipAsync(HashId group, UserReference memberReference) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/@{memberReference.ToString().ToLower()}", Method.Put));

    /// <summary>
    /// Removes the <paramref name="member" /> from a <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This disallows the <paramref name="member" /> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    public Task RemoveMembershipAsync(HashId group, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/{member}", Method.Delete));

    /// <summary>
    /// Removes the <paramref name="memberReference">referenced member</paramref> from the <paramref name="group" />.
    /// </summary>
    /// <remarks>
    /// <para>This disallows the <paramref name="memberReference">referenced member</paramref> to interact or see the specified group.</para>
    /// </remarks>
    /// <param name="group">The identifier of the parent group</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="Member">member</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageGroups" />
    public Task RemoveMembershipAsync(HashId group, UserReference memberReference) =>
        ExecuteRequestAsync(new RestRequest($"groups/{group}/members/@{memberReference.ToString().ToLower()}", Method.Delete));
    #endregion
}