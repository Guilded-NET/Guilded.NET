using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Newtonsoft.Json;

namespace Guilded.Users;

/// <summary>
/// Represents the currently logged in user.
/// </summary>
/// <seealso cref="AbstractGuildedClient.Me" />
/// <seealso cref="User" />
/// <seealso cref="Servers.Member" />
public class ClientUser : User, ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the bot the <see cref="AbstractGuildedClient">client</see> is logged into.
    /// </summary>
    /// <value>The identifier of the bot the <see cref="AbstractGuildedClient">client</see> is logged into</value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="UserSummary.Id" />
    /// <seealso cref="UserSummary.Name" />
    public Guid BotId { get; set; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that has created the <see cref="AbstractGuildedClient">client</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be the identifier of your account or the <see cref="User">user</see> who owns the bot.</para>
    /// </remarks>
    /// <value>The identifier of <see cref="User">user</see> that has created the <see cref="AbstractGuildedClient">client</see></value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="User.CreatedAt" />
    public HashId CreatedBy { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ClientUser" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see> the <see cref="AbstractGuildedClient">client</see> is logged into</param>
    /// <param name="botId">The identifier of the bot the <see cref="AbstractGuildedClient">client</see> is logged into</param>
    /// <param name="name">The name of the <see cref="AbstractGuildedClient">client</see></param>
    /// <param name="createdAt">The creation date of the <see cref="AbstractGuildedClient">client</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that has created the <see cref="AbstractGuildedClient">client</see></param>
    /// <param name="avatar">The global avatar of the <see cref="User">user</see></param>
    /// <param name="banner">The global banner of the <see cref="User">user</see></param>
    /// <param name="type">The type of the <see cref="User">user</see> they are</param>
    /// <returns>New <see cref="ClientUser" /> JSON instance</returns>
    /// <seealso cref="ClientUser" />
    [JsonConstructor]
    public ClientUser(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        Guid botId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? banner = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        UserType type = UserType.Bot
    ) : base(id, name, createdAt, avatar, banner, type) =>
        (BotId, CreatedBy) = (botId, createdBy);
    #endregion
}