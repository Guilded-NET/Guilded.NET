using System;
using Newtonsoft.Json;

namespace Guilded.Base.Users;

/// <summary>
/// The information about the current logged in user.
/// </summary>
public class Me : BaseObject
{
    /// <summary>
    /// The identifier of this user.
    /// </summary>
    /// <remarks>
    /// <para>The user identifier of the current client.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId Id { get; set; }
    /// <summary>
    /// The identifier of this bot.
    /// </summary>
    /// <remarks>
    /// <para>The bot identifier of the current client to distinguish it from flowbots.</para>
    /// </remarks>
    /// <value>Bot ID</value>
    public Guid BotId { get; set; }
    /// <summary>
    /// The name of this client.
    /// </summary>
    /// <value>Name</value>
    public string Name { get; set; }
    /// <summary>
    /// The creation date of this client.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// The identifier of the creator of this bot.
    /// </summary>
    /// <value>User ID</value>
    public HashId CreatedBy { get; set; }
    /// <summary>
    /// Creates new instance of <see cref="Me" /> with the provided details.
    /// </summary>
    /// <param name="id">The identifier of this user</param>
    /// <param name="botId">The identifier of this bot</param>
    /// <param name="name">The name of this client</param>
    /// <param name="createdAt">The creation date of this client</param>
    /// <param name="createdBy">The identifier of the creator of this bot</param>
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