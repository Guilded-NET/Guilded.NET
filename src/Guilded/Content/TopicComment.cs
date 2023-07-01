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
/// Represents a reply in a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="TitledContent{TId}" />
public class TopicComment : BaseComment, IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic reply</see> was created.
    /// </summary>
    /// <value>The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic reply</see> was created</value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="BaseComment.Id" />
    /// <seealso cref="BaseComment.ChannelId" />
    /// <seealso cref="BaseComment.CreatedBy" />
    public uint TopicId { get; }

    /// <summary>
    /// Gets the <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see>.
    /// </summary>
    /// <value>The <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="Content" />
    /// <seealso cref="BaseComment.CreatedBy" />
    /// <seealso cref="BaseComment.CreatedAt" />
    /// <seealso cref="BaseComment.Id" />
    /// <seealso cref="TopicId" />
    public Mentions? Mentions { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic reply</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="TopicComment">forum topic reply</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic reply</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<TopicCommentEvent> Updated =>
        ParentClient
            .TopicCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.TopicId == TopicId && x.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="TopicComment">forum topic comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="TopicComment">forum topic comment</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<TopicCommentEvent> Deleted =>
        ParentClient
            .TopicCommentDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.TopicId == TopicId && x.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicComment" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="TopicComment">forum topic reply</see></param>
    /// <param name="forumTopicId">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic reply</see> was created</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="TopicComment">forum topic reply</see> was created</param>
    /// <param name="content">The full-Markdown text contents of the <see cref="TopicComment">forum topic reply</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="TopicComment">forum topic reply</see></param>
    /// <param name="createdAt">The date when the <see cref="TopicComment">forum topic reply</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="TopicComment">forum topic reply</see> was edited</param>
    /// <param name="mentions">The <see cref="Mentions">mentions</see> found in the <see cref="BaseComment.Content">content</see></param>
    /// <returns>New <see cref="TopicComment" /> JSON instance</returns>
    /// <seealso cref="TopicComment" />
    [JsonConstructor]
    public TopicComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        uint forumTopicId,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null
    ) : base(id, channelId, content, createdBy, createdAt, updatedAt) =>
        (TopicId, Mentions) = (forumTopicId, mentions);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="content">The content of the <see cref="TopicComment">forum topic reply</see></param>
    public Task<TopicComment> ReplyAsync(string content) =>
        ParentClient.CreateTopicCommentAsync(ChannelId, TopicId, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicCommentAsync(Guid, uint, uint, string)" />
    /// <param name="content">The new Markdown content of the <see cref="TopicComment">forum topic reply</see></param>
    public Task<TopicComment> UpdateAsync(string content) =>
        ParentClient.UpdateTopicCommentAsync(ChannelId, TopicId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicCommentAsync(Guid, uint, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteTopicCommentAsync(ChannelId, TopicId, Id);
    #endregion
}