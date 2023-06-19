using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;

namespace Guilded;

/// <summary>
/// Represents an interface for <see cref="Server">server</see> items.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="Events.MemberJoinedEvent" />
/// <seealso cref="Events.ChannelEvent" />
public interface IServerBased : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the item is.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see></value>
    /// <seealso cref="IServerBased" />
    /// <seealso cref="ChannelContent{TId, TServer}.ServerId" />
    HashId ServerId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetServerAsync(HashId)" />
    public Task<Server> GetServerAsync() =>
        ParentClient.GetServerAsync(ServerId);
    #endregion
}

/// <summary>
/// Represents an interface for items that can be both inside a <see cref="Server">server</see> and outside it.
/// </summary>
/// <seealso cref="Message" />
public interface IGlobalContent : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the item is.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see></value>
    /// <seealso cref="IServerBased" />
    /// <seealso cref="ChannelContent{TId, TServer}.ServerId" />
    HashId? ServerId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetServerAsync(HashId)" />
    public async Task<Server?> GetServerAsync() =>
        ServerId is not null ? await ParentClient.GetServerAsync((HashId)ServerId) : null;
    #endregion
}

/// <summary>
/// Represents an interface for <see cref="ServerChannel">server channel</see> items.
/// </summary>
/// <seealso cref="ChannelContent{TId, TServer}" />
public interface IChannelBased : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">channel</see> where the item is.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="IChannelBased" />
    /// <seealso cref="ChannelContent{TId, TServer}.ChannelId" />
    Guid ChannelId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetChannelAsync(Guid)" />
    public Task<ServerChannel> GetChannelAsync() =>
        ParentClient.GetChannelAsync(ChannelId);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateChannelAsync(Guid, string?, string?, bool?)" />
    public Task<ServerChannel> UpdateChannelAsync(string? name = null, string? topic = null, bool? isPublic = null) =>
        ParentClient.UpdateChannelAsync(ChannelId, name, topic, isPublic);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteChannelAsync(Guid)" />
    public Task DeleteChannelAsync() =>
        ParentClient.DeleteChannelAsync(ChannelId);
    #endregion
}