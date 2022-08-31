using System;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when someone deletes <see cref="Message">a message</see>.
/// </summary>
/// <seealso cref="MessageDeleted" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="Message" />
public class MessageDeletedEvent : MessageEvent<MessageDeletedEvent.MessageDeleted>
{
    #region Properties
    /// <inheritdoc cref="MessageDeleted.Id" />
    public Guid Id => Message.Id;

    /// <inheritdoc cref="MessageDeleted.ChannelId" />
    public Guid ChannelId => Message.ChannelId;

    /// <inheritdoc cref="MessageDeleted.DeletedAt" />
    public DateTime DeletedAt => Message.DeletedAt;

    /// <inheritdoc cref="MessageDeleted.IsPrivate" />
    public bool IsPrivate => Message.IsPrivate;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageDeletedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the message was deleted</param>
    /// <param name="message">The minimal information about the deleted message</param>
    /// <returns>New <see cref="MessageDeletedEvent" /> JSON instance</returns>
    /// <seealso cref="MessageDeletedEvent" />
    [JsonConstructor]
    public MessageDeletedEvent(
        [JsonProperty(Required = Required.Always)]
        MessageDeleted message,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId
    ) : base(serverId, message) { }
    #endregion

    /// <summary>
    /// Represents a message that was recently deleted/removed.
    /// </summary>
    /// <seealso cref="Message" />
    /// <seealso cref="MessageDeletedEvent" />
    public class MessageDeleted :
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of the message.
        /// </summary>
        /// <value>Message ID</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the identifier of the channel where the message was.
        /// </summary>
        /// <value><see cref="Servers.ServerChannel.Id">Channel ID</see></value>
        public Guid ChannelId { get; }

        /// <summary>
        /// Gets the identifier of <see cref="Server">the server</see> where the message was.
        /// </summary>
        /// <value>Server ID?</value>
        public HashId? ServerId { get; }

        /// <summary>
        /// Gets the date when the message was deleted.
        /// </summary>
        /// <value>Date</value>
        public DateTime DeletedAt { get; }

        /// <summary>
        /// Gets whether the deleted message was <see cref="Message.IsPrivate">private mention</see> or a <see cref="Message.IsPrivate">private reply</see>.
        /// </summary>
        /// <value><see cref="Message" /> is private</value>
        public bool IsPrivate { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// The identifier of the message.
        /// </summary>
        /// <param name="id">The identifier of the message</param>
        /// <param name="channelId">The identifier of the channel where the message was</param>
        /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the message was</param>
        /// <param name="isPrivate">Whether the deleted message was <see cref="Message.IsPrivate">private mention</see> or a <see cref="Message.IsPrivate">private reply</see></param>
        /// <param name="deletedAt">the date when the message was deleted</param>
        /// <returns>New <see cref="MessageDeleted" /> JSON instance</returns>
        /// <seealso cref="MessageDeleted" />
        [JsonConstructor]
        public MessageDeleted(
            [JsonProperty(Required = Required.Always)]
            Guid id,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            DateTime deletedAt,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            HashId? serverId = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            bool isPrivate = false
        ) =>
            (Id, ChannelId, ServerId, DeletedAt, IsPrivate) = (id, channelId, serverId, deletedAt, isPrivate);
        #endregion

        #region Methods
        /// <summary>
        /// Creates string equivalent of the message.
        /// </summary>
        /// <returns>Message as string</returns>
        public override string ToString() =>
            $"Content {Id}";

        /// <summary>
        /// Returns whether this and <paramref name="obj" /> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object? obj) =>
            obj is MessageDeleted message && message.ChannelId == ChannelId && message.Id == Id;

        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(ChannelId, Id);
        #endregion
    }
}