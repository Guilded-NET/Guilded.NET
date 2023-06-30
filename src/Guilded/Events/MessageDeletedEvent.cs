using System;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone deletes a <see cref="Message">message</see>.
/// </summary>
/// <seealso cref="MessageDeleted" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="Message" />
public class MessageDeletedEvent : MessageEvent<MessageDeletedEvent.MessageDeleted>
{
    #region Properties
    /// <inheritdoc cref="MessageDeleted.DeletedAt" />
    public DateTime DeletedAt => Message.DeletedAt;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MessageDeletedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the message was deleted</param>
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
    public class MessageDeleted : ContentModel, IModelHasId<Guid>, IPrivatableContent, IChannelBased, IGlobalContent
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of the <see cref="MessageDeleted">deleted message</see>.
        /// </summary>
        /// <value>The identifier of the <see cref="MessageDeleted">deleted message</see></value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the identifier of the <see cref="ServerChannel">channel</see> where the <see cref="MessageDeleted">deleted message</see> was.
        /// </summary>
        /// <value>The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="MessageDeleted">deleted message</see> was</value>
        public Guid ChannelId { get; }

        /// <summary>
        /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="MessageDeleted">deleted message</see> was.
        /// </summary>
        /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="MessageDeleted">deleted message</see> was</value>
        public HashId? ServerId { get; }

        /// <summary>
        /// Gets the date when the <see cref="MessageDeleted">message</see> was deleted.
        /// </summary>
        /// <value>The date when the <see cref="MessageDeleted">message</see> was deleted</value>
        public DateTime DeletedAt { get; }

        /// <summary>
        /// Gets whether the <see cref="MessageDeleted">deleted message</see> was a <see cref="Message.IsPrivate">private mention</see> or a <see cref="Message.IsPrivate">private reply</see>.
        /// </summary>
        /// <value>Whether the <see cref="MessageDeleted">deleted message</see> was a <see cref="Message.IsPrivate">private mention</see> or a <see cref="Message.IsPrivate">private reply</see></value>
        public bool IsPrivate { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="MessageDeleted" /> from the specified JSON properties.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="MessageDeleted">deleted message</see></param>
        /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="MessageDeleted">deleted message</see> was</param>
        /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="MessageDeleted">deleted message</see> was</param>
        /// <param name="isPrivate">Whether the <see cref="MessageDeleted">deleted message</see> was a <see cref="Message.IsPrivate">private mention</see> or a <see cref="Message.IsPrivate">private reply</see></param>
        /// <param name="deletedAt">The date when the <see cref="MessageDeleted">message</see> was deleted</param>
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
        /// Returns a simple representation of the <see cref="MessageDeleted">deleted message</see>.
        /// </summary>
        /// <returns>A simple representation of the <see cref="MessageDeleted">deleted message</see></returns>
        public override string ToString() =>
            $"Deleted Message {Id}";

        /// <summary>
        /// Returns whether the current <see cref="MessageDeleted" /> instance and the <paramref name="obj">given parameter</paramref> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Whether the current <see cref="MessageDeleted" /> instance and the <paramref name="obj">given parameter</paramref> are equal to each other</returns>
        public override bool Equals(object? obj) =>
            obj is MessageDeleted message && message.ChannelId == ChannelId && message.Id == Id;

        /// <summary>
        /// Gets the hashcode of the <see cref="MessageDeleted">deleted message</see>.
        /// </summary>
        /// <returns>The hashcode of the <see cref="MessageDeleted">deleted message</see></returns>
        public override int GetHashCode() =>
            HashCode.Combine(ChannelId, Id);
        #endregion
    }
}