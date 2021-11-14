using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A list item in a list channel.
    /// </summary>
    /// <remarks>
    /// <para>A list item in a list channel with an optional <see cref="Note"/>.</para>
    /// <para>It can only be found as a return value when creating a list item.</para>
    /// </remarks>
    /// <seealso cref="Message"/>
    /// <seealso cref="ForumThread"/>
    public class ListItem : ChannelContent<Guid>
    {
        #region JSON properties
        /// <summary>
        /// The contents of the message in the item.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the list item formatted in Markdown.</para>
        /// </remarks>
        /// <value>Content</value>
        public string Message
        {
            get; set;
        }
        /// <summary>
        /// The contents of the note in the item.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the list item's note formatted in Markdown.</para>
        /// </remarks>
        /// <value>Content</value>
        public string? Note
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ChannelContent{T}"/> with provided details.
        /// </summary>
        /// <param name="id">The identifier of the content</param>
        /// <param name="channelId">The identifier of the channel where the content is</param>
        /// <param name="message">The contents of the message in list item</param>
        /// <param name="note">The contents of the note in list item</param>
        /// <param name="createdBy">The identifier of the user creator of the content</param>
        /// <param name="createdByBotId">The identifier of the bot creator of the content</param>
        /// <param name="createdByWebhookId">The identifier of the webhook creator of the content</param>
        /// <param name="createdAt">The date of when the content was created</param>
        [JsonConstructor]
        public ListItem(
            [JsonProperty(Required = Required.Always)]
            Guid id,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            string message,

            string? note,

            [JsonProperty(Required = Required.Always)]
            GId createdBy,

            Guid? createdByBotId,

            Guid? createdByWebhookId,

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt
        ) : base(id, channelId, createdBy, createdByBotId, createdByWebhookId, createdAt) =>
            (Message, Note) = (message, note);
        #endregion
    }
}