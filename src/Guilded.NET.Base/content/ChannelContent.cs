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
    /// <typeparam name="T">The type of the identifier <see cref="Id"/></typeparam>
    public abstract class ChannelContent<T> : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the content.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the content that was created. Usually a <see cref="Guid"/>, <see cref="uint"/> or <see cref="GId"/>.</para>
        /// </remarks>
        /// <value>Content ID</value>
        [JsonProperty(Required = Required.Always)]
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
        [JsonProperty(Required = Required.Always)]
        public Guid ChannelId
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
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the webhook creator of the content.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the webhook that posted created this content.</para>
        /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
        /// </remarks>
        /// <value>Webhook ID?</value>
        [JsonProperty("createdByWebhookId")]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the bot creator of the content.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the flow bot that created this content.</para>
        /// </remarks>
        /// <value>Bot ID?</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBot
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
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        #endregion

        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the content was created by a bot or a webhook.
        /// </summary>
        /// <remarks>
        /// <para>Whether the content was automatically created by a bot or a webhook.</para>
        /// <para>This relies on <see cref="ChannelContent{T}.CreatedByBot"/> and <see cref="ChannelContent{T}.CreatedByWebhook"/> properties. If one of them is not <see langword="null"/>, <see langword="true"/> will be returned. Otherwise, <see langword="false"/> will be returned.</para>
        /// </remarks>
        /// <returns>Created by bot or webhook</returns>
        [JsonIgnore]
        public bool CreatedAuto => !(CreatedByBot is null) || !(CreatedByWebhook is null);
        #endregion

        #region Overrides
        /// <summary>
        /// Returns whether this instance and specified instance are equal to each other.
        /// </summary>
        /// <param name="obj">Another instance to compare</param>
        /// <returns>Instances are equal</returns>
        public override bool Equals(object obj) =>
            obj is ChannelContent<T> content && content.ChannelId == ChannelId && content.Id.Equals(Id);
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