using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods Servers specifically
    /// <summary>
    /// Gets the specified <see cref="Server">server</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Server">server</see> that was specified in the arguments</returns>
    public Task<Server> GetServerAsync(HashId server) =>
        GetResponsePropertyAsync<Server>(new RestRequest($"servers/{server}", Method.Get), "server");
    #endregion

    #region Methods Server subscriptions
    /// <summary>
    /// Gets a list of <see cref="SubscriptionTier">subscription tiers</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="SubscriptionTier">subscription tiers</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="SubscriptionTier">server subscription tiers</see> in the specified <paramref name="server" /></returns>
    public Task<IList<SubscriptionTier>> GetSubscriptionTiersAsync(HashId server) =>
        GetResponsePropertyAsync<IList<SubscriptionTier>>(new RestRequest($"servers/{server}/subscriptions/tiers", Method.Get), "serverSubscriptionTiers");

    /// <summary>
    /// Gets the specified <see cref="SubscriptionTier">server subscription tier</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="SubscriptionTier">subscription tier</see> from</param>
    /// <param name="type">The <see cref="SubscriptionType">subscription tier type</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="SubscriptionTier">server subscription tier</see> that was specified in the arguments</returns>
    public Task<SubscriptionTier> GetSubscriptionTierAsync(HashId server, SubscriptionType type) =>
        GetResponsePropertyAsync<SubscriptionTier>(new RestRequest($"servers/{server}/subscriptions/tiers/{type}", Method.Get), "serverSubscriptionTier");
    #endregion

    #region Methods Groups
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

    #region Methods Roles
    /// <summary>
    /// Gets a list of <see cref="Role">roles</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Role">roles</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Role">role</see> in the specified <paramref name="server" /></returns>
    public Task<IList<Role>> GetRolesAsync(HashId server) =>
        GetResponsePropertyAsync<IList<Role>>(new RestRequest($"servers/{server}/roles", Method.Get), "roles");

    /// <summary>
    /// Gets the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> is</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Role">role</see> that was specified in the arguments</returns>
    public Task<Role> GetRoleAsync(HashId server, uint role) =>
        GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}", Method.Get), "role");

    /// <summary>
    /// Creates a new <see cref="Role">role</see> in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be created</param>
    /// <param name="name">The name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> CreateRoleAsync(HashId server, string name, bool isDisplayedSeparately = false, bool isSelfAssignable = false, bool isMentionable = false, IList<uint>? colors = null, IList<Permission>? permissions = null) =>
        GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles", Method.Post)
            .AddBody(new
            {
                name,
                isDisplayedSeparately,
                isSelfAssignable,
                isMentionable,
                permissions,
                colors,
            })
        , "role");

    /// <inheritdoc cref="CreateRoleAsync(HashId, string, bool, bool, bool, IList{uint}?, IList{Permission}?)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be created</param>
    /// <param name="name">The name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    public Task<Role> CreateRoleAsync(HashId server, string name, bool isDisplayedSeparately = false, bool isSelfAssignable = false, bool isMentionable = false, IList<Color>? colors = null, IList<Permission>? permissions = null) =>
        CreateRoleAsync(server, name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors?.Select(color => (uint)color.ToArgb()).ToList(), permissions);

    /// <summary>
    /// Updates the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update</param>
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> UpdateRoleAsync(HashId server, uint role, string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<uint>? colors = null, IList<Permission>? permissions = null)
    {
        return GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                isDisplayedSeparately,
                isSelfAssignable,
                isMentionable,
                colors,
                permissions,
            })
        , "role");
    }

    /// <inheritdoc cref="UpdateRoleAsync(HashId, uint, string?, bool?, bool?, bool?, IList{uint}?, IList{Permission}?)" />
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Group">group</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update</param>
    /// <param name="name">The new name of the <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="colors">The new displayed colours of the <see cref="Role">role</see></param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    public Task<Role> UpdateRoleAsync(HashId server, uint role, string? name = null, bool? isDisplayedSeparately = null, bool? isSelfAssignable = null, bool? isMentionable = null, IList<Color>? colors = null, IList<Permission>? permissions = null) =>
        UpdateRoleAsync(server, role, name, isDisplayedSeparately, isSelfAssignable, isMentionable, colors?.Select(color => (uint)color.ToArgb()).ToList(), permissions);

    /// <summary>
    /// Deletes the specified <paramref name="role" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be deleted</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task DeleteRoleAsync(HashId server, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/roles/{role}", Method.Delete));

    /// <summary>
    /// Updates the specified <paramref name="role">role's</paramref> <see cref="Permission">permissions</see>.
    /// </summary>
    /// <remarks>
    /// <para>Any permissions that exists in the role, but are not specified will be either left in the role or not added at all if it didn't exist prior.</para>
    /// <para>To remove a permission, specify it with <see langword="false" />. Otherwise, to add a permission, specify it as <see langword="true" />.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> will be updated</param>
    /// <param name="role">The identifier of the <see cref="Role">role</see> to update <see cref="Permission">permissions</see> of </param>
    /// <param name="permissions">The new <see cref="Permission">permissions</see> of the <see cref="Role">role</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    /// <returns>The <see cref="Role">role</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Role> UpdateRolePermissionsAsync(HashId server, uint role, IDictionary<Permission, bool> permissions)
    {
        return GetResponsePropertyAsync<Role>(new RestRequest($"servers/{server}/roles/{role}/permissions", Method.Patch)
            .AddJsonBody(new
            {
                permissions,
            })
        , "role");
    }
    #endregion

    #region Methods Members
    /// <summary>
    /// Gets the list of all <paramref name="server" /> <see cref="Member">members</see>.
    /// </summary>
    /// <param name="server">The server to get <see cref="Member">member</see> list of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Member">members</see> in the specified <paramref name="server" /></returns>
    public Task<IList<MemberSummary>> GetMembersAsync(HashId server) =>
        TransformListResponseAsync(new RestRequest($"servers/{server}/members", Method.Get), "members", x =>
        {
            // Add serverId property to them
            x.Add("serverId", JValue.CreateString(server.ToString()));
            return x.ToObject<MemberSummary>(GuildedSerializer)!;
        });

    /// <summary>
    /// Gets full information about the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server where the <see cref="Member">member</see> is</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The specified <see cref="Member">member</see></returns>
    public Task<Member> GetMemberAsync(HashId server, HashId member) =>
        TransformResponseAsync<Member>(new RestRequest($"servers/{server}/members/{member}", Method.Get), "member", value =>
        {
            value.Add("serverId", JValue.CreateString(server.ToString()));

            return value;
        });

    /// <summary>
    /// Gets full information about the <paramref name="memberReference">referenced member</paramref>.
    /// </summary>
    /// <param name="server">The server where the <see cref="Member">member</see> is</param>
    /// <param name="memberReference">The identifier of the <see cref="Member">member</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The specified <see cref="Member">member</see> by <paramref name="memberReference">reference</paramref></returns>
    public Task<Member> GetMemberAsync(HashId server, UserReference memberReference) =>
        TransformResponseAsync<Member>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}", Method.Get), "member", value =>
        {
            value.Add("serverId", JValue.CreateString(server.ToString()));

            return value;
        });

    /// <summary>
    /// Gets the list of roles the specified <paramref name="member" /> holds.
    /// </summary>
    /// <remarks>
    /// <para>No permissions are required.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch user's information</param>
    /// <param name="member">The identifier of the role holder</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>List of role IDs</returns>
    public Task<IList<uint>> GetMemberRolesAsync(HashId server, HashId member) =>
        GetResponsePropertyAsync<IList<uint>>(new RestRequest($"servers/{server}/members/{member}/roles", Method.Get), "roleIds");

    /// <summary>
    /// Gets the list of roles the <paramref name="memberReference">referenced member</paramref> holds.
    /// </summary>
    /// <remarks>
    /// <para>No permissions are required.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch <see cref="User">user</see>'s information</param>
    /// <param name="memberReference">A <see cref="UserReference">user reference</see> of the role holder</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>List of role IDs</returns>
    public Task<IList<uint>> GetMemberRolesAsync(HashId server, UserReference memberReference) =>
        GetResponsePropertyAsync<IList<uint>>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/roles", Method.Get), "roleIds");

    /// <summary>
    /// Changes the <see cref="Member.Nickname">nickname</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="member">The identifier of the member to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageNicknames">Required when deleting nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="Permission.ManageOwnNicknames">Required when deleting the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    /// <returns>Updated <see cref="Member.Nickname">nickname</see></returns>
    public Task<string> SetNicknameAsync(HashId server, HashId member, string nickname) =>
        string.IsNullOrWhiteSpace(nickname)
        ? throw new ArgumentNullException(nameof(nickname))
        : nickname.Length > 32
        ? throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max")
        : GetResponsePropertyAsync<string>(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname");

    /// <summary>
    /// Changes the <see cref="Member.Nickname">nickname</see> of the <paramref name="memberReference">referenced member</paramref>.
    /// </summary>
    /// <param name="server">The server to modify member in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the <see cref="Member">member</see> to update</param>
    /// <param name="nickname">The new nickname of the member</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageNicknames">Required when deleting nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="Permission.ManageOwnNicknames">Required when deleting the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    /// <returns>Updated <see cref="Member.Nickname">nickname</see></returns>
    public Task<string> SetNicknameAsync(HashId server, UserReference memberReference, string nickname) =>
        string.IsNullOrWhiteSpace(nickname)
        ? throw new ArgumentNullException(nameof(nickname))
        : nickname.Length > 32
        ? throw new ArgumentOutOfRangeException(nameof(nickname), nickname, $"Argument {nameof(nickname)} must be 32 characters in length max")
        : GetResponsePropertyAsync<string>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/nickname", Method.Put)
            .AddJsonBody(new
            {
                nickname
            })
        , "nickname");

    /// <summary>
    /// Removes the <see cref="Member.Nickname">nickname</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to update</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageNicknames">Required when changing nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="Permission.ManageOwnNicknames">Required when changing the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    public Task RemoveNicknameAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/nickname", Method.Delete));

    /// <summary>
    /// Removes the <see cref="Member.Nickname">nickname</see> of the <paramref name="memberReference">referenced member</paramref>.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the <see cref="Member">member</see> to update</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageNicknames">Required when changing nicknames of <see cref="Member">other members</see></permission>
    /// <permission cref="Permission.ManageOwnNicknames">Required when changing the <see cref="AbstractGuildedClient">client's</see> own nickname</permission>
    public Task RemoveNicknameAsync(HashId server, UserReference memberReference) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/nickname", Method.Delete));

    /// <summary>
    /// Adds a <paramref name="role" /> to the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the receiving <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task AddMemberRoleAsync(HashId server, HashId member, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Put));

    /// <summary>
    /// Adds a <paramref name="role" /> to the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the receiving <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task AddMemberRoleAsync(HashId server, UserReference memberReference, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/roles/{role}", Method.Put));

    /// <summary>
    /// Removes the specified <paramref name="role" /> from the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the losing <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task RemoveMemberRoleAsync(HashId server, HashId member, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}/roles/{role}", Method.Delete));

    /// <summary>
    /// Removes the specified <paramref name="role" /> from the <see cref="User">user</see>.
    /// </summary>
    /// <remarks>
    /// <para>If they don't hold the specified <paramref name="role" />, then nothing happens.</para>
    /// </remarks>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the losing <see cref="Member">member</see></param>
    /// <param name="role">The identifier of the role to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageRoles" />
    public Task RemoveMemberRoleAsync(HashId server, UserReference memberReference, uint role) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/roles/{role}", Method.Delete));

    /// <summary>
    /// Gets a list of <see cref="Permission">permissions</see> that the <see cref="Member">member</see> has.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Member">member's</see> permissions in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to get permissions of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Permission">permissions</see> in the specified <paramref name="server" /></returns>
    public Task<IList<Permission>> GetMemberPermissionsAsync(HashId server, HashId member) =>
        GetResponsePropertyAsync<IList<Permission>>(new RestRequest($"servers/{server}/members/{member}/permissions", Method.Get), "permissions");

    /// <summary>
    /// Gets a list of <see cref="Permission">permissions</see> that the <see cref="Member">member</see> has.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Member">member's</see> permissions in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the <see cref="Member">member</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Permission">permissions</see> in the specified <paramref name="server" /></returns>
    public Task<IList<Permission>> GetMemberPermissionsAsync(HashId server, UserReference memberReference) =>
        GetResponsePropertyAsync<IList<Permission>>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/permissions", Method.Get), "permissions");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the specified <paramref name="member" />.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the receiving <see cref="Member">member</see></param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The total amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> AddXpAsync(HashId server, HashId member, short amount) =>
        amount is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1000 and less than -1000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the <paramref name="memberReference">member reference</paramref>.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the receiving <see cref="Member">member</see></param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The total amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> AddXpAsync(HashId server, UserReference memberReference, short amount) =>
        amount is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1000 and less than -1000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total");

    /// <summary>
    /// Sets how much <paramref name="total" /> XP the specified <paramref name="member" /> will have.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> who is being modified</param>
    /// <param name="total">The amount of XP the <see cref="Member">member</see> should have (values — <c>[-1000000000, 1000000000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The <paramref name="total" /> amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> SetXpAsync(HashId server, HashId member, long total) =>
        total is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot add more than 1000000000 and less than -1000000000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Put)
            .AddJsonBody(new
            {
                total
            })
        , "total");

    /// <summary>
    /// Sets how much <paramref name="total" /> XP the <paramref name="memberReference">member reference</paramref> will have.
    /// </summary>
    /// <param name="server">The server to modify <see cref="Member">member</see> in</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to the modified <see cref="Member">member</see></param>
    /// <param name="total">The amount of XP the <see cref="Member">member</see> should have (values — <c>[-1000000000, 1000000000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentOutOfRangeException">When the amount of XP given exceeds the limit</exception>
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The <paramref name="total" /> amount of XP that the <see cref="Member">member</see> has</returns>
    public Task<long> SetXpAsync(HashId server, UserReference memberReference, long total) =>
        total is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot add more than 1000000000 and less than -1000000000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/xp", Method.Put)
            .AddJsonBody(new
            {
                total
            })
        , "total");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the specified <paramref name="role">role's</paramref> members.
    /// </summary>
    /// <param name="server">The server where the role is</param>
    /// <param name="role">The identifier of the receiving role</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageXp" />
    public Task AddXpAsync(HashId server, uint role, short amount) =>
        amount is > 1000 or < -1000
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1000 and less than -1000 XP")
        : ExecuteRequestAsync(new RestRequest($"servers/{server}/roles/{role}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        );
    #endregion

    #region Methods Server-wide Moderation
    /// <summary>
    /// Removes the specified <paramref name="member" /> from the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to kick the <see cref="Member">member</see> from</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to kick</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    public Task RemoveMemberAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/{member}", Method.Delete));

    /// <summary>
    /// Removes the <paramref name="memberReference">referenced member</paramref> from the <paramref name="server" />.
    /// </summary>
    /// <param name="server">The server to kick the <see cref="Member">member</see> from</param>
    /// <param name="memberReference">A reference to the <see cref="Member">member</see> to kick</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    public Task RemoveMemberAsync(HashId server, UserReference memberReference) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}", Method.Delete));

    /// <summary>
    /// Gets the list of <paramref name="server">server's</paramref> bans.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get bans of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    /// <returns>The list of fetched <see cref="MemberBan">member bans</see> in the specified <paramref name="server" /></returns>
    public Task<IList<MemberBan>> GetMemberBansAsync(HashId server) =>
        TransformListResponseAsync(new RestRequest($"servers/{server}/bans", Method.Get), "serverMemberBans", value =>
        {
            value.Add("serverId", JValue.CreateString(server.ToString()));
            return value.ToObject<MemberBan>(GuildedSerializer)!;
        });

    /// <summary>
    /// Gets the information about the <see cref="MemberBan">ban</see> of the <paramref name="member" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="User">user</see> has been banned</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to get ban information of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    /// <returns>The <see cref="MemberBan">ban</see> of the <see cref="Member">member</see> that was specified in the arguments</returns>
    public Task<MemberBan> GetMemberBanAsync(HashId server, HashId member) =>
        TransformResponseAsync<MemberBan>(new RestRequest($"servers/{server}/bans/{member}", Method.Get), "serverMemberBan", token =>
        {
            token.Add("serverId", JValue.CreateString(server.ToString()));
            return token;
        });

    /// <summary>
    /// Bans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Disallows them from joining again, until they receive an unban with <see cref="RemoveMemberBanAsync" /> method.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to ban member from</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    /// <returns>Created <see cref="MemberBan">member's ban</see></returns>
    public Task<MemberBan> AddMemberBanAsync(HashId server, HashId member, string? reason = null) =>
        TransformResponseAsync<MemberBan>(
            new RestRequest($"servers/{server}/bans/{member}", Method.Post).AddJsonBody(new { reason }),
            "serverMemberBan",
            value =>
            {
                value.Add("serverId", JValue.CreateString(server.ToString()));
                return value;
            }
        );

    /// <summary>
    /// Bans the <paramref name="memberReference">referenced member</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>Disallows them from joining again, until they receive an unban with <see cref="RemoveMemberBanAsync" /> method.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to ban member from</param>
    /// <param name="memberReference">A reference to the <see cref="Member">member</see> to ban</param>
    /// <param name="reason">The reason for a ban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    /// <returns>Created <see cref="MemberBan">member's ban</see></returns>
    public Task<MemberBan> AddMemberBanAsync(HashId server, UserReference memberReference, string? reason = null) =>
        TransformResponseAsync<MemberBan>(
            new RestRequest($"servers/{server}/bans/@{memberReference.ToString().ToLower()}", Method.Post).AddJsonBody(new { reason }),
            "serverMemberBan",
            value =>
            {
                value.Add("serverId", JValue.CreateString(server.ToString()));
                return value;
            }
        );

    /// <summary>
    /// Unbans the specified <paramref name="member" />.
    /// </summary>
    /// <remarks>
    /// <para>Allows them to join the <see cref="Server">server</see> again.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to unban <see cref="Member">member</see> in</param>
    /// <param name="member">The identifier of the <see cref="Member">member</see> to unban</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.RemoveMembers" />
    public Task RemoveMemberBanAsync(HashId server, HashId member) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/bans/{member}", Method.Delete));
    #endregion

    #region Methods Webhooks
    /// <summary>
    /// Gets a list of <see cref="Webhook">webhooks</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="channel" /> parameter is given, it gets all of the channel <see cref="Webhook">webhooks</see> instead.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get <see cref="Webhook">webhooks</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="Webhook">webhooks</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Webhook>> GetWebhooksAsync(HashId server, Guid? channel = null) =>
        GetResponsePropertyAsync<IList<Webhook>>(new RestRequest($"servers/{server}/webhooks", Method.Get)
            .AddOptionalQuery("channelId", channel)
        , "webhooks");

    /// <summary>
    /// Gets the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Webhook">webhook</see> that was specified in the arguments</returns>
    public Task<Webhook> GetWebhookAsync(HashId server, Guid webhook) =>
        GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Get), "webhook");

    /// <summary>
    /// Creates a new <see cref="Webhook">webhook</see> in the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be created</param>
    /// <param name="name">The name of the <see cref="Webhook">webhook</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <permission cref="Permission.ManageWebhooks" />
    /// <returns>The <see cref="Webhook">webhook</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> CreateWebhookAsync(HashId server, Guid channel, string name) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks", Method.Post)
            .AddJsonBody(new
            {
                name,
                channelId = channel
            })
        , "webhook");

    /// <summary>
    /// Edits the specified <paramref name="webhook" />.
    /// </summary>
    /// <remarks>
    /// <para><see cref="Webhook" /> can moved between <see cref="ServerChannel">channels</see> using <paramref name="newChannel" /> parameter.</para>
    /// </remarks>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to update</param>
    /// <param name="name">The new name of the <see cref="Webhook">webhook</see></param>
    /// <param name="newChannel">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Webhook">webhook</see> will be moved to</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageWebhooks" />
    /// <exception cref="ArgumentNullException">The specified <paramref name="name" /> is null, empty or whitespace</exception>
    /// <returns>The <see cref="Webhook">webhook</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Webhook> UpdateWebhookAsync(HashId server, Guid webhook, string name, Guid? newChannel = null) =>
        string.IsNullOrWhiteSpace(name)
        ? throw new ArgumentNullException(nameof(name))
        : GetResponsePropertyAsync<Webhook>(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Put)
            .AddJsonBody(new
            {
                name,
                channelId = newChannel
            })
        , "webhook");

    /// <summary>
    /// Deletes the specified <paramref name="webhook" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="Webhook">webhook</see> is</param>
    /// <param name="webhook">The identifier of the <see cref="Webhook">webhook</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageWebhooks" />
    public Task DeleteWebhookAsync(HashId server, Guid webhook) =>
        ExecuteRequestAsync(new RestRequest($"servers/{server}/webhooks/{webhook}", Method.Delete));
    #endregion

    #region Methods Channels
    /// <summary>
    /// Gets the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was specified in the arguments</returns>
    public Task<ServerChannel> GetChannelAsync(Guid channel) =>
        GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Get), "channel");

    /// <summary>
    /// Creates a new channel in the specified <paramref name="server" />.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="type">The type of the content that the <see cref="ServerChannel">channel</see> will hold</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <param name="group">The identifier of the group where the <see cref="ServerChannel">channel</see> will be created</param>
    /// <param name="category">The identifier of the category where the <see cref="ServerChannel">channel</see> will be created</param>
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
    public Task<ServerChannel> CreateChannelAsync(HashId server, string name, ChannelType type = ChannelType.Chat, string? topic = null, bool? isPublic = null, HashId? group = null, uint? category = null, Guid? parent = null, Guid? message = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        EnforceLimit(nameof(name), name, ServerChannel.NameLimit);
        EnforceLimitOnNullable(nameof(topic), topic, ServerChannel.TopicLimit);

        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels", Method.Post)
            .AddJsonBody(new
            {
                serverId = server,
                groupId = group,
                categoryId = category,
                parentId = parent,
                messageId = message,
                name,
                type,
                topic,
                isPublic
            })
        , "channel");
    }

    /// <summary>
    /// Updates the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the <see cref="ServerChannel">channel</see> to update</param>
    /// <param name="name">A new name of the <see cref="ServerChannel">channel</see> (max — <c>100</c>)</param>
    /// <param name="topic">A new topic describing what the <see cref="ServerChannel">channel</see> is about (max — <c>512</c>)</param>
    /// <param name="isPublic">Whether the contents of the channel are publicly viewable</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    /// <returns>The <see cref="ServerChannel">channel</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<ServerChannel> UpdateChannelAsync(Guid channel, string? name = null, string? topic = null, bool? isPublic = null)
    {
        return GetResponsePropertyAsync<ServerChannel>(new RestRequest($"channels/{channel}", Method.Patch)
            .AddJsonBody(new
            {
                name,
                topic,
                isPublic
            })
        , "channel");
    }

    /// <summary>
    /// Deletes the specified <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The identifier of the channel to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageChannels" />
    public Task DeleteChannelAsync(Guid channel) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}", Method.Delete));
    #endregion
}