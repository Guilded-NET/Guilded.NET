using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone deletes a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone creates/posts a message in the chat.</para>
    /// <para>In API, this event is called <c>ChatMessageDeleted</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeleted"/>
    public class MessageDeletedEvent : MessageEvent<MessageDeletedEvent.MessageDeleted>
    {
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
            [JsonProperty(Required = Required.Always)]
            public Guid Id
            {
                get; set;
            }
            /// <summary>
            /// The identifier of the channel where this message is.
            /// </summary>
            /// <value>Channel ID</value>
            [JsonProperty(Required = Required.Always)]
            public Guid ChannelId
            {
                get; set;
            }
            /// <summary>
            /// The date of when the message was deleted.
            /// </summary>
            /// <remarks>
            /// <para>The <see cref="DateTime"/> of when the message was removed.</para>
            /// <para>This is recorded by the server and all the delays that were
            /// created by the client will be added as well.</para>
            /// </remarks>
            /// <value>Deleted at</value>
            [JsonProperty(Required = Required.Always)]
            public DateTime DeletedAt
            {
                get; set;
            }
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
            public override bool Equals(object obj) =>
                obj is MessageDeleted message && message.ChannelId == ChannelId && message.Id == Id;
            /// <summary>
            /// Gets a hashcode of this object.
            /// </summary>
            /// <returns>HashCode</returns>
            public override int GetHashCode() =>
                HashCode.Combine(ChannelId, Id);
            #endregion
        }

        #region Properties
        /// <inheritdoc cref="MessageDeleted.Id"/>
        public Guid Id => Message.Id;
        /// <inheritdoc cref="MessageDeleted.ChannelId"/>
        public Guid ChannelId => Message.ChannelId;
        /// <inheritdoc cref="MessageDeleted.DeletedAt"/>
        [JsonIgnore]
        public DateTime DeletedAt => Message.DeletedAt;
        #endregion
    }
}