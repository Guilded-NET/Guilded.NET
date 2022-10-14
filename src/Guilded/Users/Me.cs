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
public class ClientUser : ICreatableContent, IUser
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> <see cref="AbstractGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="BotId" />
    /// <seealso cref="Name" />
    public HashId Id { get; set; }

    /// <summary>
    /// Gets the identifier of the bot <see cref="AbstractGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value>Bot ID</value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    public Guid BotId { get; set; }

    /// <summary>
    /// Gets the name of <see cref="AbstractGuildedClient">this client</see>.
    /// </summary>
    /// <value>Name</value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="Id" />
    /// <seealso cref="BotId" />
    public string Name { get; set; }

    /// <summary>
    /// Gets the creation date of <see cref="AbstractGuildedClient">this client</see>.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="CreatedBy" />
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that has created <see cref="AbstractGuildedClient">this client</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be the identifier of your account or the <see cref="User">user</see> who owns the bot.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ClientUser" />
    /// <seealso cref="CreatedAt" />
    public HashId CreatedBy { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ClientUser" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see> <see cref="AbstractGuildedClient">this client</see> is logged into</param>
    /// <param name="botId">The identifier of the bot <see cref="AbstractGuildedClient">this client</see> is logged into</param>
    /// <param name="name">The name of <see cref="AbstractGuildedClient">this client</see></param>
    /// <param name="createdAt">The creation date of <see cref="AbstractGuildedClient">this client</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that has created <see cref="AbstractGuildedClient">this client</see></param>
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
        HashId createdBy
    ) =>
        (Id, BotId, Name, CreatedAt, CreatedBy) = (id, botId, name, createdAt, createdBy);
    #endregion
}