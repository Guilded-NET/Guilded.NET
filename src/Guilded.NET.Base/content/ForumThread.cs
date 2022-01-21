using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A forum thread in a forum channel.
    /// </summary>
    /// <remarks>
    /// <para>A forum post/thread with a <see cref="Title"/> and <see cref="Content"/>, similarly to <see cref="Doc"/>.</para>
    /// <para>Currently can only be found as a return value from forum thread creation methods.</para>
    /// </remarks>
    /// <seealso cref="Message"/>
    /// <seealso cref="ListItem"/>
    public class ForumThread : ChannelContent<uint, HashId>, IReactibleContent, IWebhookCreatable
    {
        #region JSON properties
        /// <summary>
        /// The title of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The title of the forum thread that typically doesn't hold any formatting.</para>
        /// </remarks>
        /// <value>Single-line string</value>
        public string Title { get; }
        /// <summary>
        /// The contents of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the forum thread formatted in Markdown.</para>
        /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
        /// </remarks>
        /// <value>Markdown string</value>
        public string Content { get; }
        /// <summary>
        /// The identifier of the webhook creator of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the webhook that posted created this forum thread.</para>
        /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
        /// </remarks>
        /// <value>Webhook ID?</value>
        public Guid? CreatedByWebhook { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ForumThread"/> with provided details.
        /// </summary>
        /// <param name="id">The identifier of the forum thread</param>
        /// <param name="channelId">The identifier of the channel where the forum thread is</param>
        /// <param name="serverId">The identifier of the server where the forum thread is</param>
        /// <param name="title">The title of the forum thread</param>
        /// <param name="content">The contents of the forum thread</param>
        /// <param name="createdBy">The identifier of the user creator of the forum thread</param>
        /// <param name="createdByWebhookId">The identifier of the webhook creator of the forum thread</param>
        /// <param name="createdAt">The date of when the forum thread was created</param>
        [JsonConstructor]
        public ForumThread(
            [JsonProperty(Required = Required.Always)]
            uint id,

            [JsonProperty(Required = Required.Always)]
            Guid channelId,

            [JsonProperty(Required = Required.Always)]
            HashId serverId,

            [JsonProperty(Required = Required.Always)]
            string title,

            [JsonProperty(Required = Required.Always)]
            string content,

            [JsonProperty(Required = Required.Always)]
            HashId createdBy,

            [JsonProperty]
            Guid? createdByWebhookId,

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt
        ) : base(id, channelId, serverId, createdBy, createdAt) =>
            (Title, Content, CreatedByWebhook) = (title, content, createdByWebhookId);
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