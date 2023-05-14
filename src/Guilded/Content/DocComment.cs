using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a comment on a <see cref="Doc">document</see>.
/// </summary>
/// <seealso cref="CalendarEvent" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="ChannelContent{TId, TServer}" />
public class DocComment : BaseComment
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> was created.
    /// </summary>
    /// <value>The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> was created</value>
    /// <seealso cref="DocComment" />
    /// <seealso cref="BaseComment.Id" />
    /// <seealso cref="BaseComment.ChannelId" />
    /// <seealso cref="BaseComment.CreatedBy" />
    public uint DocId { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="DocComment">document comment</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="DocComment">document comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="DocComment">document comment</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<DocCommentEvent> Updated =>
        ParentClient
            .DocCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.DocId == DocId && x.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="DocComment">document comment</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="DocComment">document comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="DocComment">document comment</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<DocCommentEvent> Deleted =>
        ParentClient
            .DocCommentDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.DocId == DocId && x.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocComment" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="DocComment"></see></param>
    /// <param name="docId">The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> was created</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="DocComment">document comment</see> was created</param>
    /// <param name="content">The full-Markdown text contents of the <see cref="DocComment">document comment</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="DocComment">document comment</see></param>
    /// <param name="createdAt">The date when the <see cref="DocComment">document comment</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="DocComment">document comment</see> was edited</param>
    /// <returns>New <see cref="DocComment" /> JSON instance</returns>
    /// <seealso cref="DocComment" />
    [JsonConstructor]
    public DocComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        uint docId,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, content, createdBy, createdAt, updatedAt) =>
        DocId = docId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.CreateDocCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="DocComment">document comment</see></param>
    public Task<DocComment> ReplyAsync(string content) =>
        ParentClient.CreateDocCommentAsync(ChannelId, DocId, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocCommentAsync(Guid, uint, uint, string)" />
    /// <param name="content">The new Markdown content of the <see cref="DocComment">document comment</see></param>
    public Task<DocComment> UpdateAsync(string content) =>
        ParentClient.UpdateDocCommentAsync(ChannelId, DocId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocCommentAsync(Guid, uint, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteDocCommentAsync(ChannelId, DocId, Id);
    #endregion
}