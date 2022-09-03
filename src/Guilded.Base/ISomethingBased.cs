using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Servers;

namespace Guilded.Base;

/// <summary>
/// Represents an interface for <see cref="Server">server</see> items.
/// </summary>
/// <seealso cref="Content.Topic" />
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
    /// <seealso cref="Content.ChannelContent{TId, TServer}.ServerId" />
    HashId ServerId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.GetServerAsync(HashId)" />
    public async Task<Server> GetServerAsync() =>
        await ParentClient.GetServerAsync(ServerId);
    #endregion
}

/// <summary>
/// Represents an interface for items that can be both inside a <see cref="Server">server</see> and outside it.
/// </summary>
/// <seealso cref="Content.Message" />
public interface IGlobalContent : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the item is if it's in a <see cref="Server">server</see>.
    /// </summary>
    /// <value><see cref="Server.Id">Server ID</see>?</value>
    /// <seealso cref="IGlobalContent" />
    /// <seealso cref="Content.ChannelContent{TId, TServer}.ServerId" />
    HashId? ServerId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.GetServerAsync(HashId)" />
    public async Task<Server?> GetServerAsync() =>
        ServerId is not null ? await ParentClient.GetServerAsync((HashId)ServerId) : null;
    #endregion
}

/// <summary>
/// Represents an interface for <see cref="ServerChannel">server channel</see> items.
/// </summary>
/// <seealso cref="Content.ChannelContent{TId, TServer}" />
public interface IChannelBased : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="ServerChannel">channel</see> where the item is.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="IChannelBased" />
    /// <seealso cref="Content.ChannelContent{TId, TServer}.ChannelId" />
    Guid ChannelId { get; }
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.GetChannelAsync(Guid)" />
    public async Task<ServerChannel> GetChannelAsync() =>
        await ParentClient.GetChannelAsync(ChannelId);

    /// <inheritdoc cref="BaseGuildedClient.UpdateChannelAsync(Guid, string?, string?, bool?)" />
    public async Task<ServerChannel> UpdateChannelAsync(string? name = null, string? topic = null, bool? isPublic = null) =>
        await ParentClient.UpdateChannelAsync(ChannelId, name, topic, isPublic).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.DeleteChannelAsync(Guid)" />
    public async Task DeleteChannelAsync() =>
        await ParentClient.DeleteChannelAsync(ChannelId);
    #endregion
}