using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Permissions;
    /// <summary>
    /// A forum thread in a forum channel.
    /// </summary>
    /// <remarks>
    /// <para>A forum post/thread in forums that holds <see cref="Content"/> and a short <see cref="Title"/>.</para>
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
        /// <summary>
        /// Adds a reaction to a forum thread.
        /// </summary>
        /// <remarks>
        /// <para>Adds a reaction of identifier <paramref name="emoteId"/> to the message.</para>
        /// </remarks>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ForumPermissions.ReadForums">Required for adding a reaction to a forum thread you see</permission>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a forum thread.
        /// </summary>
        /// <remarks>
        /// <para>Remove a reaction of identifier <paramref name="emoteId"/> from the message.</para>
        /// </remarks>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ForumPermissions.ReadForums">Required for removing a reaction from a forum thread you see</permission>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        #endregion
    }
}