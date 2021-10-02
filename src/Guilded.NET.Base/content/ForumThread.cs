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
        [JsonProperty(Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// The contents of the forum thread.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the forum thread in Markdown format.</para>
        /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
        /// </remarks>
        /// <value>Content in Markdown</value>
        [JsonProperty(Required = Required.Always)]
        public string Content
        {
            get; set;
        }
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