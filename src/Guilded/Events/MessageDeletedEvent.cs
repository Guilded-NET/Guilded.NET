using System;
using System.Collections.Generic;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
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
    public class MessageDeleted : Message
    {
        #region Properties
        /// <summary>
        /// Gets the date when the <see cref="MessageDeleted">message</see> was deleted.
        /// </summary>
        /// <value>The date when the <see cref="MessageDeleted">message</see> was deleted</value>
        public DateTime DeletedAt { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="MessageDeleted" /> from the specified JSON properties.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Message">message</see></param>
        /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="Message">message</see> is</param>
        /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Message">message</see> is</param>
        /// <param name="content">The text contents of the <see cref="Message">message</see></param>
        /// <param name="replyMessageIds">The list of <see cref="Message">messages</see> that the current <see cref="Message">message</see> is replying to</param>
        /// <param name="hiddenLinkPreviewUrls">The list of links that will not be <see cref="Embed">embeded</see> in the <see cref="Message">message</see></param>
        /// <param name="embeds">The list of <see cref="Embed">custom embeds</see> that are part of the <see cref="Message">message's</see> contents</param>
        /// <param name="isPrivate">Whether the reply or mention is private</param>
        /// <param name="isSilent">Whether the reply or mention is silent and doesn't ping any user</param>
        /// <param name="mentions">The <see cref="Mentions">mentions</see> found in the <see cref="Content">content</see></param>
        /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the message</param>
        /// <param name="createdByWebhookId">The identifier of the <see cref="Webhook">webhook</see> that created the <see cref="Message">message</see></param>
        /// <param name="createdAt">The date when the <see cref="Message">message</see> was created</param>
        /// <param name="deletedAt">The date when the <see cref="MessageDeleted">message</see> was deleted</param>
        /// <param name="updatedAt">The date when the <see cref="Message">message</see> was edited</param>
        /// <param name="type">The type of the <see cref="Message">message</see></param>
        /// <returns>New <see cref="MessageDeleted" /> JSON instance</returns>
        /// <seealso cref="MessageDeleted" />
        [JsonConstructor]
        public MessageDeleted(
            [JsonProperty(Required = Required.Always)]
            Guid id,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            HashId createdBy,

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt,

            [JsonProperty(Required = Required.Always)]
            DateTime deletedAt,

            [JsonProperty(Required = Required.Always)]
            MessageType type,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            HashId? serverId = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            IList<Guid>? replyMessageIds = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            ISet<Uri>? hiddenLinkPreviewUrls = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            string? content = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            IList<Embed>? embeds = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            bool isPrivate = false,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            bool isSilent = false,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            Mentions? mentions = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            Guid? createdByWebhookId = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            DateTime? updatedAt = null
        ) : base(id, channelId, createdBy, createdAt, type, serverId, replyMessageIds, hiddenLinkPreviewUrls, content, embeds, isPrivate, isSilent, mentions, createdByWebhookId, updatedAt) =>
            DeletedAt = deletedAt;
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