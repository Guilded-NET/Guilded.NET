using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Users;
    public abstract partial class BaseGuildedClient
    {
        #region Profile info
        /// <summary>
        /// Gets user's social links.
        /// </summary>
        /// <remarks>
        /// <para>Gets given user's social link based on given <paramref name="linkType"/>.</para>
        /// <para>This does not require any permissions to be given, as it is not team-based.</para>
        /// </remarks>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="linkType">The social link to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>User's social link</returns>
        public abstract Task<SocialLink> GetSocialLinkAsync(GId userId, SocialLinkType linkType);
        #endregion
    }
}