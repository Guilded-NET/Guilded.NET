using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Global minimal information about a user.
/// </summary>
/// <remarks>
/// <para>Defines a normal user with minimal information.</para>
/// </remarks>
/// <seealso cref="User" />
/// <seealso cref="SocialLink" />
public class UserSummary : ClientObject
{
    #region JSON properties
    /// <summary>
    /// The identifier of this user.
    /// </summary>
    /// <value>User ID</value>
    public HashId Id { get; }
    /// <summary>
    /// The type of the user.
    /// </summary>
    /// <remarks>
    /// <para>Defines the type of the user they are.</para>
    /// </remarks>
    /// <value>User type</value>
    public UserType Type { get; }
    /// <summary>
    /// The name of the user.
    /// </summary>
    /// <remarks>
    /// <para>The global username that user uses.</para>
    /// </remarks>
    /// <value>Name</value>
    public string Name { get; set; }
    #endregion

    #region Properties
    /// <summary>
    /// Whether the user is a bot.
    /// </summary>
    /// <remarks>
    /// <para>Gets whether the user is a global bot.</para>
    /// </remarks>
    /// <value>Is a bot</value>
    public bool IsBot => Type == UserType.Bot;

    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="UserSummary"/> with specified properties.
    /// </summary>
    /// <param name="id">The identifier of the user</param>
    /// <param name="type">The type of user they are</param>
    /// <param name="name">The name of the user</param>
    [JsonConstructor]
    public UserSummary(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        UserType type,

        [JsonProperty(Required = Required.Always)]
        string name
    ) =>
        (Id, Type, Name) = (id, type, name);
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
    /// <inheritdoc cref="BaseGuildedClient.KickMemberAsync(HashId, HashId)"/>
    public async Task KickMemberAsync(HashId serverId) =>
        await ParentClient.KickMemberAsync(serverId, Id).ConfigureAwait(false);
    #endregion
}