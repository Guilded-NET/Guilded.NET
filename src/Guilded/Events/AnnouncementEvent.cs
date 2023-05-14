using System;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Servers;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when someone creates, updates or deletes a <see cref="Content.Announcement">announcement</see>.
/// </summary>
/// <seealso cref="Content.Announcement" />
/// <seealso cref="TopicEvent" />
/// <seealso cref="MessageEvent" />
/// <seealso cref="ItemEvent" />
/// <seealso cref="CalendarEventEvent" />
/// <seealso cref="ChannelEvent" />
public class AnnouncementEvent : IHasParentClient, ICreatableContent, IReactibleContent, IServerBased, IChannelBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Content.Announcement">announcement</see> received from the event.
    /// </summary>
    /// <value>The <see cref="Content.Announcement">announcement</see> received from the event</value>
    /// <seealso cref="AnnouncementEvent" />
    /// <seealso cref="Title" />
    /// <seealso cref="ServerId" />
    public Announcement Announcement { get; }

    /// <inheritdoc />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="ChannelContent{T, S}.Id" />
    public HashId Id => Announcement.Id;

    /// <inheritdoc cref="ChannelContent{T, S}.ChannelId" />
    public Guid ChannelId => Announcement.ChannelId;

    /// <inheritdoc cref="TitledContent{T}.Title" />
    public string Title => Announcement.Title;

    /// <inheritdoc cref="Announcement.Content" />
    public string Content => Announcement.Content;

    /// <inheritdoc cref="Announcement.Mentions" />
    public Mentions? Mentions => Announcement.Mentions;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedBy" />
    public HashId CreatedBy => Announcement.CreatedBy;

    /// <inheritdoc cref="ChannelContent{T, S}.CreatedAt" />
    public DateTime CreatedAt => Announcement.CreatedAt;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => Announcement.ParentClient;
    #endregion

    #region Properties Events
    /// <inheritdoc cref="Announcement.Updated" />
    public IObservable<AnnouncementEvent> Updated =>
        Announcement.Updated;

    /// <inheritdoc cref="Announcement.Deleted" />
    public IObservable<AnnouncementEvent> Deleted =>
        Announcement.Deleted;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="AnnouncementEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the announcement event occurred</param>
    /// <param name="announcement">The <see cref="Content.Announcement">announcement</see> received from the event</param>
    /// <returns>New <see cref="AnnouncementEvent" /> JSON instance</returns>
    /// <seealso cref="AnnouncementEvent" />
    [JsonConstructor]
    public AnnouncementEvent(
        [JsonProperty(Required = Required.Always)]
        Announcement announcement,

        [JsonProperty(Required = Required.Always)]
        HashId serverId
    ) =>
        (ServerId, Announcement) = (serverId, announcement);
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.UpdateAnnouncementAsync(Guid, HashId, string, string)" />
    /// <param name="title">The new title of the <see cref="Content.Announcement">announcement</see></param>
    /// <param name="content">The Markdown content of the <see cref="Content.Announcement">announcement</see></param>
    public Task<Announcement> UpdateAsync(string title, string content) =>
        Announcement.UpdateAsync(title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteAnnouncementAsync(Guid, HashId)" />
    public Task DeleteAsync() =>
        Announcement.DeleteAsync();

    /// <inheritdoc cref="AbstractGuildedClient.AddAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(uint emote) =>
        Announcement.AddReactionAsync(emote);

    /// <inheritdoc cref="AbstractGuildedClient.RemoveAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(uint emote) =>
        Announcement.RemoveReactionAsync(emote);
    #endregion

    #region Methods Comments
    /// <inheritdoc cref="AbstractGuildedClient.CreateAnnouncementCommentAsync(Guid, HashId, string)" />
    /// <param name="content">The content of the <see cref="AnnouncementComment">announcement comment</see></param>
    public Task<AnnouncementComment> CreateCommentAsync(string content) =>
        Announcement.CreateCommentAsync(content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateAnnouncementCommentAsync(Guid, HashId, uint, string)" />
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="AnnouncementComment">announcement comment</see></param>
    public Task<AnnouncementComment> UpdateCommentAsync(uint announcementComment, string content) =>
        Announcement.UpdateCommentAsync(announcementComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteAnnouncementCommentAsync(Guid, HashId, uint)" />
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to delete</param>
    public Task DeleteCommentAsync(uint announcementComment) =>
        Announcement.DeleteCommentAsync(announcementComment);
    #endregion
}