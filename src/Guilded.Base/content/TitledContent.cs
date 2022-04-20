using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a document in a document channel.
/// </summary>
/// <seealso cref="ForumThread" />
/// <seealso cref="ListItem{T}" />
/// <seealso cref="Message" />
public class TitledContent : ChannelContent<uint, HashId>, IUpdatableContent, IReactibleContent
{
    #region JSON properties

    #region Content
    /// <summary>
    /// Gets the title of the document.
    /// </summary>
    /// <remarks>
    /// <para>This does not have any Markdown formatting and will not contain <c>\n</c> or other line breaking characters.</para>
    /// </remarks>
    /// <value>Single-line string</value>
    public string Title { get; }
    /// <summary>
    /// Gets the text contents of the document.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    public string Content { get; }
    #endregion

    /// <summary>
    /// Gets the date of when the content were updated.
    /// </summary>
    /// <value>Date?</value>
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TitledContent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the channel content</param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of the server where the channel content are</param>
    /// <param name="title">The title of the channel content</param>
    /// <param name="content">The text contents of the channel content</param>
    /// <param name="createdBy">The identifier of the user that created the channel content</param>
    /// <param name="createdAt">The date of when the channel content were created</param>
    /// <param name="updatedAt">The date of when the channel content were recently updated</param>
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

        [JsonProperty]
        DateTime? updatedAt
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Title, Content, UpdatedAt) = (title, content, updatedAt);
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)"/>
    /// <param name="emoteId">The identifier of the emote to add</param>
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    // /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)"/>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // public async Task RemoveReactionAsync(uint emoteId) =>
    //     await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    #endregion
}