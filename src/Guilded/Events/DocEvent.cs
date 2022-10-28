using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes <see cref="Content.Doc">a document</see>.
/// </summary>
/// <seealso cref="Content.Doc" />
/// <seealso cref="TopicEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class DocEvent : ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets <see cref="Content.Doc">the document</see> received from the event.
    /// </summary>
    /// <value><see cref="Content.Doc" /></value>
    /// <seealso cref="DocEvent" />
    /// <seealso cref="Title" />
    /// <seealso cref="ServerId" />
    public Doc Doc { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Doc.ChannelId;

    /// <inheritdoc cref="TitledContent.Title" />
    public string Title => Doc.Title;

    /// <inheritdoc cref="Doc.Content" />
    public string Content => Doc.Content;

    /// <inheritdoc cref="Doc.Mentions" />
    public Mentions? Mentions => Doc.Mentions;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Doc.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Doc.CreatedAt;

    /// <inheritdoc cref="Doc.UpdatedBy" />
    public HashId? UpdatedBy => Doc.UpdatedBy;

    /// <inheritdoc cref="TitledContent.UpdatedAt" />
    public DateTime? UpdatedAt => Doc.UpdatedAt;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Doc.Updated" />
    public IObservable<DocEvent> Updated =>
        Doc.Updated;

    /// <inheritdoc cref="Doc.Deleted" />
    public IObservable<DocEvent> Deleted =>
        Doc.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the doc event occurred</param>
    /// <param name="doc"><see cref="Content.Doc">The doc</see> received from the event</param>
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

    #region Methods
    /// <inheritdoc cref="Doc.UpdateAsync(string, string)" />
    public async Task<Doc> UpdateAsync(string title, string content) =>
        await Doc.UpdateAsync(title, content).ConfigureAwait(false);

    /// <inheritdoc cref="Doc.DeleteAsync" />
    public async Task DeleteAsync() =>
        await Doc.DeleteAsync().ConfigureAwait(false);

    /// <inheritdoc cref="TitledContent.AddReactionAsync(uint)" />
    public async Task AddReactionAsync(uint emoteId) =>
        await Doc.AddReactionAsync(emoteId).ConfigureAwait(false);

    /// <inheritdoc cref="TitledContent.RemoveReactionAsync(uint)" />
    public async Task RemoveReactionAsync(uint emoteId) =>
        await Doc.RemoveReactionAsync(emoteId).ConfigureAwait(false);
    #endregion
}