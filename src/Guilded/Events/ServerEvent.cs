using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone updates a <see cref="Server">server</see> or invites the <see cref="GuildedBotClient">client bot</see> to a <see cref="Server">server</see> or gets removed from a <see cref="Server">server</see>.
/// </summary>
public class ServerEvent : IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined, left or the <see cref="Server">server</see> that has been updated.
    /// </summary>
    /// <value>The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</value>
    /// <seealso cref="ServerEvent" />
    /// <seealso cref="ServerId" />
    public Server Server { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined.
    /// </summary>
    /// <value>The identifier of the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</value>
    /// <seealso cref="ServerEvent" />
    /// <seealso cref="Server" />
    public HashId ServerId => Server.Id;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Server.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ServerEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="server">The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</param>
    /// <returns>New <see cref="ServerEvent" /> JSON instance</returns>
    public ServerEvent(Server server) =>
        Server = server;
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone invites the <see cref="GuildedBotClient">client bot</see> to a <see cref="Server">server</see> or gets removed from a <see cref="Server">server</see>.
/// </summary>
public class ServerAddedEvent : ServerEvent, IUserCreated
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see></value>
    /// <seealso cref="ServerAddedEvent" />
    /// <seealso cref="ServerEvent.ServerId" />
    /// <seealso cref="ServerEvent.Server" />
    public HashId CreatedBy { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ServerAddedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="server">The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</param>
    /// <param name="createdBy">The <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see></param>
    /// <returns>New <see cref="ServerAddedEvent" /> JSON instance</returns>
    [JsonConstructor]
    public ServerAddedEvent(
        [JsonProperty(Required = Required.Always)]
        Server server,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy
    ) : base(server) =>
        CreatedBy = createdBy;
    #endregion
}

/// <summary>
/// Represents an event that occurs when someone invites the <see cref="GuildedBotClient">client bot</see> to a <see cref="Server">server</see> or gets removed from a <see cref="Server">server</see>.
/// </summary>
public class ServerRemovedEvent : ServerEvent
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="User">user</see> that removed the <see cref="GuildedBotClient">client bot</see> from the <see cref="Server">server</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> that removed the <see cref="GuildedBotClient">client bot</see> from the <see cref="Server">server</see></value>
    /// <seealso cref="ServerRemovedEvent" />
    /// <seealso cref="ServerEvent.ServerId" />
    /// <seealso cref="ServerEvent.Server" />
    public HashId DeletedBy { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ServerRemovedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="server">The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</param>
    /// <param name="deletedBy">The <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see></param>
    /// <returns>New <see cref="ServerRemovedEvent" /> JSON instance</returns>
    [JsonConstructor]
    public ServerRemovedEvent(
        [JsonProperty(Required = Required.Always)]
        Server server,

        [JsonProperty(Required = Required.Always)]
        HashId deletedBy
    ) : base(server) =>
        DeletedBy = deletedBy;
    #endregion
}
