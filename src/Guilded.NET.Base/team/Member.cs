using System.Threading.Tasks;
using Guilded.NET.Base.Users;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A member in a member list.
    /// </summary>
    /// <remarks>
    /// <para>Defines a normal or updated team member.</para>
    /// </remarks>
    /// <seealso cref="Events.MemberUpdatedEvent"/>
    public class Member : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of this member.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId Id
        {
            get; set;
        }
        /// <summary>
        /// A nickname of this member.
        /// </summary>
        /// <remarks>
        /// <para>Defines a nickname of this member. This may be <see langword="null"/> if the member
        /// has no nickname.</para>
        /// </remarks>
        /// <value>Name?</value>
        public string Nickname
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <summary>
        /// Gets member's social links.
        /// </summary>
        /// <remarks>
        /// <para>Gets member's social link based on given <paramref name="linkType"/>.</para>
        /// <para>This does not require any permissions to be given, as it is not team-based.</para>
        /// </remarks>
        /// <param name="linkType">The social link to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>Member's social link</returns>
        public async Task<SocialLink> GetSocialLinkAsync(SocialLinkType linkType) =>
            await ParentClient.GetSocialLinkAsync(Id, linkType).ConfigureAwait(false);
        #endregion
    }
}