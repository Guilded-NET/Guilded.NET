using System;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the currently logged in user.
/// </summary>
public class Me : BaseObject
{
    /// <summary>
    /// Gets the identifier of the user <see cref="BaseGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value>User ID</value>
    public HashId Id { get; set; }
    /// <summary>
    /// Gets the identifier of the bot <see cref="BaseGuildedClient">this client</see> is logged into.
    /// </summary>
    /// <value>Bot ID</value>
    public Guid BotId { get; set; }
    /// <summary>
    /// Gets the name of <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <value>Name</value>
    public string Name { get; set; }
    /// <summary>
    /// Gets the creation date of <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets the identifier of the user that has created <see cref="BaseGuildedClient">this client</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be the identifier of your account or the user that owns the bot.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId CreatedBy { get; set; }
    /// <summary>
    /// Initializes a new instance of <see cref="Me" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the user <see cref="BaseGuildedClient">this client</see> is logged into</param>
    /// <param name="botId">The identifier of the bot <see cref="BaseGuildedClient">this client</see> is logged into</param>
    /// <param name="name">The name of <see cref="BaseGuildedClient">this client</see></param>
    /// <param name="createdAt">The creation date of <see cref="BaseGuildedClient">this client</see></param>
    /// <param name="createdBy">The identifier of the user that has created <see cref="BaseGuildedClient">this client</see></param>
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
}