using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A forum thread in a forum channel.
    /// </summary>
    /// <remarks>
    /// <para>A forum post/thread in forums.</para>
    /// <para>Currently can only be found as a return value from forum thread creation methods.</para>
    /// </remarks>
    /// <seealso cref="Message"/>
    /// <seealso cref="ListItem"/>
    public class ForumThread : ChannelContent<uint>
    {
        #region JSON properties
        /// <summary>
        /// The title of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The title of the forum thread that typically doesn't hold any formatting.</para>
        /// </remarks>
        /// <value>Title</value>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// The contents of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the forum thread formatted in Markdown.</para>
        /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
        /// </remarks>
        /// <value>Content in Markdown</value>
        public string Content
        {
            get; set;
        }
        #endregion

        #region Constructorss
        /// <summary>
        /// Creates a new instance of <see cref="ChannelContent{T}"/> with provided details.
        /// </summary>
        /// <param name="id">The identifier of the content</param>
        /// <param name="channelId">The identifier of the channel where the content is</param>
        /// <param name="title">The title of the forum thread</param>
        /// <param name="content">The contents of the forum thread</param>
        /// <param name="createdBy">The identifier of the user creator of the content</param>
        /// <param name="createdByBotId">The identifier of the bot creator of the content</param>
        /// <param name="createdByWebhookId">The identifier of the webhook creator of the content</param>
        /// <param name="createdAt">The date of when the content was created</param>
        [JsonConstructor]
        public ForumThread(
            [JsonProperty(Required = Required.Always)]
            uint id,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            string title,

            [JsonProperty(Required = Required.Always)]
            string content,

            [JsonProperty(Required = Required.Always)]
            GId createdBy,

            Guid? createdByBotId,

            Guid? createdByWebhookId,

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt
        ) : base(id, channelId, createdBy, createdByBotId, createdByWebhookId, createdAt) =>
            (Title, Content) = (title, content);
        #endregion

        #region Additional
        /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(System.Guid, uint, uint)"/>
        /// <param name="emoteId">The identifier of the emote to add</param>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(System.Guid, uint, uint)"/>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        #endregion
    }
}