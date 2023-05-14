using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods Short Info
    /// <summary>
    /// Adds or edits the status of the <see cref="AbstractGuildedClient">client's</see> bot account.
    /// </summary>
    /// <param name="content">The text contents of the status</param>
    /// <param name="emoteId">The identifier of the status's <see cref="Emote">emote</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    public Task SetStatusAsync(string content, uint emoteId) =>
        ExecuteRequestAsync(new RestRequest("/users/@me/status", Method.Put).AddBody(new { content, emoteId }));

    /// <summary>
    /// Removes the status of the <see cref="AbstractGuildedClient">client's</see> bot account.
    /// </summary>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    public Task RemoveStatusAsync() =>
        ExecuteRequestAsync(new RestRequest("/users/@me/status", Method.Delete));
    #endregion

    #region Methods Profile info
    /// <summary>
    /// Gets the specified <paramref name="member">member's</paramref> social link based on given <paramref name="linkType">social link type</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>This does not require any permissions to be given.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch user's information</param>
    /// <param name="member">The identifier of <see cref="User">user</see></param>
    /// <param name="linkType">The social link to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="SocialLink">social link</see> of the specified <see cref="Servers.Member">member</see></returns>
    public Task<SocialLink> GetSocialLinkAsync(HashId server, HashId member, SocialLinkType linkType) =>
        GetResponsePropertyAsync<SocialLink>(new RestRequest($"servers/{server}/members/{member}/social-links/{linkType.ToString().ToLower()}", Method.Get), "socialLink");

    /// <summary>
    /// Gets the specified member's (from their <paramref name="memberReference">reference</paramref>) social link based on given <paramref name="linkType">social link type</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>This does not require any permissions to be given.</para>
    /// </remarks>
    /// <param name="server">The server where to fetch user's information</param>
    /// <param name="memberReference">A <see cref="UserReference">reference</see> to some kind of <see cref="User">user</see></param>
    /// <param name="linkType">The social link to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="SocialLink">social link</see> of the specified <see cref="Servers.Member">member</see></returns>
    public Task<SocialLink> GetSocialLinkAsync(HashId server, UserReference memberReference, SocialLinkType linkType) =>
        GetResponsePropertyAsync<SocialLink>(new RestRequest($"servers/{server}/members/@{memberReference.ToString().ToLower()}/social-links/{linkType.ToString().ToLower()}", Method.Get), "socialLink");

    /// <summary>
    /// Gets the information about the specified <paramref name="user" />.
    /// </summary>
    /// <remarks>
    /// <para>This does not require any permissions to be given.</para>
    /// </remarks>
    /// <param name="user">The identifier of the <see cref="User">user</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The specified <see cref="User">user</see></returns>
    public Task<User> GetUserAsync(HashId user) =>
        GetResponsePropertyAsync<User>(new RestRequest($"users/{user}", Method.Get), "user");

    /// <summary>
    /// Gets the information about the specified <paramref name="userReference">user reference</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>This does not require any permissions to be given.</para>
    /// </remarks>
    /// <param name="userReference">A <see cref="UserReference">reference</see> to a type of the <see cref="User">user</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The specified <see cref="User">user</see> by <paramref name="userReference">reference</paramref></returns>
    public Task<User> GetUserAsync(UserReference userReference) =>
        GetResponsePropertyAsync<User>(new RestRequest($"users/@{userReference.ToString().ToLower()}", Method.Get), "user");
    #endregion
}