using System;
using System.Threading.Tasks;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a document in a document channel.
/// </summary>
/// <seealso cref="ForumThread" />
/// <seealso cref="ListItemBase{T}" />
/// <seealso cref="Message" />
public class TitledContent : ChannelContent<uint, HashId>, IUpdatableContent, IReactibleContent
{
    #region JSON properties

    #region Content
    /// <summary>
    /// Gets the title of <see cref="TitledContent">the titled content</see>.
    /// </summary>
    /// <remarks>
    /// <para>This does not have any Markdown formatting and will not contain <c>\n</c> or other line breaking characters.</para>
    /// </remarks>
    /// <value>Single-line string</value>
    /// <seealso cref="TitledContent" />
    /// <seealso cref="Content" />
    public string Title { get; }
    /// <summary>
    /// Gets the text contents of <see cref="TitledContent">the titled content</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    /// <seealso cref="TitledContent" />
    /// <seealso cref="Title" />
    public string Content { get; }
    #endregion

    /// <summary>
    /// Gets the date when the content were updated.
    /// </summary>
    /// <value>Date?</value>
    /// <seealso cref="TitledContent" />
    /// <seealso cref="ChannelContent{T, S}.CreatedAt" />
    /// <seealso cref="ChannelContent{T, S}.CreatedBy" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TitledContent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the channel content</param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of the server where the channel content are</param>
    /// <param name="title">The title of the channel content</param>
    /// <param name="content">The text contents of the channel content</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the channel content</param>
    /// <param name="createdAt">the date when the channel content were created</param>
    /// <param name="updatedAt">the date when the channel content were recently updated</param>
    /// <returns>New <see cref="TitledContent" /> JSON instance</returns>
    /// <seealso cref="TitledContent" />
    [JsonConstructor]
    public TitledContent(
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

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Title, Content, UpdatedAt) = (title, content, updatedAt);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the emote to add</param>
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    // /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // public async Task RemoveReactionAsync(uint emoteId) =>
    //     await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    #endregion
}