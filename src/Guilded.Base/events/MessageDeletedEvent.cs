using System;
using Newtonsoft.Json;

namespace Guilded.Base.Events
{
    /// <summary>
    /// An event that occurs once someone deletes a message.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>ChatMessageDeleted</c> and opcode <c>0</c> that occurs once someone creates/posts a message in the chat.</para>
    /// </remarks>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeleted"/>
    /// <seealso cref="Content.Message"/>
    public class MessageDeletedEvent : MessageEvent<MessageDeletedEvent.MessageDeleted>
    {
        #region Properties
        /// <inheritdoc cref="MessageDeleted.Id"/>
        public Guid Id => Message.Id;
        /// <inheritdoc cref="MessageDeleted.ChannelId"/>
        public Guid ChannelId => Message.ChannelId;
        /// <inheritdoc cref="MessageDeleted.DeletedAt"/>
        public DateTime DeletedAt => Message.DeletedAt;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="MessageDeletedEvent"/>. This is currently only used in deserialization.
        /// </summary>
        /// <param name="serverId">The identifier of the server where the message was deleted</param>
        /// <param name="message">The minimal information about the deleted message</param>
        [JsonConstructor]
        public MessageDeletedEvent(
            [JsonProperty(Required = Required.Always)]
            HashId serverId,

            [JsonProperty(Required = Required.Always)]
            MessageDeleted message
        ) : base(serverId, message) { }
        #endregion

        /// <summary>
        /// A message that was recently deleted/removed.
        /// </summary>
        /// <remarks>
        /// <para>A no longer existing message that was deleted/removed by an author of this message or by a server staff.</para>
        /// </remarks>
        /// <seealso cref="Content.Message"/>
        /// <seealso cref="MessageDeletedEvent"/>
        public class MessageDeleted : BaseObject
        {
            #region JSON properties
            /// <summary>
            /// The identifier of the message.
            /// </summary>
            /// <value>Message ID</value>
            public Guid Id { get; }
            /// <summary>
            /// The identifier of the channel where the message was.
            /// </summary>
            /// <value>Channel ID</value>
            public Guid ChannelId { get; }
            /// <summary>
            /// The identifier of the server where the message was.
            /// </summary>
            /// <value>Server ID?</value>
            public HashId? ServerId { get; }
            /// <summary>
            /// The date of when the message was deleted.
            /// </summary>
            /// <remarks>
            /// <para>The <see cref="DateTime"/> of when the message was removed.</para>
            /// </remarks>
            /// <value>Deleted at</value>
            public DateTime DeletedAt { get; }
            #endregion

            #region Constructors
            /// <summary>
            /// The identifier of the message.
            /// </summary>
            /// <param name="id">The identifier of the message</param>
            /// <param name="channelId">The identifier of the channel where the message was</param>
            /// <param name="serverId">The identifier of the server where the message was</param>
            /// <param name="deletedAt">The date of when the message was deleted</param>
            [JsonConstructor]
            public MessageDeleted(
                [JsonProperty(Required = Required.Always)]
                Guid id,

                [JsonProperty(Required = Required.Always)]
                Guid channelId,

                [JsonProperty]
                HashId serverId,

                [JsonProperty(Required = Required.Always)]
                DateTime deletedAt
            ) =>
                (Id, ChannelId, ServerId, DeletedAt) = (id, channelId, serverId, deletedAt);
            #endregion

            #region Overrides
            /// <summary>
            /// Creates string equivalent of the message.
            /// </summary>
            /// <returns>Message as string</returns>
            public override string ToString() =>
                $"Content {Id}";
            /// <summary>
            /// Returns whether this and <paramref name="obj"/> are equal to each other.
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
}