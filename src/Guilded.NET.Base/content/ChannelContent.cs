using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A base for forum posts, media, announcements, etc..
    /// </summary>
    /// <typeparam name="T">ID type</typeparam>
    public abstract class ChannelContent<T> : ClientObject
    {
        /// <summary>
        /// ID of the content that was posted.
        /// </summary>
        /// <value>Content ID</value>
        [JsonProperty(Required = Required.Always)]
        public T Id
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the author of the content.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the webhook that posted the content.
        /// </summary>
        /// <value>Webhook ID?</value>
        [JsonProperty("createdByWebhookId")]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the bot that posted the content.
        /// </summary>
        /// <value>Bot ID?</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBot
        {
            get; set;
        }
        /// <summary>
        /// The date of when the content were posted.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }

        #region Overrides
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is ChannelContent<T> content && content.Id.Equals(Id);
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            // TODO: Replace CreatedBy with ChannelId
            HashCode.Combine(Id, (object)CreatedBy ?? CreatedByBot ?? CreatedByWebhook);
        #endregion
    }
}