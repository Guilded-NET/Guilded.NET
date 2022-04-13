using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Events;
using Guilded.Base.Users;
using Newtonsoft.Json.Linq;

using RestSharp;
using Websocket.Client;

namespace Guilded;

/// <summary>
/// A base for all Guilded clients.
/// </summary>
/// <remarks>
/// <para>A base class for <see cref="GuildedBotClient"/> and soon other clients.</para>
/// <para>There is not much to be used here. It is recommended to use <see cref="GuildedBotClient"/>.</para>
/// </remarks>
/// <seealso cref="GuildedBotClient"/>
public abstract partial class AbstractGuildedClient : BaseGuildedClient
{
    /// <summary>
    /// An event when the client is prepared.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client has added finishing touches. You can use this as a signal <see cref="Prepared"/> ensures all client functions are properly working and can be used.</para>
    /// <para>As of now, this is called at the same time as <see cref="BaseGuildedClient.Connected"/> event.</para>
    /// </remarks>
    protected Subject<Me> PreparedSubject = new();
    /// <inheritdoc cref="PreparedSubject"/>
    public IObservable<Me> Prepared => PreparedSubject.AsObservable();
    /// <inheritdoc cref="WelcomeEvent.User" />
    public Me? Me { get; protected set; }
    /// <summary>
    /// Whether the client is already prepared.
    /// </summary>
    /// <value>Client is prepared</value>
    public bool IsPrepared { get; protected set; }
    /// <summary>
    /// A base constructor for creating Guilded clients.
    /// </summary>
    /// <seealso cref="GuildedBotClient()"/>
    /// <seealso cref="GuildedBotClient(string)"/>
    protected AbstractGuildedClient()
    {
        #region Event list
        // Dictionary of supported events, so we wouldn't need to manually do it.
        // The only manual work to be done is in AbstractGuildedClient.Messages.cs file,
        // which only allows us to subscribe to events and it is literally +1 member
        // to be added and copy pasting for the most part.
        GuildedEvents = new Dictionary<object, IEventInfo<object>>
        {
            // Event messages
            { (byte)1,                  new EventInfo<WelcomeEvent>() },
            { (byte)2,                  new EventInfo<ResumeEvent>() },
            // Team events
            { "TeamMemberJoined",       new EventInfo<MemberJoinedEvent>() },
            { "TeamMemberUpdated",      new EventInfo<MemberUpdatedEvent>() },
            { "teamRolesUpdated",       new EventInfo<RolesUpdatedEvent>() },
            { "TeamXpAdded",            new EventInfo<XpAddedEvent>() },
            { "TeamMemberRemoved",      new EventInfo<MemberRemovedEvent>() },
            { "TeamMemberBanned",       new EventInfo<MemberBanEvent>() },
            { "TeamMemberUnbanned",     new EventInfo<MemberBanEvent>() },
            { "TeamWebhookCreated",     new EventInfo<WebhookEvent>() },
            { "TeamWebhookUpdated",     new EventInfo<WebhookEvent>() },
            // Chat messages
            { "ChatMessageCreated",     new EventInfo<MessageEvent>() },
            { "ChatMessageUpdated",     new EventInfo<MessageEvent>() },
            { "ChatMessageDeleted",     new EventInfo<MessageDeletedEvent>() }
        };
        #endregion

        WebsocketMessage.Subscribe(
            OnSocketMessage,
            // Relay the error onto welcome observable
            e => GuildedEvents[(byte)1].OnError(e)
        );

        // Prepare state
        Welcome.Subscribe(welcome =>
        {
            Me = welcome.User;

            if (!IsPrepared)
            {
                PreparedSubject.OnNext(Me);
                IsPrepared = true;
            }
        });
        Disconnected.Subscribe(info =>
        {
            if (info.Type != DisconnectionType.NoMessageReceived)
                IsPrepared = false;
        });
    }
    /// <summary>
    /// Connects this client to Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Connects to Guilded and starts Guilded's WebSocket.</para>
    /// </remarks>
    /// <seealso cref="DisconnectAsync"/>
    /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
    /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
    public override async Task ConnectAsync()
    {
        try
        {
            await Websocket.StartOrFail().ConfigureAwait(false);
            ConnectedSubject.OnNext(this);
        }
        catch (Exception e)
        {
            ConnectedSubject.OnError(e);
        }
    }
    /// <summary>
    /// Disconnects this client from Guilded.
    /// </summary>
    /// <remarks>
    /// <para>The method that stops Guilded WebSocket.</para>
    /// </remarks>
    /// <seealso cref="ConnectAsync"/>
    /// <seealso cref="Dispose"/>
    /// <seealso cref="GuildedBotClient.ConnectAsync()"/>
    /// <seealso cref="GuildedBotClient.ConnectAsync(string)"/>
    public override async Task DisconnectAsync()
    {
        if (Websocket.IsRunning)
            await Websocket.StopOrFail(WebSocketCloseStatus.NormalClosure, "manual").ConfigureAwait(false);
    }
    /// <summary>
    /// Disposes <see cref="AbstractGuildedClient"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>Disposes <see cref="AbstractGuildedClient"/> and its WebSockets.</para>
    /// </remarks>
    /// <seealso cref="DisconnectAsync"/>
    public override void Dispose()
    {
        DisconnectAsync().GetAwaiter().GetResult();
        // Dispose them entirely; they aren't disposed by DisconnectAsync, only shut down
        Websocket.Dispose();
    }
    private async Task<T> GetResponseProperty<T>(RestRequest request, object key) =>
        (await ExecuteRequestAsync<JContainer>(request).ConfigureAwait(false)).Data![key]!.ToObject<T>(GuildedSerializer)!;
}