using System;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a document in a document channel.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
/// <seealso cref="ListItemBase{T}" />
/// <seealso cref="Message" />
public abstract class TitledContent : ChannelContent<uint, HashId>, IUpdatableContent, IReactibleContent, IServerBased
{
    #region Properties

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
    #endregion

    /// <summary>
    /// Gets the date when <see cref="TitledContent">the titled content</see> were updated.
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
    /// <param name="id">The identifier of <see cref="TitledContent">the titled content</see></param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the channel content are</param>
    /// <param name="title">The title of <see cref="TitledContent">the titled content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created <see cref="ChannelContent{TId, TServer}">the content</see></param>
    /// <param name="createdAt">The date when <see cref="ChannelContent{TId, TServer}">the content</see> were created</param>
    /// <param name="updatedAt">The date when <see cref="TitledContent">the titled content</see> were updated</param>
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
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        (Title, UpdatedAt) = (title, updatedAt);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to add</param>
    public async Task AddReactionAsync(uint emoteId) =>
        await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);

    /// <inheritdoc cref="BaseGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public async Task RemoveReactionAsync(uint emoteId) =>
        await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
    #endregion
}