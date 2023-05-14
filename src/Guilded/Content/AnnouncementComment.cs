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
/// Represents a comment on a <see cref="Announcement">announcement</see>.
/// </summary>
/// <seealso cref="Announcement" />
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="ChannelContent{TId, TServer}" />
public class AnnouncementComment : BaseComment
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">announcement comment</see> was created.
    /// </summary>
    /// <value>The identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">announcement comment</see> was created</value>
    /// <seealso cref="AnnouncementComment" />
    /// <seealso cref="BaseComment.Id" />
    /// <seealso cref="BaseComment.ChannelId" />
    /// <seealso cref="BaseComment.CreatedBy" />
    public HashId AnnouncementId { get; }
    #endregion

    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="AnnouncementComment">announcement comment</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="AnnouncementComment">announcement comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="AnnouncementComment">announcement comment</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<AnnouncementCommentEvent> Updated =>
        ParentClient
            .AnnouncementCommentUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.AnnouncementId == AnnouncementId && x.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="AnnouncementComment">announcement comment</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="AnnouncementComment">announcement comment</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="AnnouncementComment">announcement comment</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<AnnouncementCommentEvent> Deleted =>
        ParentClient
            .AnnouncementCommentDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.AnnouncementId == AnnouncementId && x.Id == Id
            )
            .Take(1);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementComment" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="AnnouncementComment">announcement comment</see></param>
    /// <param name="announcementId">The identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">announcement comment</see> was created</param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="AnnouncementComment">announcement comment</see> was created</param>
    /// <param name="content">The full-Markdown text contents of the <see cref="AnnouncementComment">announcement comment</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="AnnouncementComment">announcement comment</see></param>
    /// <param name="createdAt">The date when the <see cref="AnnouncementComment">announcement comment</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="AnnouncementComment">announcement comment</see> was edited</param>
    /// <returns>New <see cref="AnnouncementComment" /> JSON instance</returns>
    /// <seealso cref="AnnouncementComment" />
    [JsonConstructor]
    public AnnouncementComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        HashId announcementId,

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
        AnnouncementId = announcementId;
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.CreateAnnouncementCommentAsync(Guid, HashId, string)" />
    /// <param name="content">The content of the <see cref="AnnouncementComment">announcement comment</see></param>
    public Task<AnnouncementComment> ReplyAsync(string content) =>
        ParentClient.CreateAnnouncementCommentAsync(ChannelId, AnnouncementId, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateAnnouncementCommentAsync(Guid, HashId, uint, string)" />
    /// <param name="content">The new Markdown content of the <see cref="AnnouncementComment">announcement comment</see></param>
    public Task<AnnouncementComment> UpdateAsync(string content) =>
        ParentClient.UpdateAnnouncementCommentAsync(ChannelId, AnnouncementId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteAnnouncementCommentAsync(Guid, HashId, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteAnnouncementCommentAsync(ChannelId, AnnouncementId, Id);
    #endregion
}