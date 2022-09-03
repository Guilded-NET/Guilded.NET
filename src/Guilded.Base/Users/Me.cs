using System;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the currently logged in user.
/// </summary>
/// <seealso cref="User" />
/// <seealso cref="Servers.Member" />
public class Me : IModelHasId<HashId>, ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> <see cref="BaseGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="Me" />
    /// <seealso cref="BotId" />
    /// <seealso cref="Name" />
    public HashId Id { get; set; }

    /// <summary>
    /// Gets the identifier of the bot <see cref="BaseGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value>Bot ID</value>
    /// <seealso cref="Me" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    public Guid BotId { get; set; }

    /// <summary>
    /// Gets the name of <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <value>Name</value>
    /// <seealso cref="Me" />
    /// <seealso cref="Id" />
    /// <seealso cref="BotId" />
    public string Name { get; set; }

    /// <summary>
    /// Gets the creation date of <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Me" />
    /// <seealso cref="CreatedBy" />
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that has created <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be the identifier of your account or <see cref="Users.User">the user</see> who owns the bot.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="Me" />
    /// <seealso cref="CreatedAt" />
    public HashId CreatedBy { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Me" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="User">user</see> <see cref="BaseGuildedClient">this client</see> is logged into</param>
    /// <param name="botId">The identifier of the bot <see cref="BaseGuildedClient">this client</see> is logged into</param>
    /// <param name="name">The name of <see cref="BaseGuildedClient">this client</see></param>
    /// <param name="createdAt">The creation date of <see cref="BaseGuildedClient">this client</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that has created <see cref="BaseGuildedClient">this client</see></param>
    /// <returns>New <see cref="Me" /> JSON instance</returns>
    /// <seealso cref="Me" />
    [JsonConstructor]
    public Me(
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