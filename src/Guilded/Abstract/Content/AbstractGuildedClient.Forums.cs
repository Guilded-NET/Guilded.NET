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
    #region Properties Forum channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Topic">forum topic</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicCreated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUpdated => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicDeleted => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is pinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicPinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicPinned => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicPinned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is unpinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUnpinned</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUnpinned => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUnpinned"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is pinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicLocked</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicUnlocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicLocked => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicLocked"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Topic">forum topic</see> is unpinned.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicUnlocked</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicReactionRemoved" />
    public IObservable<TopicEvent> TopicUnlocked => ((IEventInfo<TopicEvent>)GuildedEvents["ForumTopicUnlocked"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicReaction">reaction</see> is added to a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicReactionRemoved" />
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    public IObservable<TopicReactionEvent> TopicReactionAdded => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicReaction">reaction</see> is removed from a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicReactionAdded" />
    /// <seealso cref="TopicCreated" />
    /// <seealso cref="TopicUpdated" />
    /// <seealso cref="TopicDeleted" />
    /// <seealso cref="TopicPinned" />
    /// <seealso cref="TopicUnpinned" />
    /// <seealso cref="TopicLocked" />
    /// <seealso cref="TopicUnlocked" />
    public IObservable<TopicReactionEvent> TopicReactionRemoved => ((IEventInfo<TopicReactionEvent>)GuildedEvents["ForumTopicReactionDeleted"]).Observable;
    #endregion

    #region Properties Forum channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="TopicComment">forum topic comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentCreated => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicComment">forum topic comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentUpdated => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicComment">forum topic comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentReactionAdded" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentEvent> TopicCommentDeleted => ((IEventInfo<TopicCommentEvent>)GuildedEvents["ForumTopicCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicCommentReaction">reaction</see> is added to a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionRemoved" />
    public IObservable<TopicCommentReactionEvent> TopicCommentReactionAdded => ((IEventInfo<TopicCommentReactionEvent>)GuildedEvents["ForumTopicCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="TopicCommentReaction">reaction</see> is added to a <see cref="TopicComment">forum topic comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>ForumTopicCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="TopicCommentCreated" />
    /// <seealso cref="TopicCommentUpdated" />
    /// <seealso cref="TopicCommentDeleted" />
    /// <seealso cref="TopicCommentReactionAdded" />
    public IObservable<TopicCommentReactionEvent> TopicCommentReactionRemoved => ((IEventInfo<TopicCommentReactionEvent>)GuildedEvents["ForumTopicCommentReactionDeleted"]).Observable;
    #endregion

    #region Methods Forum channels > Topics
    /// <summary>
    /// Gets a list of <see cref="Topic">forum topics</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Topic">topics</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="Topic">topics</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The list of fetched <see cref="Topic">forum topics</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<TopicSummary>> GetTopicsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<TopicSummary>>(
            new RestRequest($"channels/{channel}/topics", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "forumTopics");

    /// <summary>
    /// Gets the <paramref name="topic">specified forum topic</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <returns>The <see cref="Topic">forum topic</see> that was specified in the arguments</returns>
    public Task<Topic> GetTopicAsync(Guid channel, uint topic) =>
        GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Get), "forumTopic");

    /// <summary>
    /// Creates a new <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The content of the <see cref="Topic">forum topic</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopics" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Topic">forum topic</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Topic">forum topic</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Topic> CreateTopicAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <summary>
    /// Edits <see cref="Topic">forum topic's</see> <paramref name="title" /> and <paramref name="content" />.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to update</param>
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The new contents of the <see cref="Topic">forum topic</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.CreateTopics" />
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <paramref name="topic">forum topic</paramref> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Topic> UpdateTopicAsync(Guid channel, uint topic, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Topic>(new RestRequest($"channels/{channel}/topics/{topic}", Method.Patch)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "forumTopic");

    /// <summary>
    /// Deletes a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics">Required when deleting <see cref="Topic">forum topic</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}", Method.Delete));

    /// <summary>
    /// Pins a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to pin</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics" />
    public Task PinTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/pin", Method.Put));

    /// <inheritdoc cref="PinTopicAsync(Guid, uint)" />
    [Obsolete($"Use `{nameof(PinTopicAsync)}` instead")]
    public Task AddTopicPinAsync(Guid channel, uint topic) =>
        PinTopicAsync(channel, topic);

    /// <summary>
    /// Unpins a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unpin</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.ManageTopics" />
    public Task UnpinTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/pin", Method.Delete));

    /// <inheritdoc cref="UnpinTopicAsync(Guid, uint)" />
    [Obsolete($"Use `{nameof(UnpinTopicAsync)}` instead")]
    public Task RemoveTopicPinAsync(Guid channel, uint topic) =>
        UnpinTopicAsync(channel, topic);

    /// <summary>
    /// Locks a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to lock</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.LockTopics" />
    public Task LockTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/lock", Method.Put));

    /// <summary>
    /// Unlocks a <see cref="Topic">forum topic</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unlock</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetTopics" />
    /// <permission cref="Permission.LockTopics" />
    public Task UnlockTopicAsync(Guid channel, uint topic) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/topics/{topic}/lock", Method.Delete));
    #endregion
}