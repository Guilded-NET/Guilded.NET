using System;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Users;
    public abstract partial class AbstractGuildedClient
    {
        #region Profile info
        /// <summary>
        /// Gets user's social links.
        /// </summary>
        /// <remarks>
        /// <para>Gets <paramref name="userId"/>'s social link based on given <paramref name="linkType"/>.</para>
        /// <para>This does not require any permissions to be given, as it is not team-based.</para>
        /// </remarks>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="linkType">The social link to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>User's social link</returns>
        public override async Task<SocialLink> GetSocialLinkAsync(GId userId, SocialLinkType linkType) =>
            await GetObject<SocialLink>($"users/{userId}/social-links/{linkType.ToString().ToLower()}", Method.GET, key: "socialLink").ConfigureAwait(false);
        #endregion
    }
}