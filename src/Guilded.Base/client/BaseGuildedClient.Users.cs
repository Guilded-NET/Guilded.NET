using System.Threading.Tasks;

namespace Guilded.Base
{
    using Users;
    public abstract partial class BaseGuildedClient
    {
        #region Profile info
        /// <summary>
        /// Gets the specified user's social links.
        /// </summary>
        /// <remarks>
        /// <para>Gets the specified user's social link based on given <paramref name="linkType"/>.</para>
        /// <para>This does not require any permissions to be given, as it is not team-based.</para>
        /// </remarks>
        /// <param name="serverId">The server where to fetch user's information</param>
        /// <param name="memberId">The identifier of the user</param>
        /// <param name="linkType">The social link to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>User's social link</returns>
        public abstract Task<SocialLink> GetSocialLinkAsync(HashId serverId, HashId memberId, SocialLinkType linkType);
        #endregion
    }
}