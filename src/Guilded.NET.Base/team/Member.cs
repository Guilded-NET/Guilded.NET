using System.Threading.Tasks;
using Guilded.NET.Base.Users;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Servers
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
        public HashId Id
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
        public string? Nickname
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <inheritdoc cref="BaseGuildedClient.GetSocialLinkAsync(HashId, HashId, SocialLinkType)"/>
        public async Task<SocialLink> GetSocialLinkAsync(HashId serverId, SocialLinkType linkType) =>
            await ParentClient.GetSocialLinkAsync(serverId, Id, linkType).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.UpdateNicknameAsync(HashId, HashId, string)"/>
        public async Task<string> UpdateNicknameAsync(HashId serverId, string nickname) =>
            await ParentClient.UpdateNicknameAsync(serverId, Id, nickname).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.DeleteMessageAsync(System.Guid, System.Guid)"/>
        public async Task DeleteNicknameAsync(HashId serverId) =>
            await ParentClient.DeleteNicknameAsync(serverId, Id).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.AddRoleAsync(HashId, HashId, uint)"/>
        public async Task AddRoleAsync(HashId serverId, uint roleId) =>
            await ParentClient.AddRoleAsync(serverId, Id, roleId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.RemoveRoleAsync(HashId, HashId, uint)"/>
        public async Task RemoveRoleAsync(HashId serverId, uint roleId) =>
            await ParentClient.RemoveRoleAsync(serverId, Id, roleId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.AddXpAsync(HashId, HashId, long)"/>
        public async Task<long> AddXpAsync(HashId serverId, short amount) =>
            await ParentClient.AddXpAsync(serverId, Id, amount).ConfigureAwait(false);
        #endregion
    }
}