using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Methods Reactions > Messages
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.AddVoice" />
    /// <permission cref="Permission.GetStreams" />
    public Task AddMessageReactionAsync(Guid channel, Guid message, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddMessageReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddMessageReactionAsync(channel, message, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the specified <paramref name="message" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetMessages" />
    /// <permission cref="Permission.AddVoice" />
    /// <permission cref="Permission.GetStreams" />
    public Task RemoveMessageReactionAsync(Guid channel, Guid message, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/messages/{message}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveMessageReactionAsync(Guid channel, Guid message, Emote emote) =>
        RemoveMessageReactionAsync(channel, message, emote.Id);
    #endregion

    #region Methods Forum channels > Topic replies
    /// <summary>
    /// Gets a list of <see cref="TopicComment">forum topic comments</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get <see cref="TopicComment">forum topic replies</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The list of fetched <see cref="TopicComment">forum topic replies</see> in the specified <paramref name="topic" /></returns>
    public Task<IList<TopicComment>> GetTopicCommentsAsync(Guid channel, uint topic) =>
        GetResponsePropertyAsync<IList<TopicComment>>(new RestRequest($"channels/{channel}/topics/{topic}/comments", Method.Get), "forumTopicComments");

    /// <summary>
    /// Gets the <paramref name="topicComment">specified forum topic reply</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The <see cref="TopicComment">forum topic reply</see> that was specified in the arguments</returns>
    public Task<TopicComment> GetTopicCommentAsync(Guid channel, uint topic, uint topicComment) =>
        GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Get), "forumTopicComment");

    /// <summary>
    /// Creates a new <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> should be</param>
    /// <param name="content">The content of the <see cref="TopicComment">forum topic comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopicComments" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="TopicComment">forum topic comment</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<TopicComment> CreateTopicCommentAsync(Guid channel, uint topic, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments", Method.Post).AddJsonBody(new { content }), "forumTopicComment");

    /// <summary>
    /// Edits <see cref="TopicComment">forum topic comment's</see> <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to update</param>
    /// <param name="content">The new contents of the <see cref="TopicComment">forum topic comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopicComments" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="topicComment">forum topic calendar</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<TopicComment> UpdateTopicCommentAsync(Guid channel, uint topic, uint topicComment, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<TopicComment>(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Patch).AddJsonBody(new { content }), "forumTopicComment");

    /// <summary>
    /// Deletes a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics">Required when deleting <see cref="TopicComment">forum topic comment</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteTopicCommentAsync(Guid channel, uint topic, uint topicComment) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}", Method.Delete));
    #endregion

    #region Methods Reactions > Topics
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the specified <paramref name="topic">forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    public Task AddTopicReactionAsync(Guid channel, uint topic, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddTopicReactionAsync(Guid channel, uint topic, Emote emote) =>
        AddTopicReactionAsync(channel, topic, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the specified <paramref name="topic">forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    public Task RemoveTopicReactionAsync(Guid channel, uint topic, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveTopicReactionAsync(Guid channel, uint topic, Emote emote) =>
        RemoveTopicReactionAsync(channel, topic, emote.Id);
    #endregion

    #region Methods Reactions > Topic Comments
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="topicComment">specified forum topic comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <paramref name="topicComment">forum topic comment</paramref> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    public Task AddTopicCommentReactionAsync(Guid channel, uint topic, uint topicComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddTopicCommentReactionAsync(Guid, uint, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <paramref name="topicComment">forum topic comment</paramref> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddTopicCommentReactionAsync(Guid channel, uint topic, uint topicComment, Emote emote) =>
        AddTopicCommentReactionAsync(channel, topic, topicComment, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the specified <paramref name="topicComment">forum topic comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <paramref name="topicComment">forum topic comment</paramref> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    public Task RemoveTopicCommentReactionAsync(Guid channel, uint topic, uint topicComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/comments/{topicComment}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicCommentReactionAsync(Guid, uint, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <paramref name="topicComment">forum topic comment</paramref> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveTopicCommentReactionAsync(Guid channel, uint topic, uint topicComment, Emote emote) =>
        RemoveTopicCommentReactionAsync(channel, topic, topicComment, emote.Id);
    #endregion

    #region Methods Calendar channels > Rsvp 
    /// <summary>
    /// Gets a list of <see cref="CalendarEvent">calendar events</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarEventRsvp">RSVPs</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The list of fetched <see cref="CalendarEventRsvp">calendar event RSVPs</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<CalendarEventRsvp>> GetEventRsvpsAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<IList<CalendarEventRsvp>>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps", Method.Get), "calendarEventRsvps");

    /// <inheritdoc cref="GetEventRsvpsAsync(Guid, uint)" />
    [Obsolete($"Use `{nameof(GetEventRsvpsAsync)}` instead")]
    public Task<IList<CalendarEventRsvp>> GetRsvpsAsync(Guid channel, uint calendarEvent) =>
        GetEventRsvpsAsync(channel, calendarEvent);

    /// <summary>
    /// Gets the specified <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to get <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The <see cref="CalendarEventRsvp">calendar event RSVP</see> that was specified in the arguments</returns>
    public Task<CalendarEventRsvp> GetEventRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        GetResponsePropertyAsync<CalendarEventRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Get), "calendarEventRsvp");

    /// <inheritdoc cref="GetEventRsvpAsync(Guid, uint, HashId)" />
    [Obsolete($"Use `{nameof(GetEventRsvpAsync)}` instead")]
    public Task<CalendarEventRsvp> GetRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        GetEventRsvpAsync(channel, calendarEvent, user);

    /// <summary>
    /// Creates or edits the specified <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to set <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEventRsvps">Required when setting <see cref="CalendarEventRsvp">calendar event RSVPs</see> that aren't for the <see cref="AbstractGuildedClient">client</see></permission>
    /// <returns>Set <see cref="CalendarEventRsvp">calendar event RSVP</see></returns>
    public Task<CalendarEventRsvp> SetEventRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarEventRsvpStatus status) =>
        GetResponsePropertyAsync<CalendarEventRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Put)
            .AddJsonBody(new
            {
                status
            })
        , "calendarEventRsvp");

    /// <inheritdoc cref="SetEventRsvpAsync(Guid, uint, HashId, CalendarEventRsvpStatus)" />
    [Obsolete($"Use `{nameof(SetEventRsvpAsync)}` instead")]
    public Task<CalendarEventRsvp> SetRsvpAsync(Guid channel, uint calendarEvent, HashId user, CalendarEventRsvpStatus status) =>
        SetEventRsvpAsync(channel, calendarEvent, user, status);

    /// <summary>
    /// Deletes the specified <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">calendar event RSVP</see> is</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to remove <see cref="CalendarEventRsvp">RSVP</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEventRsvps">Required when removing <see cref="CalendarEventRsvp">calendar event RSVPs</see> that aren't for the <see cref="AbstractGuildedClient">client</see></permission>
    public Task RemoveEventRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps/{user}", Method.Delete));

    /// <inheritdoc cref="RemoveEventRsvpAsync(Guid, uint, HashId)" />
    [Obsolete($"Use `{nameof(RemoveEventRsvpAsync)}` instead")]
    public Task RemoveRsvpAsync(Guid channel, uint calendarEvent, HashId user) =>
        RemoveEventRsvpAsync(channel, calendarEvent, user);

    /// <summary>
    /// Creates or edits the specified <see cref="CalendarEventRsvp">calendar event RSVP</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventRsvp">RSVP</see> is</param>
    /// <param name="users">The list of identifiers of the <see cref="User">users</see> to set <see cref="CalendarEventRsvpStatus">RSVP status</see> of</param>
    /// <param name="status">The status of the <see cref="CalendarEvent">calendar RSVP</see> to set</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UpdateEventRsvps">Required when setting <see cref="CalendarEventRsvp">calendar event RSVPs</see> that aren't for the <see cref="AbstractGuildedClient">client</see></permission>
    /// <returns>Set <see cref="CalendarEventRsvp">calendar event RSVP</see></returns>
    public Task<CalendarEventRsvp> SetEventRsvpsAsync(Guid channel, uint calendarEvent, IList<HashId> users, CalendarEventRsvpStatus status) =>
        GetResponsePropertyAsync<CalendarEventRsvp>(new RestRequest($"channels/{channel}/events/{calendarEvent}/rsvps", Method.Put)
            .AddJsonBody(new
            {
                userIds = users,
                status
            })
        , "calendarEventRsvp");
    #endregion

    #region Methods Calendar channels > Comments
    /// <summary>
    /// Gets a list of <see cref="CalendarEventComment">calendar event comments</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarEventComment">comments</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The list of fetched <see cref="CalendarEventComment">calendar event comments</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<CalendarEventComment>> GetEventCommentsAsync(Guid channel, uint calendarEvent) =>
        GetResponsePropertyAsync<IList<CalendarEventComment>>(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments", Method.Get), "calendarEventComments");

    /// <summary>
    /// Gets the <paramref name="calendarEventComment">specified calendar event comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to get <see cref="CalendarEventComment">comment</see> of</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <returns>The <see cref="CalendarEventComment">calendar event comment</see> that was specified in the arguments</returns>
    public Task<CalendarEventComment> GetEventCommentAsync(Guid channel, uint calendarEvent, uint calendarEventComment) =>
        GetResponsePropertyAsync<CalendarEventComment>(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments/{calendarEventComment}", Method.Get), "calendarEventComment");

    /// <summary>
    /// Creates a new <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> should be</param>
    /// <param name="content">The content of the <see cref="CalendarEventComment">calendar event comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="CalendarEventComment">calendar event comment</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEventComment> CreateEventCommentAsync(Guid channel, uint calendarEvent, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<CalendarEventComment>(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments", Method.Post).AddJsonBody(new { content }), "calendarEventComment");

    /// <summary>
    /// Edits <see cref="CalendarEventComment">calendar event comment's</see> <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="CalendarEventComment">calendar event comment</see> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to update</param>
    /// <param name="content">The new contents of the <see cref="CalendarEventComment">calendar event comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="calendarEventComment">calendar event comment</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<CalendarEventComment> UpdateEventCommentAsync(Guid channel, uint calendarEvent, uint calendarEventComment, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<CalendarEventComment>(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments/{calendarEventComment}", Method.Patch).AddJsonBody(new { content }), "calendarEventComment");

    /// <summary>
    /// Deletes a <see cref="CalendarEventComment">calendar event comment</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    public Task DeleteEventCommentAsync(Guid channel, uint calendarEvent, uint calendarEventComment) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments/{calendarEventComment}", Method.Delete));
    #endregion

    #region Methods Reactions > Event
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="calendarEvent">specified calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    public Task AddEventReactionAsync(Guid channel, uint calendarEvent, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddEventReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddEventReactionAsync(Guid channel, uint calendarEvent, Emote emote) =>
        AddEventReactionAsync(channel, calendarEvent, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="calendarEvent">specified calendar event</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    public Task RemoveEventReactionAsync(Guid channel, uint calendarEvent, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveEventReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see>to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveEventReactionAsync(Guid channel, uint calendarEvent, Emote emote) =>
        RemoveEventReactionAsync(channel, calendarEvent, emote.Id);
    #endregion

    #region Methods Reactions > Event Comments
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="calendarEventComment">specified calendar event comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <paramref name="calendarEventComment" /> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">forum topic comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    public Task AddEventCommentReactionAsync(Guid channel, uint calendarEvent, uint calendarEventComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments/{calendarEventComment}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddEventCommentReactionAsync(Guid, uint, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <paramref name="calendarEventComment" /> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">forum topic comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddEventCommentReactionAsync(Guid channel, uint calendarEvent, uint calendarEventComment, Emote emote) =>
        AddEventCommentReactionAsync(channel, calendarEvent, calendarEventComment, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="calendarEventComment">specified calendar event comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <paramref name="calendarEventComment" /> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetEvents" />
    public Task RemoveEventCommentReactionAsync(Guid channel, uint calendarEvent, uint calendarEventComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/events/{calendarEvent}/comments/{calendarEventComment}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveEventCommentReactionAsync(Guid, uint, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="calendarEvent">The identifier of the <see cref="CalendarEvent">calendar event</see> where <paramref name="calendarEventComment" /> is</param>
    /// <param name="calendarEventComment">The identifier of the <see cref="CalendarEventComment">calendar event comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveEventCommentReactionAsync(Guid channel, uint calendarEvent, uint calendarEventComment, Emote emote) =>
        RemoveEventCommentReactionAsync(channel, calendarEvent, calendarEventComment, emote.Id);
    #endregion

    #region Methods Doc channels > Comments
    /// <summary>
    /// Gets a list of <see cref="DocComment">document comments</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to get <see cref="DocComment">comments</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The list of fetched <see cref="DocComment">document comments</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<DocComment>> GetDocCommentsAsync(Guid channel, uint doc) =>
        GetResponsePropertyAsync<IList<DocComment>>(new RestRequest($"channels/{channel}/docs/{doc}/comments", Method.Get), "docComments");

    /// <summary>
    /// Gets the <paramref name="docComment">specified calendar event comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to get <see cref="DocComment">comment</see> of</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The <see cref="DocComment">document comment</see> that was specified in the arguments</returns>
    public Task<DocComment> GetDocCommentAsync(Guid channel, uint doc, uint docComment) =>
        GetResponsePropertyAsync<DocComment>(new RestRequest($"channels/{channel}/docs/{doc}/comments/{docComment}", Method.Get), "docComment");

    /// <summary>
    /// Creates a new <see cref="DocComment">comment</see> on a <see cref="Doc">document</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> should be</param>
    /// <param name="content">The content of the <see cref="DocComment">document comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="DocComment">document comment</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<DocComment> CreateDocCommentAsync(Guid channel, uint doc, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<DocComment>(new RestRequest($"channels/{channel}/docs/{doc}/comments", Method.Post).AddJsonBody(new { content }), "docComment");

    /// <summary>
    /// Edits <see cref="DocComment">calendar event comment's</see> <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to update</param>
    /// <param name="content">The new contents of the <see cref="DocComment">document comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="docComment">document comment</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<DocComment> UpdateDocCommentAsync(Guid channel, uint doc, uint docComment, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<DocComment>(new RestRequest($"channels/{channel}/docs/{doc}/comments/{docComment}", Method.Patch).AddJsonBody(new { content }), "docComment");

    /// <summary>
    /// Deletes a <see cref="DocComment">document comment</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <see cref="DocComment">document comment</see> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.UpdateDocs">Required when deleting <see cref="DocComment">document comment</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteDocCommentAsync(Guid channel, uint doc, uint docComment) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}/comments/{docComment}", Method.Delete));
    #endregion

    #region Methods Reactions > Docs
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    public Task AddDocReactionAsync(Guid channel, uint doc, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddDocReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddDocReactionAsync(Guid channel, uint doc, Emote emote) =>
        AddDocReactionAsync(channel, doc, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="doc">specified document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    public Task RemoveDocReactionAsync(Guid channel, uint doc, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveDocReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see>to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveDocReactionAsync(Guid channel, uint doc, Emote emote) =>
        RemoveDocReactionAsync(channel, doc, emote.Id);
    #endregion

    #region Methods Reactions > Doc Comments
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="docComment">specified document comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <paramref name="docComment">document comment</paramref> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    public Task AddDocCommentReactionAsync(Guid channel, uint doc, uint docComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}/comments/{docComment}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddDocCommentReactionAsync(Guid, uint, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <paramref name="docComment">document comment</paramref> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddDocCommentReactionAsync(Guid channel, uint doc, uint docComment, Emote emote) =>
        AddEventCommentReactionAsync(channel, doc, docComment, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="docComment">specified document comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where the <paramref name="docComment">document comment</paramref> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    public Task RemoveDocCommentReactionAsync(Guid channel, uint doc, uint docComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}/comments/{docComment}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> where <paramref name="docComment">document comment</paramref> is</param>
    /// <param name="docComment">The identifier of the <see cref="DocComment">document comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveDocCommentReactionAsync(Guid channel, uint doc, uint docComment, Emote emote) =>
        RemoveDocCommentReactionAsync(channel, doc, docComment, emote.Id);
    #endregion

    #region Methods Announcements channels > Comments
    /// <summary>
    /// Gets a list of <see cref="AnnouncementComment">announcement comments</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to get <see cref="DocComment">comments</see> of</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The list of fetched <see cref="AnnouncementComment">announcement comments</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<AnnouncementComment>> GetAnnouncementCommentsAsync(Guid channel, HashId announcement) =>
        GetResponsePropertyAsync<IList<AnnouncementComment>>(new RestRequest($"channels/{channel}/announcements/{announcement}/comments", Method.Get), "announcementComments");

    /// <summary>
    /// Gets the <paramref name="announcementComment">specified announcement comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to get <see cref="AnnouncementComment">comment</see> of</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <returns>The <see cref="AnnouncementComment">announcement comment</see> that was specified in the arguments</returns>
    public Task<AnnouncementComment> GetAnnouncementCommentAsync(Guid channel, HashId announcement, uint announcementComment) =>
        GetResponsePropertyAsync<AnnouncementComment>(new RestRequest($"channels/{channel}/announcements/{announcement}/comments/{announcementComment}", Method.Get), "announcementComment");

    /// <summary>
    /// Creates a new <see cref="AnnouncementComment">comment</see> on a <see cref="Announcement">announcement</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <see cref="DocComment">document comment</see> should be</param>
    /// <param name="content">The content of the <see cref="AnnouncementComment">announcement comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="AnnouncementComment">announcement comment</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<AnnouncementComment> CreateAnnouncementCommentAsync(Guid channel, HashId announcement, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<AnnouncementComment>(new RestRequest($"channels/{channel}/announcements/{announcement}/comments", Method.Post).AddJsonBody(new { content }), "announcementComment");

    /// <summary>
    /// Edits <see cref="AnnouncementComment">announcement comment's</see> <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <see cref="DocComment">document comment</see> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to update</param>
    /// <param name="content">The new contents of the <see cref="AnnouncementComment">announcement comment</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="announcementComment">announcement comment</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<AnnouncementComment> UpdateAnnouncementCommentAsync(Guid channel, HashId announcement, uint announcementComment, string content) =>
        string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<AnnouncementComment>(new RestRequest($"channels/{channel}/announcements/{announcement}/comments/{announcementComment}", Method.Patch).AddJsonBody(new { content }), "announcementComment");

    /// <summary>
    /// Deletes a <see cref="AnnouncementComment">announcement comment</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <see cref="AnnouncementComment">announcement comment</see> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    /// <permission cref="Permission.UpdateDocs">Required when deleting <see cref="DocComment">document comment</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteAnnouncementCommentAsync(Guid channel, HashId announcement, uint announcementComment) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}/comments/{announcementComment}", Method.Delete));
    #endregion

    #region Methods Reactions > Announcements
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="announcement">specified announcement</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Doc">document</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    public Task AddAnnouncementReactionAsync(Guid channel, HashId announcement, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddAnnouncementReactionAsync(Guid channel, HashId announcement, Emote emote) =>
        AddAnnouncementReactionAsync(channel, announcement, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="announcement">specified announcement</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    public Task RemoveAnnouncementReactionAsync(Guid channel, HashId announcement, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveAnnouncementReactionAsync(Guid, HashId, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveAnnouncementReactionAsync(Guid channel, HashId announcement, Emote emote) =>
        RemoveAnnouncementReactionAsync(channel, announcement, emote.Id);
    #endregion

    #region Methods Reactions > Announcement Comments
    /// <summary>
    /// Adds an <paramref name="emote" /> as a reaction to the <paramref name="announcementComment">specified announcement comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <paramref name="announcementComment">announcement comment</paramref> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    public Task AddAnnouncementCommentReactionAsync(Guid channel, HashId announcement, uint announcementComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcements/{announcement}/comments/{announcementComment}/emotes/{emote}", Method.Put));

    /// <inheritdoc cref="AddAnnouncementCommentReactionAsync(Guid, HashId, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <paramref name="announcementComment">announcement comment</paramref> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddAnnouncementCommentReactionAsync(Guid channel, HashId announcement, uint announcementComment, Emote emote) =>
        AddAnnouncementCommentReactionAsync(channel, announcement, announcementComment, emote.Id);

    /// <summary>
    /// Removes an <paramref name="emote" /> as a reaction from the <paramref name="announcementComment">specified announcement comment</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where the <paramref name="announcementComment">announcement comment</paramref> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetAnnouncements" />
    public Task RemoveAnnouncementCommentReactionAsync(Guid channel, HashId announcement, uint announcementComment, uint emote) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/announcement/{announcement}/comments/{announcementComment}/emotes/{emote}", Method.Delete));

    /// <inheritdoc cref="RemoveTopicReactionAsync(Guid, uint, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="announcement">The identifier of the <see cref="Announcement">announcement</see> where <paramref name="announcementComment">announcement comment</paramref> is</param>
    /// <param name="announcementComment">The identifier of the <see cref="AnnouncementComment">announcement comment</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveAnnouncementCommentReactionAsync(Guid channel, HashId announcement, uint announcementComment, Emote emote) =>
        RemoveAnnouncementCommentReactionAsync(channel, announcement, announcementComment, emote.Id);
    #endregion

    #region Methods Reactions > Vague
    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, Guid message, uint emote) =>
        AddMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, Guid message, Emote emote) =>
        AddMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, Guid message, uint emote) =>
        RemoveMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="message">The identifier of the <see cref="Message">message</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, Guid message, Emote emote) =>
        RemoveMessageReactionAsync(channel, message, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Message">message</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, uint topic, uint emote) =>
        AddTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="AddMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to add a <see cref="Reaction">reaction</see> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    public Task AddReactionAsync(Guid channel, uint topic, Emote emote) =>
        AddTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, uint topic, uint emote) =>
        RemoveTopicReactionAsync(channel, topic, emote);

    /// <inheritdoc cref="RemoveMessageReactionAsync(Guid, Guid, uint)" />
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to remove a <see cref="Reaction">reaction</see> from</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to remove</param>
    public Task RemoveReactionAsync(Guid channel, uint topic, Emote emote) =>
        RemoveTopicReactionAsync(channel, topic, emote);
    #endregion
}
