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
public class Doc : TitledContent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the member that updated the document.
    /// </summary>
    /// <remarks>
    /// <para>Only includes the user who updated this document most recently.</para>
    /// </remarks>
    /// <value>User ID?</value>
    public HashId? UpdatedBy { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Doc"/> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the document</param>
    /// <param name="channelId">The identifier of the channel where the document is</param>
    /// <param name="serverId">The identifier of the server where the document is</param>
    /// <param name="title">The title of the document</param>
    /// <param name="content">The text contents of the document</param>
    /// <param name="createdBy">The identifier of the user that created the document</param>
    /// <param name="createdAt">The date of when the document was created</param>
    /// <param name="updatedBy">The identifier of the user who recently updated the document</param>
    /// <param name="updatedAt">The date of when the document was recently updated</param>
    [JsonConstructor]
    public Doc(
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
        HashId? updatedBy,

        [JsonProperty]
        DateTime? updatedAt
    ) : base(id, channelId, serverId, title, content, createdBy, createdAt, updatedAt) =>
        UpdatedBy = updatedBy;
    #endregion

    #region Additional
    /// <inheritdoc cref="BaseGuildedClient.UpdateDocAsync(Guid, uint, string, string)"/>
    /// <param name="title">The new title of the document</param>
    /// <param name="content">The Markdown content of the document</param>
    public async Task<Doc> UpdateAsync(string title, string content) =>
        await ParentClient.UpdateDocAsync(ChannelId, Id, title, content);
    /// <inheritdoc cref="BaseGuildedClient.DeleteDocAsync(Guid, uint)"/>
    public async Task DeleteAsync() =>
        await ParentClient.DeleteDocAsync(ChannelId, Id);
    #endregion
}