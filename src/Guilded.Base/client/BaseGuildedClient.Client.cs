using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Websocket.Client;
using Websocket.Client.Models;

namespace Guilded.Base;

/// <summary>
/// An API wrapping layer for all Guilded client.
/// </summary>
/// <remarks>
/// <para>The base that adds a layer to Guilded API wrapping. This is used in all Guilded.NET clients.</para>
/// </remarks>
public abstract partial class BaseGuildedClient : BaseGuildedService, IAsyncDisposable
{
    #region Fields
    /// <summary>
    /// An event when the client gets connected.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client connects to Guilded.</para>
    /// <para>This usually occurs once <see cref="ConnectAsync" /> is called and no errors get thrown.</para>
    /// </remarks>
    /// <seealso cref="ConnectAsync" />
    /// <seealso cref="Disconnected" />
    protected Subject<BaseGuildedClient> ConnectedSubject = new();
    #endregion

    #region Properties
    /// <inheritdoc cref="ConnectedSubject" />
    public IObservable<BaseGuildedClient> Connected => ConnectedSubject.AsObservable();

    /// <summary>
    /// An event when client gets reconnected.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client reconnects to Guilded.</para>
    /// </remarks>
    /// <seealso cref="Disconnected" />
    /// <seealso cref="Connected" />
    public IObservable<ReconnectionInfo> Reconnected => Websocket.ReconnectionHappened;

    /// <summary>
    /// An event when the client gets disconnected.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once Guilded client disconnects from Guilded.</para>
    /// <para>This usually occurs once <see cref="DisconnectAsync" /> is called and no errors get thrown, or once an error occurs.</para>
    /// </remarks>
    /// <seealso cref="DisconnectAsync" />
    /// <seealso cref="Connected" />
    public IObservable<DisconnectionInfo> Disconnected => Websocket.DisconnectionHappened;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates default settings for <see cref="BaseGuildedClient" />'s child types.
    /// </summary>
    /// <remarks>
    /// <para>Inititates basic client components for API-related things, such as WebSocket and REST client. The rest is up to child types.</para>
    /// </remarks>
    /// <param name="apiUrl">The URL to Guilded-like API</param>
    /// <param name="websocketUrl">The URL to Guilded-like WebSocket client</param>
    protected BaseGuildedClient(Uri apiUrl, Uri websocketUrl) : base(apiUrl)
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

        // Event stuff
        Websocket.MessageReceived
            .Where(msg => msg.MessageType == WebSocketMessageType.Text)
            .Subscribe(OnWebsocketResponse);
    }

    /// <summary>
    /// Creates default settings for <see cref="BaseGuildedClient" />'s child types with <see cref="GuildedUrl.Api" /> as URL.
    /// </summary>
    /// <remarks>
    /// <para>Inititates REST client and serializer settings.</para>
    /// <para>The <see cref="GuildedUrl.Api" /> property and <see cref="GuildedUrl.Websocket" /> property URLs will be used by default.</para>
    /// </remarks>
    protected BaseGuildedClient() : this(GuildedUrl.Api, GuildedUrl.Websocket) { }
    #endregion

    #region Methods
    /// <summary>
    /// Connects this client to Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Creates a new connection to Guilded with this client.</para>
    /// <note type="tip">See documentation of child types for more information.</note>
    /// </remarks>
    /// <seealso cref="DisconnectAsync" />
    public abstract Task ConnectAsync();

    /// <summary>
    /// Disconnects this client from Guilded.
    /// </summary>
    /// <remarks>
    /// <para>Stops any connections this client has with Guilded.</para>
    /// </remarks>
    /// <seealso cref="ConnectAsync" />
    /// <seealso cref="DisposeAsync" />
    public virtual async Task DisconnectAsync()
    {
        if (Websocket.IsRunning)
            await Websocket.StopOrFail(WebSocketCloseStatus.NormalClosure, "DisconnectAsync invoked").ConfigureAwait(false);
    }

    /// <summary>
    /// Disposes <see cref="BaseGuildedClient" /> instance.
    /// </summary>
    /// <seealso cref="DisconnectAsync" />
    public virtual async ValueTask DisposeAsync()
    {
        await DisconnectAsync();

        Rest.Dispose();
        Websocket.Dispose();
        GC.SuppressFinalize(this);
    }
    #endregion
}