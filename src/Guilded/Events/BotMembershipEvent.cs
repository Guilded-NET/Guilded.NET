using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="CalendarEvent">calendar event</see>.
/// </summary>
public class BotMembershipEvent : IServerBased, IUserCreated
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined.
    /// </summary>
    /// <value>The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</value>
    /// <seealso cref="BotMembershipEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="CreatedBy" />
    public Server Server { get; }

    /// <summary>
    /// Gets the <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see>.
    /// </summary>
    /// <value>The <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see></value>
    /// <seealso cref="BotMembershipEvent" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Server" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined.
    /// </summary>
    /// <value>The identifier of the <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</value>
    /// <seealso cref="BotMembershipEvent" />
    /// <seealso cref="Server" />
    /// <seealso cref="CreatedBy" />
    public HashId ServerId => Server.Id;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Server.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="BotMembershipEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="server">The <see cref="Servers.Server">server</see> that the <see cref="GuildedBotClient">client bot</see> has joined</param>
    /// <param name="createdBy">The <see cref="User">user</see> that added the <see cref="GuildedBotClient">client bot</see> to the <see cref="Server">server</see></param>
    /// <returns>New <see cref="BotMembershipEvent" /> JSON instance</returns>
    [JsonConstructor]
    public BotMembershipEvent(
        [JsonProperty(Required = Required.Always)]
        Server server,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy
    ) =>
        (Server, CreatedBy) = (server, createdBy);
    #endregion
}