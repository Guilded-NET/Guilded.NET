using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.Doc">document</see>.
/// </summary>
/// <seealso cref="Content.Doc" />
/// <seealso cref="TopicEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class DocEvent : IHasParentClient, ICreatableContent, IUpdatableContent, IReactibleContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Doc">document</see> received from the event.
    /// </summary>
    /// <value>The <see cref="Content.Doc">document</see> received from the event</value>
    /// <seealso cref="DocEvent" />
    /// <seealso cref="Title" />
    /// <seealso cref="ServerId" />
    public Doc Doc { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public uint Id => Doc.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Doc.ChannelId;

    /// <inheritdoc cref="TitledContent{T}.Title" />
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

    /// <inheritdoc cref="Doc.UpdatedAt" />
    public DateTime? UpdatedAt => Doc.UpdatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Doc.ParentClient;
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
    /// <param name="doc">The <see cref="Content.Doc">document</see> received from the event</param>
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
    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Content.Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Content.Doc">document</see></param>
    public Task<Doc> UpdateAsync(string title, string content) =>
        Doc.UpdateAsync(title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        Doc.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.AddDocReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emote) =>
        Doc.AddReactionAsync(emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveDocReactionAsync(Guid, uint, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emote) =>
        Doc.RemoveReactionAsync(emote);
    #endregion

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateDocCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="DocComment">document comment</see></param>
    public Task<DocComment> CreateCommentAsync(string content) =>
        Doc.CreateCommentAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocCommentAsync(Guid, uint, uint, string)" />
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="DocComment">document comment</see></param>
    public Task<DocComment> UpdateCommentAsync(uint docComment, string content) =>
        Doc.UpdateCommentAsync(docComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocCommentAsync(Guid, uint, uint)" />
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to delete</param>
    public Task DeleteCommentAsync(uint docComment) =>
        Doc.DeleteCommentAsync(docComment);
    #endregion
}