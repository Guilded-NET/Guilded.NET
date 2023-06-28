using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Users;

/// <summary>
/// Global minimal information about a user.
/// </summary>
/// <seealso cref="User" />
/// <seealso cref="SocialLink" />
public class UserSummary : ContentModel, IUser
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see>.
    /// </summary>
    /// <value>The identifier of <see cref="User">user</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="Type" />
    /// <seealso cref="Name" />
    public HashId Id { get; }

    /// <summary>
    /// Gets the type of the <see cref="User">user</see> they are.
    /// </summary>
    /// <value>The type of the <see cref="User">user</see> they are</value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    public UserType Type { get; }

    /// <summary>
    /// Gets the global username of the <see cref="User">user</see>.
    /// </summary>
    /// <value>The global username of the <see cref="User">user</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="User.Banner" />
    public string Name { get; }

    /// <summary>
    /// Gets the global avatar of the <see cref="User">user</see>.
    /// </summary>
    /// <value>The global avatar of the <see cref="User">user</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="User.Banner" />
    /// <seealso cref="Name" />
    public Uri? Avatar { get; }

    /// <summary>
    /// Gets whether the <see cref="User">user</see> is a <see cref="UserType.Bot">bot</see>.
    /// </summary>
    /// <value>Whether the <see cref="User">user</see> is a <see cref="UserType.Bot">bot</see></value>
    /// <seealso cref="User" />
    /// <seealso cref="UserSummary" />
    /// <seealso cref="Type" />
    /// <seealso cref="Id" />
    public bool IsBot => Type == UserType.Bot;
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member</see> leaves the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Member">member</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member</see> leaves the <see cref="Server">server</see></returns>
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberBanAdded" />
    /// <seealso cref="MemberBanRemoved" />
    public IObservable<MemberRemovedEvent> MemberRemoved =>
        ParentClient
            .MemberRemoved
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member</see> joins the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Member">member</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member</see> joins the <see cref="Server">server</see></returns>
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberBanAdded" />
    /// <seealso cref="MemberBanRemoved" />
    public IObservable<MemberJoinedEvent> MemberJoined =>
        ParentClient
            .MemberJoined
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> nickname changes in the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Member">member</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> nickname changes in the <see cref="Server">server</see></returns>
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberBanAdded" />
    /// <seealso cref="MemberBanRemoved" />
    public IObservable<MemberUpdatedEvent> MemberUpdated =>
        ParentClient
            .MemberUpdated
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> gets banned in the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Member">member</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> gets banned in the <see cref="Server">server</see></returns>
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberBanRemoved" />
    public IObservable<MemberBanEvent> MemberBanAdded =>
        ParentClient
            .MemberBanned
            .HasId(Id);

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> gets unbanned in the <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Member">member</see> and <see cref="Server">server</see> specifically.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when <see cref="Member">member's</see> gets unbanned in the <see cref="Server">server</see></returns>
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="MemberBanRemoved" />
    public IObservable<MemberBanEvent> MemberBanRemoved =>
        ParentClient
            .MemberUnbanned
            .HasId(Id);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserSummary" /> with specified properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see></param>
    /// <param name="type">The type of the <see cref="User">user</see> they are</param>
    /// <param name="name">The global username of the <see cref="User">user</see></param>
    /// <param name="avatar">The global avatar of the <see cref="User">user</see></param>
    /// <returns>New <see cref="UserSummary" /> JSON instance</returns>
    /// <seealso cref="UserSummary" />
    [JsonConstructor]
    public UserSummary(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        UserType type = UserType.User
    ) =>
        (Id, Type, Name, Avatar) = (id, type, name, avatar);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetSocialLinkAsync(HashId, HashId, SocialLinkType)" />
    public Task<SocialLink> GetSocialLinkAsync(HashId server, SocialLinkType linkType) =>
        ParentClient.GetSocialLinkAsync(server, Id, linkType);

    /// <inheritdoc cref="AbstractGuildedClient.SetNicknameAsync(HashId, HashId, string)" />
    public Task<string> SetNicknameAsync(HashId server, string nickname) =>
        ParentClient.SetNicknameAsync(server, Id, nickname);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteMessageAsync(Guid, Guid)" />
    public Task RemoveNicknameAsync(HashId server) =>
        ParentClient.RemoveNicknameAsync(server, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddMemberRoleAsync(HashId, HashId, uint)" />
    public Task AddMemberRoleAsync(HashId server, uint role) =>
        ParentClient.AddMemberRoleAsync(server, Id, role);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberRoleAsync(HashId, HashId, uint)" />
    public Task RemoveMemberRoleAsync(HashId server, uint role) =>
        ParentClient.RemoveMemberRoleAsync(server, Id, role);

    /// <inheritdoc cref="AbstractGuildedClient.GetMemberPermissionsAsync(HashId, HashId)" />
    public Task<IList<Permission>> GetMemberPermissionsAsync(HashId server) =>
        ParentClient.GetMemberPermissionsAsync(server, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddXpAsync(HashId, HashId, short)" />
    public Task<long> AddXpAsync(HashId server, short amount) =>
        ParentClient.AddXpAsync(server, Id, amount);

    /// <inheritdoc cref="AbstractGuildedClient.SetXpAsync(HashId, HashId, long)" />
    public Task<long> SetXpAsync(HashId server, long amount) =>
        ParentClient.SetXpAsync(server, Id, amount);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberAsync(HashId, HashId)" />
    public Task RemoveMemberAsync(HashId server) =>
        ParentClient.RemoveMemberAsync(server, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddMemberBanAsync(HashId, HashId, string?)" />
    public Task AddMemberBanAsync(HashId server, string? reason = null) =>
        ParentClient.AddMemberBanAsync(server, Id, reason);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberBanAsync(HashId, HashId)" />
    public Task RemoveMemberBanAsync(HashId server) =>
        ParentClient.RemoveMemberBanAsync(server, Id);

    /// <inheritdoc cref="AbstractGuildedClient.GetMemberBanAsync(HashId, HashId)" />
    public Task GetMemberBanAsync(HashId server) =>
        ParentClient.GetMemberBanAsync(server, Id);
    #endregion

    #region Methods Obsolete
    /// <inheritdoc cref="AbstractGuildedClient.DeleteMessageAsync(Guid, Guid)" />
    [Obsolete("Use `RemoveNicknameAsync` instead")]
    public Task DeleteNicknameAsync(HashId server) =>
        RemoveNicknameAsync(server);

    /// <inheritdoc cref="AbstractGuildedClient.AddMemberRoleAsync(HashId, HashId, uint)" />
    [Obsolete("Use `AddMemberRoleAsync` instead")]
    public Task AddRoleAsync(HashId server, uint role) =>
        AddMemberRoleAsync(server, role);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveMemberRoleAsync(HashId, HashId, uint)" />
    [Obsolete("Use `RemoveMemberRoleAsync` instead")]
    public Task RemoveRoleAsync(HashId server, uint role) =>
        RemoveMemberRoleAsync(server, role);

    /// <inheritdoc cref="AbstractGuildedClient.GetMemberBanAsync(HashId, HashId)" />
    [Obsolete("Use `GetMemberBanAsync` instead")]
    public Task GetBanAsync(HashId server) =>
        ParentClient.GetMemberBanAsync(server, Id);
    #endregion

    #region Methods Overrides
    /// <summary>
    /// Returns the string representation of this <see cref="UserSummary" /> instance.
    /// </summary>
    /// <remarks>
    /// <para>The mention syntax of the user will be returned.</para>
    /// </remarks>
    /// <example>
    /// <para>An example of a code with clearly defined identifier:</para>
    /// <code lang="csharp">
    /// UserSummary user = new(new HashId("R40Mp0Wd"), UserType.User, "Example");
    /// Console.WriteLine("Here's the user: {0}", user);
    /// </code>
    /// <para>The output of the code above:</para>
    /// <code lang="bash">
    /// Here's the user: &lt;@R40Mp0Wd&gt;
    /// </code>
    /// </example>
    /// <returns>Markdown user mention</returns>
    public override string ToString() =>
        $"<@{Id}>";
    #endregion
}