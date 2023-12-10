using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Members
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">members</see> receive or lose roles.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerRolesUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<RolesUpdatedEvent> MemberRolesUpdated => ((IEventInfo<RolesUpdatedEvent>)GuildedEvents["ServerRolesUpdated"]).Observable;

    /// <inheritdoc cref="MemberRolesUpdated" />
    [Obsolete($"Renamed to `{nameof(MemberRolesUpdated)}` due to possible confusion with RoleUpdated")]
    public IObservable<RolesUpdatedEvent> RolesUpdated => MemberRolesUpdated;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when server-wide profile of a <see cref="Member">member</see> gets changed.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberUpdatedEvent> MemberUpdated => ((IEventInfo<MemberUpdatedEvent>)GuildedEvents["ServerMemberUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> joins a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberJoined</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberJoinedEvent> MemberJoined => ((IEventInfo<MemberJoinedEvent>)GuildedEvents["ServerMemberJoined"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> leaves a <see cref="Server">server</see>, gets kicked or gets banned from a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberRemoved</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberRemovedEvent> MemberRemoved => ((IEventInfo<MemberRemovedEvent>)GuildedEvents["ServerMemberRemoved"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Member">member</see> gets banned from the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberBanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberBanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberBanned"]).Observable;

    /// <inheritdoc cref="MemberBanRemoved" />
    [Obsolete("Use `MemberBanned` instead")]
    public IObservable<MemberBanEvent> MemberBanAdded => MemberBanned;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="User">user</see> gets unbanned in a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberUnbanned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    /// <seealso cref="ServerAdded" />
    public IObservable<MemberBanEvent> MemberUnbanned => ((IEventInfo<MemberBanEvent>)GuildedEvents["ServerMemberUnbanned"]).Observable;

    /// <inheritdoc cref="MemberUnbanned" />
    [Obsolete("Use `MemberUnbanned` instead")]
    public IObservable<MemberBanEvent> MemberBanRemoved => MemberUnbanned;
    #endregion

    #region Properties Member Social Links
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> is added to <see cref="Member">member's</see> profile.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkUpdated" />
    /// <seealso cref="MemberSocialLinkDeleted" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkCreated => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> in <see cref="Member">member's</see> profile gets updated.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkCreated" />
    /// <seealso cref="MemberSocialLinkDeleted" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkUpdated => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="SocialLink">social link</see> in <see cref="Member">member's</see> profile gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ServerMemberSocialLinkDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberSocialLinkCreated" />
    /// <seealso cref="MemberSocialLinkUpdated" />
    public IObservable<MemberSocialLinkEvent> MemberSocialLinkDeleted => ((IEventInfo<MemberSocialLinkEvent>)GuildedEvents["ServerMemberSocialLinkDeleted"]).Observable;
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
        amount is > Member.MaxAddXp or < Member.MinAddXp
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1 000 and less than -1 000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/{member}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        , "total");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the <paramref name="memberReference">referenced member</paramref>.
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
        amount is > Member.MaxAddXp or < Member.MinAddXp
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1 000 and less than -1 000 XP")
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
        total is > Member.MaxSetXp or < Member.MinSetXp
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot add more than 1 000 000 000 and less than -1 000 000 000 XP")
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
        total is > Member.MaxSetXp or < Member.MinSetXp
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot add more than 1 000 000 000 and less than -1 000 000 000 XP")
        : GetResponsePropertyAsync<long>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/xp", Method.Put)
            .AddJsonBody(new
            {
                total
            })
        , "total");

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to the specified <paramref name="role">role's</paramref> <see cref="Member">members</see>.
    /// </summary>
    /// <param name="server">The <see cref="Server">server</see> where the <see cref="Role">role</see> is</param>
    /// <param name="role">The identifier of the receiving <see cref="Role">role</see></param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageXp" />
    public Task AddXpAsync(HashId server, uint role, short amount) =>
        amount is > Member.MaxAddXp or < Member.MinAddXp
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1 000 and less than -1 000 XP")
        : ExecuteRequestAsync(new RestRequest($"servers/{server}/roles/{role}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount
            })
        );

    /// <summary>
    /// Gives the specified <paramref name="amount" /> of XP to all of the specified <paramref name="members" />.
    /// </summary>
    /// <param name="server">The server where the <see cref="Member">members</see> are</param>
    /// <param name="amount">The amount of XP received (values — <c>[-1000, 1000]</c>)</param>
    /// <param name="members">The list of <see cref="Member">members</see> that will be receiving the XP</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The total amount of XP that the each specified <see cref="Member">member</see> has</returns>
    public Task<IDictionary<HashId, long>> AddBulkXpAsync(HashId server, short amount, params HashId[] members) =>
        amount is > Member.MaxAddXp or < Member.MinAddXp
        ? throw new ArgumentOutOfRangeException(nameof(amount), amount, "Cannot add more than 1 000 and less than -1 000 XP")
        : GetResponsePropertyAsync<IDictionary<HashId, long>>(new RestRequest($"servers/{server}/xp", Method.Post)
            .AddJsonBody(new
            {
                amount,
                users = members,
            }),
            "totalsByUserId"
        );

    /// <summary>
    /// Sets the specified <paramref name="total">total amount</paramref> of XP for all of the specified <paramref name="members" />.
    /// </summary>
    /// <param name="server">The server where the <see cref="Member">members</see> are</param>
    /// <param name="total">The total amount of XP to set for the user</param>
    /// <param name="members">The list of <see cref="Member">members</see> that will have their XP changed</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.ManageXp" />
    /// <returns>The total amount of XP that the each specified <see cref="Member">member</see> has</returns>
    public Task<IDictionary<HashId, long>> SetBulkXpAsync(HashId server, long total, params HashId[] members) =>
        total is > Member.MaxSetXp or < Member.MinSetXp
        ? throw new ArgumentOutOfRangeException(nameof(total), total, "Cannot set more than 1 000 000 000 and less than -1 000 000 000 XP")
        : GetResponsePropertyAsync<IDictionary<HashId, long>>(new RestRequest($"servers/{server}/xp", Method.Put)
            .AddJsonBody(new
            {
                amount = total,
                users = members,
            }),
            "totalsByUserId"
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
}