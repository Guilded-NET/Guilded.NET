using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a document in a document channel.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
/// <seealso cref="ItemBase{T}" />
/// <seealso cref="Message" />
public abstract class TitledContent<T> : ChannelContent<T, HashId>, IReactibleContent, IServerBased where T : notnull
{
    #region Properties Content
    /// <summary>
    /// Gets the title of the <see cref="TitledContent{T}">titled content</see>.
    /// </summary>
    /// <remarks>
    /// <para>This does not have any Markdown formatting and will not contain <c>\n</c> or other line breaking characters.</para>
    /// </remarks>
    /// <value>Single-line string</value>
    /// <seealso cref="TitledContent{T}" />
    /// <seealso cref="Content" />
    public string Title { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TitledContent{T}" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="TitledContent{T}">titled content</see></param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel content are</param>
    /// <param name="title">The title of the <see cref="TitledContent{T}">titled content</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="ChannelContent{TId, TServer}">content</see></param>
    /// <param name="createdAt">The date when the <see cref="ChannelContent{TId, TServer}">content</see> were created</param>
    /// <returns>New <see cref="TitledContent{T}" /> JSON instance</returns>
    /// <seealso cref="TitledContent{T}" />
    [JsonConstructor]
    public TitledContent(
        [JsonProperty(Required = Required.Always)]
        T id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string title,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt
    ) : base(id, channelId, serverId, createdBy, createdAt) =>
        Title = title;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.AddReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to add</param>
    public abstract Task AddReactionAsync(uint emoteId);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveReactionAsync(Guid, uint, uint)" />
    /// <param name="emoteId">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public abstract Task RemoveReactionAsync(uint emoteId);
    #endregion
}