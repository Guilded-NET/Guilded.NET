using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Guilded.Base;
using Websocket.Client;
using Websocket.Client.Models;

namespace Guilded.Connection;

/// <summary>
/// An API wrapping layer for all Guilded client.
/// </summary>
/// <remarks>
/// <para>The base that adds a layer to Guilded API wrapping. This is used in all Guilded.NET clients.</para>
/// </remarks>
public abstract partial class BaseGuildedConnection : BaseGuildedService, IAsyncDisposable
{
    #region Fields
    /// <summary>
    /// Gets an event that occurs when the client gets connected.
    /// </summary>
    /// <remarks>
    /// <para>This usually occurs once <see cref="ConnectAsync" /> is called and no errors get thrown.</para>
    /// </remarks>
    /// <seealso cref="ConnectAsync" />
    /// <seealso cref="Reconnected" />
    /// <seealso cref="Disconnected" />
    /// <seealso cref="DisconnectedWithError" />
    protected Subject<BaseGuildedConnection> ConnectedSubject { get; } = new();
    #endregion

    #region Properties
    /// <inheritdoc cref="ConnectedSubject" />
    public IObservable<BaseGuildedConnection> Connected => ConnectedSubject.AsObservable();

    /// <summary>
    /// An event when client gets reconnected.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client reconnects to Guilded.</para>
    /// </remarks>
    /// <seealso cref="Connected" />
    /// <seealso cref="Disconnected" />
    /// <seealso cref="DisconnectedWithError" />
    public IObservable<ReconnectionInfo> Reconnected => Websocket.ReconnectionHappened;

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> for an event when the client gets disconnected.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client disconnects from Guilded.</para>
    /// <para>This usually occurs once <see cref="DisconnectAsync" /> is called and no errors get thrown, or once an error occurs.</para>
    /// </remarks>
    /// <seealso cref="DisconnectAsync" />
    /// <seealso cref="DisconnectedWithError" />
    /// <seealso cref="Reconnected" />
    /// <seealso cref="Connected" />
    public IObservable<DisconnectionInfo> Disconnected => Websocket.DisconnectionHappened;

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> for an event when the <see cref="BaseGuildedConnection">client</see> gets disconnected with an <see cref="Exception">error</see>.
    /// </summary>
    /// <seealso cref="DisconnectAsync" />
    /// <seealso cref="DisconnectedWithError" />
    /// <seealso cref="Reconnected" />
    /// <seealso cref="Connected" />
    public IObservable<DisconnectionInfo> DisconnectedWithError => Disconnected.Where(x => x.Exception is not null);
    #endregion

    #region Constructors
    /// <summary>
    /// Creates default settings for <see cref="BaseGuildedConnection" />'s child types.
    /// </summary>
    /// <remarks>
    /// <para>Inititates basic client components for API-related things, such as WebSocket and REST client. The rest is up to child types.</para>
    /// </remarks>
    /// <param name="apiUrl">The URL to Guilded-like API</param>
    /// <param name="websocketUrl">The URL to Guilded-like WebSocket client</param>
    protected BaseGuildedConnection(Uri apiUrl, Uri websocketUrl) : base(apiUrl)
    {
        Func<ClientWebSocket> factory = new(() =>
        {
            ClientWebSocket socket = new()
            {
                Options = {
                    KeepAliveInterval = TimeSpan.FromMilliseconds(DefaultHeartbeatInterval),
                }
            };
            // Set any required headers, such as auth token
            foreach (KeyValuePair<string, string> header in AdditionalHeaders)
                socket.Options.SetRequestHeader(header.Key, header.Value);

            if (LastMessageId is not null) socket.Options.SetRequestHeader("guilded-last-message-id", LastMessageId);

            return socket;
        });
        Websocket = new(websocketUrl ?? GuildedUrl.Websocket, factory);

        // Don't keep on erroring
        Websocket
            .DisconnectionHappened
            .Where(x => x.CloseStatus == WebSocketCloseStatus.InvalidPayloadData)
            .Subscribe(_ => LastMessageId = null);

        // Event stuff
        Websocket
            .MessageReceived
            .Where(msg => msg.MessageType == WebSocketMessageType.Text)
            .Subscribe(OnWebsocketResponse);
    }

    /// <summary>
    /// Creates default settings for <see cref="BaseGuildedConnection" />'s child types with <see cref="GuildedUrl.Api" /> as URL.
    /// </summary>
    /// <remarks>
    /// <para>Inititates REST client and serializer settings.</para>
    /// <para>The <see cref="GuildedUrl.Api" /> property and <see cref="GuildedUrl.Websocket" /> property URLs will be used by default.</para>
    /// </remarks>
    protected BaseGuildedConnection() : this(GuildedUrl.Api, GuildedUrl.Websocket) { }
    #endregion

    #region Methods
    /// <summary>
    /// Connects the <see cref="BaseGuildedConnection">client</see> to Guilded API.
    /// </summary>
    /// <seealso cref="DisconnectAsync" />
    public virtual async Task ConnectAsync()
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
    /// Stops any connections the <see cref="BaseGuildedConnection">client</see> holds to Guilded API.
    /// </summary>
    /// <seealso cref="ConnectAsync" />
    /// <seealso cref="DisposeAsync" />
    public virtual async Task DisconnectAsync()
    {
        if (Websocket.IsRunning)
            await Websocket.StopOrFail(WebSocketCloseStatus.NormalClosure, "DisconnectAsync invoked").ConfigureAwait(false);
    }

    /// <summary>
    /// Disposes the <see cref="BaseGuildedConnection">connection</see>.
    /// </summary>
    /// <seealso cref="DisconnectAsync" />
    public virtual async ValueTask DisposeAsync()
    {
        await DisconnectAsync();

        Dispose();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the <see cref="BaseGuildedConnection">connection</see>.
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        Websocket.Dispose();
        GC.SuppressFinalize(this);
    }
    #endregion
}