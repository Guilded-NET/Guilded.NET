using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Guilded.Base;
using Guilded.Client;
using Guilded.Events;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents an announcement in a <see cref="AnnouncementChannel">announcement channel</see>.
/// </summary>
/// <seealso cref="Doc" />
/// <seealso cref="CalendarEvent" />
/// <seealso cref="Topic" />
/// <seealso cref="Message" />
/// <seealso cref="Item" />
public class Announcement : TitledContent<HashId>
{
    #region Properties Events
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Announcement">announcement</see> gets edited.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Announcement">announcement</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Announcement">announcement</see> gets edited</returns>
    /// <seealso cref="Deleted" />
    public IObservable<AnnouncementEvent> Updated =>
        ParentClient
            .AnnouncementUpdated
            .Where(x =>
                x.ChannelId == ChannelId && x.Announcement.Id == Id
            );

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="Announcement">announcement</see> gets deleted.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="IObservable{T}">observable</see> will be filtered for this <see cref="Announcement">announcement</see> specific.</para>
    /// </remarks>
    /// <returns>The <see cref="IObservable{T}">observable</see> for an event when the <see cref="Announcement">announcement</see> gets deleted</returns>
    /// <seealso cref="Updated" />
    public IObservable<AnnouncementEvent> Deleted =>
        ParentClient
            .AnnouncementDeleted
            .Where(x =>
                x.ChannelId == ChannelId && x.Announcement.Id == Id
            )
            .Take(1);
    #endregion

    #region Properties
    /// <summary>
    /// Gets the text contents of the <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>The text contents of the <see cref="Announcement">announcement</see></value>
    /// <seealso cref="Announcement" />
    /// <seealso cref="TitledContent{T}.Title" />
    public string Content { get; }

    /// <summary>
    /// Gets the <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see>.
    /// </summary>
    /// <value>The <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see></value>
    public Mentions? Mentions { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Announcement" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Announcement">announcement</see></param>
    /// <param name="channelId">The identifier of the channel where the channel content are</param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the channel content are</param>
    /// <param name="title">The title of the <see cref="Announcement">announcement</see></param>
    /// <param name="content">The text contents of the <see cref="Announcement">announcement</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="Announcement">announcement</see></param>
    /// <param name="createdAt">The date when the <see cref="Announcement">announcement</see> was created</param>
    /// <param name="mentions">The <see cref="Mentions">mentions</see> found in the <see cref="Content">content</see></param>
    /// <returns>New <see cref="Announcement" /> JSON instance</returns>
    /// <seealso cref="Announcement" />
    [JsonConstructor]
    public Announcement(
        [JsonProperty(Required = Required.Always)]
        HashId id,

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
        Mentions? mentions = null
    ) : base(id, channelId, serverId, title, createdBy, createdAt) =>
        (Content, Mentions) = (content, mentions);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Announcement">announcement</see></param>
    /// <param name="content">The Markdown content of the <see cref="Announcement">announcement</see></param>
    public Task<Announcement> UpdateAsync(string title, string content) =>
        ParentClient.UpdateAnnouncementAsync(ChannelId, Id, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteAnnouncementAsync(ChannelId, Id);

    /// <inheritdoc cref="AbstractGuildedClient.AddAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public override Task AddReactionAsync(uint emote) =>
        ParentClient.AddAnnouncementReactionAsync(ChannelId, Id, emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public override Task RemoveReactionAsync(uint emote) =>
        ParentClient.RemoveAnnouncementReactionAsync(ChannelId, Id, emote);
    #endregion

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateAnnouncementCommentAsync(Guid, HashId, string)" />
    /// <param name="content">The content of the <see cref="DocComment">document comment</see></param>
    public Task<AnnouncementComment> CreateCommentAsync(string content) =>
        ParentClient.CreateAnnouncementCommentAsync(ChannelId, Id, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateAnnouncementCommentAsync(Guid, HashId, uint, string)" />
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="AnnouncementComment">announcement comment</see></param>
    public Task<AnnouncementComment> UpdateCommentAsync(uint announcementComment, string content) =>
        ParentClient.UpdateAnnouncementCommentAsync(ChannelId, Id, announcementComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteAnnouncementCommentAsync(Guid, HashId, uint)" />
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to delete</param>
    public Task DeleteCommentAsync(uint announcementComment) =>
        ParentClient.DeleteAnnouncementCommentAsync(ChannelId, Id, announcementComment);
    #endregion
}