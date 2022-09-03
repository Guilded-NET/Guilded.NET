using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes <see cref="ServerChannel">a channel</see>.
/// </summary>
/// <seealso cref="ListItemEvent" />
/// <seealso cref="DocEvent" />
/// <seealso cref="MessageEvent" />
public class ChannelEvent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="ServerChannel">the channel</see> received from the event.
    /// </summary>
    /// <value><see cref="ServerChannel">Channel</see></value>
    /// <seealso cref="ChannelEvent" />
    /// <seealso cref="Name" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServerId" />
    public ServerChannel Channel { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }

    /// <inheritdoc cref="ServerChannel.GroupId" />
    public HashId? GroupId => Channel.GroupId;

    /// <inheritdoc cref="ServerChannel.CategoryId" />
    public uint? CategoryId => Channel.CategoryId;

    /// <inheritdoc cref="ServerChannel.ParentId" />
    public Guid? ParentId => Channel.ParentId;

    /// <inheritdoc cref="ServerChannel.Name" />
    public string Name => Channel.Name;

    /// <inheritdoc cref="ServerChannel.Topic" />
    public string? Topic => Channel.Topic;

    /// <inheritdoc cref="ServerChannel.ParentId" />
    public ChannelType Type => Channel.Type;

    /// <inheritdoc cref="ServerChannel.CreatedBy" />
    public HashId CreatedBy => Channel.CreatedBy;

    /// <inheritdoc cref="ServerChannel.CreatedAt" />
    public DateTime CreatedAt => Channel.CreatedAt;

    /// <inheritdoc cref="ServerChannel.UpdatedAt" />
    public DateTime? UpdatedAt => Channel.UpdatedAt;

    /// <inheritdoc cref="ServerChannel.ArchivedBy" />
    public HashId? ArchivedBy => Channel.ArchivedBy;

    /// <inheritdoc cref="ServerChannel.ArchivedAt" />
    public DateTime? ArchivedAt => Channel.ArchivedAt;

    /// <inheritdoc cref="ServerChannel.IsArchived" />
    public bool IsArchived => Channel.IsArchived;

    /// <inheritdoc cref="ServerChannel.IsThread" />
    public bool IsThread => Channel.IsThread;

    /// <inheritdoc cref="ServerChannel.IsCategorized" />
    public bool IsCategorized => Channel.IsCategorized;

    /// <inheritdoc cref="ServerChannel.IsPublic" />
    public bool IsPublic => Channel.IsPublic;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the channel event occurred</param>
    /// <param name="channel">The channel received from the event</param>
    /// <returns>New <see cref="ChannelEvent" /> JSON instance</returns>
    /// <seealso cref="ChannelEvent" />
    [JsonConstructor]
    public ChannelEvent(
        [JsonProperty(Required = Required.Always)]
        ServerChannel channel,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Channel) = (serverId, channel);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.DeleteChannelAsync(Guid)" />
    public Task<ServerChannel> UpdateAsync(string? name = null, string? topic = null, bool? isPublic = null) =>
        Channel.UpdateAsync(name, topic, isPublic);

    /// <inheritdoc cref="ServerChannel.DeleteAsync" />
    public Task DeleteAsync() =>
        Channel.DeleteAsync();

    /// <inheritdoc cref="ServerChannel.CreateWebhookAsync(string)" />
    public Task<Webhook> CreateWebhookAsync(string name) =>
        Channel.CreateWebhookAsync(name);
    #endregion
}