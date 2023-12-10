using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Events;
using Guilded.Permissions;
using Guilded.Servers;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Announcements channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Announcement">announcement</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    public IObservable<AnnouncementEvent> AnnouncementCreated => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Announcement">announcement</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementDeleted" />
    public IObservable<AnnouncementEvent> AnnouncementUpdated => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Announcement">announcement</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    public IObservable<AnnouncementEvent> AnnouncementDeleted => ((IEventInfo<AnnouncementEvent>)GuildedEvents["AnnouncementDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementReaction">reaction</see> is added to a <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    /// <seealso cref="AnnouncementReactionRemoved" />
    public IObservable<AnnouncementReactionEvent> AnnouncementReactionAdded => ((IEventInfo<AnnouncementReactionEvent>)GuildedEvents["AnnouncementReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementReaction">reaction</see> is added to a <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCreated" />
    /// <seealso cref="AnnouncementUpdated" />
    /// <seealso cref="AnnouncementDeleted" />
    /// <seealso cref="AnnouncementReactionAdded" />
    public IObservable<AnnouncementReactionEvent> AnnouncementReactionRemoved => ((IEventInfo<AnnouncementReactionEvent>)GuildedEvents["AnnouncementReactionDeleted"]).Observable;
    #endregion

    #region Properties Announcements channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="AnnouncementComment">announcement comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentCreated => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementComment">announcement comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentUpdated => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementComment">announcement comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentEvent> AnnouncementCommentDeleted => ((IEventInfo<AnnouncementCommentEvent>)GuildedEvents["AnnouncementCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementCommentReaction">reaction</see> is added to a <see cref="AnnouncementComment">announcement comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionRemoved" />
    public IObservable<AnnouncementCommentReactionEvent> AnnouncementCommentReactionAdded => ((IEventInfo<AnnouncementCommentReactionEvent>)GuildedEvents["AnnouncementCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="AnnouncementCommentReaction">reaction</see> is added to a <see cref="AnnouncementComment">announcement comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>AnnouncementCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="AnnouncementCommentCreated" />
    /// <seealso cref="AnnouncementCommentUpdated" />
    /// <seealso cref="AnnouncementCommentDeleted" />
    /// <seealso cref="AnnouncementCommentReactionAdded" />
    public IObservable<AnnouncementCommentReactionEvent> AnnouncementCommentReactionRemoved => ((IEventInfo<AnnouncementCommentReactionEvent>)GuildedEvents["AnnouncementCommentReactionDeleted"]).Observable;
    #endregion

    #region Methods Announcement channels
    /// <summary>
    /// Gets a list of <see cref="Announcement">announcements</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="AnnouncementChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Announcement">announcements</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of the fetched <see cref="Announcement">announcements</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The list of fetched <see cref="Announcement">announcements</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Announcement>> GetAnnouncementsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<Announcement>>(
            new RestRequest($"channels/{channel}/announcements", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs");

    /// <summary>
    /// Gets the specified <paramref name="announcement" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The <see cref="Announcement">announcement</see> that was specified in the arguments</returns>
    public Task<Announcement> GetAnnouncementAsync(Guid channel, HashId announcement) =>
        GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Get), "doc");

    /// <summary>
    /// Creates a new <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Announcement">announcement</see></param>
    /// <param name="content">The Markdown content of the <see cref="Announcement">announcement</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.CreateAnnouncements" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Announcement">announcement</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Announcement">announcement</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Announcement> CreateAnnouncementAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "announcement");

    /// <summary>
    /// Edits the text <paramref name="content" /> or the <paramref name="title" /> of the specified <paramref name="announcement" />.
    /// </summary>
    /// <remarks>
    /// <para>The updated <paramref name="announcement" /> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="AnnouncementChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Announcement">announcement</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Announcement">announcement</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.ManageAnnouncements">Required when editing <see cref="Announcement">announcements</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Doc">document</see></permission>
    /// <returns>The <see cref="Announcement">announcement</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Announcement> UpdateAnnouncementAsync(Guid channel, HashId announcement, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Announcement>(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Patch)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "announcement");

    /// <summary>
    /// Deletes the specified <paramref name="announcement" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.ManageAnnouncements">Required when deleting <see cref="Announcement">announcements</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteAnnouncementAsync(Guid channel, HashId announcement) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}", Method.Delete));
    #endregion
}