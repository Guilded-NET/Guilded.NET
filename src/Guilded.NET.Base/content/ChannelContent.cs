using System;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// The base type for channel contents.
    /// </summary>
    /// <remarks>
    /// <para>Defines the base type for all channel contents, apart from deleted ones.</para>
    /// </remarks>
    /// <typeparam name="T">The type of the content identifier (property <see cref="Id"/>)</typeparam>
    /// <typeparam name="S">The type of the server identifier (property <see cref="ServerId"/>)</typeparam>
    public abstract class ChannelContent<T, S> : ClientObject where T : notnull
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the content.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the content that was created. Usually a <see cref="Guid"/>, <see cref="uint"/> or <see cref="HashId"/>.</para>
        /// </remarks>
        /// <value>Content ID</value>
        public T Id
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the channel where the content is.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of channel where the content was found.</para>
        /// <para>This channel can be of any type and there is no identifying channel type as of now.</para>
        /// </remarks>
        /// <value>Channel ID</value>
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the server where the content is.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the server where the content was found.</para>
        /// <para>The server can be either optional or not optional. This depends whether the content is global or server-wide. Content like forum threads will be server-wide, while content like chat messages and reactions will be global.</para>
        /// </remarks>
        /// <value>Server ID or Server ID?</value>
        public S ServerId
        {
            get; set;
        }

        #region Who, when
        /// <summary>
        /// The identifier of the user creator of the content.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user that created this content.</para>
        /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
        /// </remarks>
        /// <value>User ID</value>
        public HashId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The date of when the content was created.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the content was created.</para>
        /// </remarks>
        /// <value>Created at</value>
        public DateTime CreatedAt
        {
            get; set;
        }
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ChannelContent{T,S}"/> with provided details.
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
        /// Returns whether this instance and specified instance are equal to each other.
        /// </summary>
        /// <param name="obj">Another instance to compare</param>
        /// <returns>Instances are equal</returns>
        public override bool Equals(object? obj) =>
            obj is ChannelContent<T, S> content && content.ChannelId == ChannelId && content.Id.Equals(Id);
        /// <summary>
        /// Gets a hashcode of this instance.
        /// </summary>
        /// <returns>Instance's HashCode</returns>
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
}