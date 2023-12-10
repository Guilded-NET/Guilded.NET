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
    #region Properties Docs channels
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="Doc">document</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocCreated => ((IEventInfo<DocEvent>)GuildedEvents["DocCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Doc">document</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocDeleted" />
    public IObservable<DocEvent> DocUpdated => ((IEventInfo<DocEvent>)GuildedEvents["DocUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="Doc">document</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    public IObservable<DocEvent> DocDeleted => ((IEventInfo<DocEvent>)GuildedEvents["DocDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocReaction">reaction</see> is added to a <see cref="Doc">document</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    /// <seealso cref="DocReactionRemoved" />
    public IObservable<DocReactionEvent> DocReactionAdded => ((IEventInfo<DocReactionEvent>)GuildedEvents["DocReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocReaction">reaction</see> is added to a <see cref="Doc">document</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCreated" />
    /// <seealso cref="DocUpdated" />
    /// <seealso cref="DocDeleted" />
    /// <seealso cref="DocReactionAdded" />
    public IObservable<DocReactionEvent> DocReactionRemoved => ((IEventInfo<DocReactionEvent>)GuildedEvents["DocReactionDeleted"]).Observable;
    #endregion

    #region Properties Docs channels > Comments
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a new <see cref="DocComment">document comment</see> is posted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentCreated => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocComment">document comment</see> is edited.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentUpdated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentUpdated => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentUpdated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocComment">document comment</see> is deleted.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentReactionAdded" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentEvent> DocCommentDeleted => ((IEventInfo<DocCommentEvent>)GuildedEvents["DocCommentDeleted"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocCommentReaction">reaction</see> is added to a <see cref="DocComment">document comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentReactionCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionRemoved" />
    public IObservable<DocCommentReactionEvent> DocCommentReactionAdded => ((IEventInfo<DocCommentReactionEvent>)GuildedEvents["DocCommentReactionCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when a <see cref="DocCommentReaction">reaction</see> is added to a <see cref="DocComment">document comment</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>DocCommentReactionDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="DocCommentCreated" />
    /// <seealso cref="DocCommentUpdated" />
    /// <seealso cref="DocCommentDeleted" />
    /// <seealso cref="DocCommentReactionAdded" />
    public IObservable<DocCommentReactionEvent> DocCommentReactionRemoved => ((IEventInfo<DocCommentReactionEvent>)GuildedEvents["DocCommentReactionDeleted"]).Observable;
    #endregion

    #region Methods Document channels
    /// <summary>
    /// Gets a list of <see cref="Doc">documents</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="limit">The limit of how many <see cref="Doc">documents</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of the fetched <see cref="Doc">documents</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The list of fetched <see cref="Doc">documents</see> in the specified <paramref name="channel" /></returns>
    public Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null) =>
        GetResponsePropertyAsync<IList<Doc>>(
            new RestRequest($"channels/{channel}/docs", Method.Get)
                .AddOptionalQuery("limit", limit, encode: false)
                .AddOptionalQuery("before", before)
        , "docs");

    /// <summary>
    /// Gets the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <returns>The <see cref="Doc">document</see> that was specified in the arguments</returns>
    public Task<Doc> GetDocAsync(Guid channel, uint doc) =>
        GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Get), "doc");

    /// <summary>
    /// Creates a new <see cref="Doc">document</see>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="title">The title of the <see cref="Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.CreateDocs" />
    /// <permission cref="Permission.UseEveryoneMention">Required when posting a <see cref="Doc">document</see> that contains an <c>@everyone</c> or <c>@here</c> mentions</permission>
    /// <returns>The <see cref="Doc">document</see> that was created by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Doc> CreateDocAsync(Guid channel, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs", Method.Post)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <summary>
    /// Edits the text <paramref name="content" /> or the <paramref name="title" /> of the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The updated <paramref name="doc">document</paramref> will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent <see cref="DocChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Doc">document</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Doc">document</see></param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.UpdateDocs">Required when editing <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    /// <permission cref="Permission.UseEveryoneMention">Required when adding an <c>@everyone</c> or a <c>@here</c> mention to a <see cref="Doc">document</see></permission>
    /// <returns>The <see cref="Doc">document</see> that was updated by the <see cref="AbstractGuildedClient">client</see></returns>
    public Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content) =>
        string.IsNullOrWhiteSpace(title)
        ? throw new ArgumentNullException(nameof(title))
        : string.IsNullOrWhiteSpace(content)
        ? throw new ArgumentNullException(nameof(content))
        : GetResponsePropertyAsync<Doc>(new RestRequest($"channels/{channel}/docs/{doc}", Method.Put)
            .AddJsonBody(new
            {
                title,
                content
            })
        , "doc");

    /// <summary>
    /// Deletes the specified <paramref name="doc">document</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent <see cref="ServerChannel">channel</see></param>
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to delete</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedPermissionException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <permission cref="Permission.GetDocs" />
    /// <permission cref="Permission.DeleteDocs">Required when deleting <see cref="Doc">documents</see> that the <see cref="AbstractGuildedClient">client</see> doesn't own</permission>
    public Task DeleteDocAsync(Guid channel, uint doc) =>
        ExecuteRequestAsync(new RestRequest($"channels/{channel}/docs/{doc}", Method.Delete));
    #endregion
}