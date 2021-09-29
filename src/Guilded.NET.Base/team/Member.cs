using System.Threading.Tasks;
using Guilded.NET.Base.Users;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Permissions;
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
        /// <para>Defines a nickname of this member. This may be <see langword="null"/> if the member has no nickname.</para>
        /// </remarks>
        /// <value>Name?</value>
        public string Nickname
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <inheritdoc cref="BaseGuildedClient.GetSocialLinkAsync(GId, SocialLinkType)"/>
        public async Task<SocialLink> GetSocialLinkAsync(SocialLinkType linkType) =>
            await ParentClient.GetSocialLinkAsync(Id, linkType).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.UpdateNicknameAsync(GId, string)"/>
        public async Task<string> UpdateNicknameAsync(string nickname) =>
            await ParentClient.UpdateNicknameAsync(Id, nickname).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.DeleteMessageAsync(System.Guid, System.Guid)"/>
        public async Task DeleteNicknameAsync() =>
            await ParentClient.DeleteNicknameAsync(Id).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.AddRoleAsync(GId, uint)"/>
        public async Task AddRoleAsync(uint roleId) =>
            await ParentClient.AddRoleAsync(Id, roleId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.RemoveRoleAsync(GId, uint)"/>
        public async Task RemoveRoleAsync(uint roleId) =>
            await ParentClient.RemoveRoleAsync(Id, roleId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.AddXpAsync(GId, short)"/>
        public async Task<long> AddXpAsync(short amount) =>
            await ParentClient.AddXpAsync(Id, amount).ConfigureAwait(false);
        #endregion
    }
}