using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.NET.Base.Permissions;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A document in a document channel.
    /// </summary>
    /// <remarks>
    /// <para>A document with a <see cref="Title"/> and the <see cref="Content"/>, similarly to <see cref="ForumThread"/>.</para>
    /// </remarks>
    public class Doc : ChannelContent<uint>, IUpdatableContent, IReactibleContent
    {
        #region JSON properties

        #region Content
        /// <summary>
        /// The title of the document.
        /// </summary>
        /// <remarks>
        /// <para>The displayed title of the document.</para>
        /// <para>This does not have any Markdown formatting.</para>
        /// </remarks>
        /// <value>Single-line string</value>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// The contents of the document.
        /// </summary>
        /// <remarks>
        /// <para>The contents of document in Markdown format.</para>
        /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
        /// </remarks>
        /// <value>Markdown string</value>
        public string Content
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// The date of when the document was updated.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the document was updated/edited.</para>
        /// </remarks>
        /// <value>Date?</value>
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the user updater of the document.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user who updated this document. Only includes the person who updated this document most recently.</para>
        /// </remarks>
        /// <value>User ID?</value>
        public GId? UpdatedBy
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
        /// <param name="title">The title of the document</param>
        /// <param name="content">The contents of the document</param>
        /// <param name="createdBy">The identifier of the user creator of the content</param>
        /// <param name="createdAt">The date of when the content was created</param>
        /// <param name="updatedBy">The identifier of the user who recently updated the content</param>
        /// <param name="updatedAt">The date of when the content was recently updated</param>
        [JsonConstructor]
        public Doc(
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

            [JsonProperty(Required = Required.Always)]
            DateTime createdAt,

            GId? updatedBy,

            DateTime? updatedAt
        ) : base(id, channelId, createdBy, createdAt) =>
            (Title, Content, UpdatedBy, UpdatedAt) = (title, content, updatedBy, updatedAt);
        #endregion

        #region Additional
        /// <inheritdoc cref="BaseGuildedClient.UpdateDocAsync(Guid, uint, string, string)"/>
        /// <param name="title">The new title of the document</param>
        /// <param name="content">The Markdown content of the document</param>
        public async Task<Doc> UpdateDocAsync(string title, string content) =>
            await ParentClient.UpdateDocAsync(ChannelId, Id, title, content);
        /// <inheritdoc cref="BaseGuildedClient.DeleteDocAsync(Guid, uint)"/>
        public async Task DeleteDocAsync() =>
            await ParentClient.DeleteDocAsync(ChannelId, Id);
        /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)"/>
        /// <param name="emoteId">The identifier of the emote to add</param>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)"/>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        #endregion
    }
}