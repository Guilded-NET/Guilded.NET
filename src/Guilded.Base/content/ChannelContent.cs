using System;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the base for channel content.
/// </summary>
/// <remarks>
/// <para>This does not include deleted content.</para>
/// </remarks>
/// <typeparam name="T">The type of the content identifier (property <see cref="Id"/>)</typeparam>
/// <typeparam name="S">The type of the server identifier (property <see cref="ServerId"/>)</typeparam>
public abstract class ChannelContent<T, S> : ClientObject, ICreatableContent where T : notnull
{
    #region JSON properties
    /// <summary>
    /// The identifier of the content.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the content that was created. Usually a <see cref="Guid"/>, <see cref="uint"/> or <see cref="HashId"/>.</para>
    /// </remarks>
    /// <value>Content ID</value>
    public T Id { get; }
    /// <summary>
    /// The identifier of the channel where the content is.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of channel where the content was found.</para>
    /// <para>This channel can be of any type and there is no identifying channel type as of now.</para>
    /// </remarks>
    /// <value>Channel ID</value>
    public Guid ChannelId { get; }
    /// <summary>
    /// The identifier of the server where the content is.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the server where the content was found.</para>
    /// <para>The server can be either optional or not optional. This depends whether the content is global or server-wide. Content like forum threads will be server-wide, while content like chat messages and reactions will be global.</para>
    /// </remarks>
    /// <value>Server ID or Server ID?</value>
    public S ServerId { get; }

    #region Who, when
    /// <summary>
    /// Gets the identifier of the user that created the content.
    /// </summary>
    /// <remarks>
    /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId CreatedBy { get; }
    /// <summary>
    /// Gets the date of when the content was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }
    #endregion

    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ChannelContent{T, S}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the content</param>
    /// <param name="channelId">The identifier of the channel where the content is</param>
    /// <param name="serverId">The identifier of the server where the content is</param>
    /// <param name="createdBy">The identifier of the user creator of the content</param>
    /// <param name="createdAt">The date of when the content was created</param>
    protected ChannelContent(T id, Guid channelId, S serverId, HashId createdBy, DateTime createdAt) =>
        (Id, ChannelId, ServerId, CreatedBy, CreatedAt) = (id, channelId, serverId, createdBy, createdAt);
    #endregion

    #region Overrides
    /// <summary>
    /// Returns whether this instance and the <paramref name="other">specified instance</paramref> are equal to each other.
    /// </summary>
    /// <param name="other">Another instance to compare</param>
    /// <returns>Instances are equal</returns>
    public override bool Equals(object? other) =>
        other is ChannelContent<T, S> content && content.ChannelId == ChannelId && content.Id.Equals(Id);
    /// <summary>
    /// Returns a hashcode of this instance.
    /// </summary>
    /// <returns>HashCode</returns>
    public override int GetHashCode() =>
        HashCode.Combine(ChannelId, Id);
    /// <summary>
    /// Returns string equivalent to this instance.
    /// </summary>
    /// <returns>Instance as a string</returns>
    public override string ToString() =>
        $"Content {Id}";
    #endregion
}