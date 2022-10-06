using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the base for channel content.
/// </summary>
/// <remarks>
/// <para>This does not include deleted content.</para>
/// </remarks>
/// <typeparam name="TId">The type of <see cref="ChannelContent{TId, TServer}">the content</see> identifier (property <see cref="Id" />)</typeparam>
/// <typeparam name="TServer">The type of the <see cref="Server">server</see> identifier (property <see cref="ServerId" />)</typeparam>
/// <seealso cref="Message" />
/// <seealso cref="Topic" />
/// <seealso cref="ListItem" />
/// <seealso cref="Doc" />
public abstract class ChannelContent<TId, TServer> : ContentModel, IModelHasId<TId>, ICreatableContent, IChannelBased where TId : notnull
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="ChannelContent{TId, TServer}">the content</see>.
    /// </summary>
    /// <value>Content ID</value>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="ServerId" />
    public TId Id { get; }

    /// <summary>
    /// Gets the identifier of the channel where <see cref="ChannelContent{TId, TServer}">the content</see> are.
    /// </summary>
    /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    /// <seealso cref="Id" />
    /// <seealso cref="ServerId" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where <see cref="ChannelContent{TId, TServer}">the content</see> are.
    /// </summary>
    /// <value>Server ID</value>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    public TServer ServerId { get; }

    #region Who, when
    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created <see cref="ChannelContent{TId, TServer}">the content</see>.
    /// </summary>
    /// <remarks>
    /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ChannelContent{TId, TServer}">the content</see> were created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="IUpdatableContent.UpdatedAt" />
    public DateTime CreatedAt { get; }
    #endregion

    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelContent{T, S}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="ChannelContent{TId, TServer}">the content</see></param>
    /// <param name="channelId">The identifier of the channel where <see cref="ChannelContent{TId, TServer}">the content</see> is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where <see cref="ChannelContent{TId, TServer}">the content</see> is</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of <see cref="ChannelContent{TId, TServer}">the content</see></param>
    /// <param name="createdAt">the date when <see cref="ChannelContent{TId, TServer}">the content</see> was created</param>
    /// <returns>New <see cref="ChannelContent{TId, TServer}" /> JSON instance</returns>
    /// <seealso cref="ChannelContent{TId, TServer}" />
    protected ChannelContent(TId id, Guid channelId, TServer serverId, HashId createdBy, DateTime createdAt) =>
        (Id, ChannelId, ServerId, CreatedBy, CreatedAt) = (id, channelId, serverId, createdBy, createdAt);
    #endregion

    #region Methods
    /// <summary>
    /// Returns whether this instance and the <paramref name="other" /> specified instance are equal to each other.
    /// </summary>
    /// <param name="other">Another instance to compare</param>
    /// <returns>Instances are equal</returns>
    /// <seealso cref="GetHashCode" />
    /// <seealso cref="string" />
    public override bool Equals(object? other) =>
        other is ChannelContent<TId, TServer> content && content.ChannelId == ChannelId && content.Id.Equals(Id);

    /// <summary>
    /// Returns a hashcode of this instance.
    /// </summary>
    /// <returns>HashCode</returns>
    /// <seealso cref="Equals" />
    /// <seealso cref="string" />
    public override int GetHashCode() =>
        HashCode.Combine(ChannelId, Id);

    /// <summary>
    /// Returns string equivalent to this instance.
    /// </summary>
    /// <returns>Instance as a string</returns>
    /// <seealso cref="GetHashCode" />
    /// <seealso cref="Equals" />
    public override string ToString() =>
        $"Content {Id}";
    #endregion
}