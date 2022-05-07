using System;
using System.Threading.Tasks;
using Guilded.Base.Content;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>DocCreated</c>, <c>DocUpdated</c> or <c>DocDeleted</c> and opcode <c>0</c> that occurs once someone posts, edits or deletes a <see cref="DocEvent.Doc">doc</see> in <see cref="ChannelId">a channel</see>.
/// </summary>
/// <seealso cref="Content.Doc" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ListItemEvent" />
/// <seealso cref="ChannelEvent" />
public class DocEvent : BaseObject, IServerEvent
{
    #region JSON properties
    /// <summary>
    /// Gets the document received from the event.
    /// </summary>
    /// <value>Doc</value>
    /// <seealso cref="DocEvent" />
    /// <seealso cref="Title" />
    /// <seealso cref="ServerId" />
    public Doc Doc { get; }
    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Doc.ChannelId;
    /// <inheritdoc cref="TitledContent.Title" />
    public string Title => Doc.Title;
    /// <inheritdoc cref="TitledContent.Content" />
    public string Content => Doc.Content;
    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Doc.CreatedBy;
    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Doc.CreatedAt;
    /// <inheritdoc cref="Doc.UpdatedBy" />
    public HashId? UpdatedBy => Doc.UpdatedBy;
    /// <inheritdoc cref="TitledContent.UpdatedAt" />
    public DateTime? UpdatedAt => Doc.UpdatedAt;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the server where the doc event occurred</param>
    /// <param name="doc">The doc received from the event</param>
    /// <returns>New <see cref="DocEvent" /> JSON instance</returns>
    /// <seealso cref="DocEvent" />
    [JsonConstructor]
    public DocEvent(
        [JsonProperty(Required = Required.Always)]
        Doc doc,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Doc) = (serverId, doc);
    #endregion

    #region Additional
    /// <inheritdoc cref="Doc.UpdateAsync(string, string)" />
    public async Task<Doc> UpdateAsync(string title, string content) =>
        await Doc.UpdateAsync(title, content).ConfigureAwait(false);
    /// <inheritdoc cref="Doc.DeleteAsync" />
    public async Task DeleteAsync() =>
        await Doc.DeleteAsync().ConfigureAwait(false);
    /// <inheritdoc cref="TitledContent.AddReactionAsync(uint)" />
    public async Task<Reaction> AddReactionAsync(uint emoteId) =>
        await Doc.AddReactionAsync(emoteId).ConfigureAwait(false);
    // /// <inheritdoc cref="Message.RemoveReactionAsync(uint)" />
    // public async Task RemoveReactionAsync(uint emoteId) =>
    //     await Message.RemoveReactionAsync(emoteId).ConfigureAwait(false);
    #endregion
}