using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// The information about a reaction.
    /// </summary>
    /// <remarks>
    /// <para>Defines a reaction in <see cref="ChannelContent{T, S}"/>. Only currently exists on messages, forum threads, announcements, media, documents and calendar events. Currently doesn't hold the count of all reactions, nor return all reacting users.</para>
    /// </remarks>
    /// <seealso cref="Message"/>
    public class Reaction : ClientObject, IWebhookCreatable
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the emote reacted with.
        /// </summary>
        /// <value>Emote ID</value>
        public uint Id { get; }
        /// <summary>
        /// The identifier of the server where the content is.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the server where the content was found.</para>
        /// <para>The server can be either optional or not optional. This depends whether the content is global or server-wide. Content like forum threads will be server-wide, while content like chat messages and reactions will be global.</para>
        /// </remarks>
        /// <value>Server ID?</value>
        public HashId? ServerId { get; }
        /// <summary>
        /// The date of when this reaction was created.
        /// </summary>
        /// <value>Created at</value>
        public DateTime CreatedAt { get; }
        /// <summary>
        /// The identifier of the user creator of the reaction.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user that created this reaction.</para>
        /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
        /// </remarks>
        /// <value>User ID</value>
        public HashId CreatedBy { get; }
        /// <summary>
        /// The identifier of the webhook creator of the reaction.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the webhook that posted created this reaction.</para>
        /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
        /// </remarks>
        /// <value>Webhook ID?</value>
        public Guid? CreatedByWebhook { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="Reaction"/> with provided details.
        /// </summary>
        /// <param name="id">The identifier of the emote reacted with</param>
        /// <param name="serverId">The identifier of the server where the reaction is</param>
        /// <param name="createdBy">The identifier of the user creator of the reaction</param>
        /// <param name="createdByWebhookId">The identifier of the webhook creator of the reaction</param>
        /// <param name="createdAt">The date of when the reaction was created</param>
        [JsonConstructor]
        public Reaction(
            [JsonProperty(Required = Required.Always)]
            uint id,

            [JsonProperty]
            HashId? serverId,

            [JsonProperty(Required = Required.Always)]
            HashId createdBy,

            [JsonProperty]
            Guid? createdByWebhookId,

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt
        ) =>
            (Id, ServerId, CreatedAt, CreatedBy, CreatedByWebhook) = (id, serverId, createdAt, createdBy, createdByWebhookId);
        #endregion

        #region Overrides
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object? obj) =>
            obj is Reaction reaction && Id == reaction.Id;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, CreatedAt);
        #endregion
    }
}