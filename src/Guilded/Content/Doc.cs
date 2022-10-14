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
/// Represents a document in <see cref="Servers.ChannelType.Docs">a document channel</see>.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="ListItem" />
/// <seealso cref="Message" />
public class Doc : TitledContent, IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the <see cref="Doc">document</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    /// <seealso cref="Doc" />
    /// <seealso cref="TitledContent.Title" />
    public string Content { get; }

    /// <summary>
    /// Gets <see cref="Content.Mentions">the mentions</see> found in <see cref="Content">the content</see>.
    /// </summary>
    /// <value><see cref="Content.Mentions" />?</value>
    public Mentions? Mentions { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Member">the member</see> who updated <see cref="Doc">the document</see>.
    /// </summary>
    /// <remarks>
    /// <para>Only includes the <see cref="User">user</see> who updated <see cref="Doc">the document</see> most recently.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see>?</value>
    /// <seealso cref="Doc" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="IWebhookCreatable.CreatedByWebhook" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    public HashId? UpdatedBy { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Doc">document</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Doc">document</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Doc">document</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<DocEvent> Updated =>
        ParentClient
            .DocUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Doc.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Doc">document</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Doc">document</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Doc">document</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<DocEvent> Deleted =>
        ParentClient
            .DocDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Doc.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Doc" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the document</param>
    /// <param name="channelId">The identifier of the channel where the document is</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the document is</param>
    /// <param name="title">The title of the document</param>
    /// <param name="content">The text contents of the document</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the document</param>
    /// <param name="createdAt">the date when the document was created</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> who recently updated the document</param>
    /// <param name="updatedAt">the date when the document was recently updated</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Content">the content</see></param>
    /// <returns>New <see cref="Doc" /> JSON instance</returns>
    /// <seealso cref="Doc" />
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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null
    ) : base(id, channelId, serverId, title, createdBy, createdAt, updatedAt) =>
        (Content, Mentions, UpdatedBy) = (content, mentions, updatedBy);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Doc">document</see></param>
    public Task<Doc> UpdateAsync(string title, string content) =>
        ParentClient.UpdateDocAsync(ChannelId, Id, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteDocAsync(ChannelId, Id);
    #endregion
}