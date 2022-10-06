using System;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// /// Represents a team or a guild in Guilded.
/// </summary>
public class Server : ContentModel, IModelHasId<HashId>
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see>.
    /// </summary>
    /// <value><see cref="Id">Server ID</see></value>
    /// <seealso cref="Server" />
    /// <seealso cref="DefaultChannelId" />
    /// <seealso cref="OwnerId" />
    /// <seealso cref="Name" />
    /// <seealso cref="Url" />
    public HashId Id { get; }

    /// <summary>
    /// Gets the displayed name of the <see cref="Server">server</see>.
    /// </summary>
    /// <value>Unique Name</value>
    /// <seealso cref="Server" />
    /// <seealso cref="About" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Banner" />
    /// <seealso cref="Id" />
    public string Name { get; }

    /// <summary>
    /// Gets the part of URL to the the <see cref="Server">server</see>.
    /// </summary>
    /// <example>
    /// <para>If <see cref="Url" /> is set as <c>GuildedNET</c>, then URL to the <see cref="Server">server</see> will be <see href="https://guilded.gg/GuildedNET" />.</para>
    /// </example>
    /// <value><see cref="Uri" /> Directory</value>
    /// <seealso cref="Server" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Banner" />
    public string? Url { get; }

    /// <summary>
    /// Gets the description of the <see cref="Server">server</see>.
    /// </summary>
    /// <value>Single-line String</value>
    /// <seealso cref="Server" />
    /// <seealso cref="Name" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Banner" />
    public string? About { get; }

    /// <summary>
    /// Gets the selected <see cref="ServerType">type</see> of the <see cref="Server">server</see>.
    /// </summary>
    /// <value><see cref="ServerType">Server Type</see></value>
    /// <seealso cref="Server" />
    /// <seealso cref="Banner" />
    /// <seealso cref="Name" />
    /// <seealso cref="About" />
    public ServerType? Type { get; }

    /// <summary>
    /// Gets <see cref="Uri">the URL</see> to the icon image of the <see cref="Server">server</see>.
    /// </summary>
    /// <value>Media URL</value>
    /// <seealso cref="Server" />
    /// <seealso cref="Banner" />
    /// <seealso cref="Name" />
    /// <seealso cref="About" />
    public Uri? Avatar { get; }

    /// <summary>
    /// Gets <see cref="Uri">the URL</see> to the banner image of the <see cref="Server">server</see>.
    /// </summary>
    /// <value>Media URL</value>
    /// <seealso cref="Server" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Name" />
    /// <seealso cref="About" />
    public Uri? Banner { get; }

    /// <summary>
    /// Gets the set timezone of the <see cref="Server">server</see>.
    /// </summary>
    /// <value><see cref="Uri">Media URL</see></value>
    /// <seealso cref="Server" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Name" />
    /// <seealso cref="About" />
    public string? Timezone { get; }

    /// <summary>
    /// Gets whether the <see cref="Server">server</see> is verified by the Guilded staff team.
    /// </summary>
    /// <value>Server is verified</value>
    /// <seealso cref="Server" />
    /// <seealso cref="Avatar" />
    /// <seealso cref="Name" />
    /// <seealso cref="About" />
    public bool IsVerified { get; }

    /// <summary>
    /// Gets <see cref="ServerChannel">the channel</see> of the <see cref="Server">server</see> that is the main channel.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="Server" />
    /// <seealso cref="Id" />
    /// <seealso cref="OwnerId" />
    public Guid? DefaultChannelId { get; }

    /// <summary>
    /// Gets <see cref="Users.User">the user</see> that created the <see cref="Server">server</see>.
    /// </summary>
    /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
    /// <seealso cref="Server" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="Id" />
    /// <seealso cref="DefaultChannelId" />
    public HashId OwnerId { get; }

    /// <summary>
    /// Gets the date when the <see cref="Server">server</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Server" />
    /// <seealso cref="OwnerId" />
    public DateTime CreatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Server" /> from specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Server">server</see></param>
    /// <param name="ownerId"><see cref="Users.User">The user</see> that created the <see cref="Server">server</see></param>
    /// <param name="name">The displayed name of the <see cref="Server">server</see></param>
    /// <param name="createdAt">The date when the <see cref="Server">server</see> was created</param>
    /// <param name="type">The selected <see cref="ServerType">type</see> of the <see cref="Server">server</see></param>
    /// <param name="url">The part of URL to the the <see cref="Server">server</see></param>
    /// <param name="about">The description of the <see cref="Server">server</see></param>
    /// <param name="avatar"><see cref="Uri">The URL</see> to the icon image of the <see cref="Server">server</see></param>
    /// <param name="banner"><see cref="Uri">The URL</see> to the banner image of the <see cref="Server">server</see></param>
    /// <param name="timezone">The set timezone of the <see cref="Server">server</see></param>
    /// <param name="isVerified">Whether the <see cref="Server">server</see> is verified by the Guilded staff team</param>
    /// <param name="defaultChannelId"><see cref="ServerChannel">The channel</see> of the <see cref="Server">server</see> that is the main channel</param>
    /// <returns><see cref="Server" /> from JSON</returns>
    [JsonConstructor]
    public Server(
        [JsonProperty(Required = Required.Always)]
        HashId id,

        [JsonProperty(Required = Required.Always)]
        HashId ownerId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ServerType? type = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? url = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? about = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? avatar = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? banner = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? timezone = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isVerified = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? defaultChannelId = null
    ) =>
        (Id, OwnerId, Type, Name, Url, About, Avatar, Banner, Timezone, IsVerified, DefaultChannelId, CreatedAt) = (id, ownerId, type, name, url, about, avatar, banner, timezone, isVerified, defaultChannelId, createdAt);
    #endregion
}