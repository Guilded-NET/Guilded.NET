using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Guilded.Base;
using Websocket.Client;

namespace Guilded.Connection;

public abstract partial class BaseGuildedConnection
{
    #region Constants
    /// <summary>
    /// The default timespan between each interval in milliseconds.
    /// </summary>
    private const double DefaultHeartbeatInterval = 22500;
    #endregion

    #region Fields
    private readonly Subject<GuildedSocketMessage> _onWebsocketMessage = new();

    private readonly Subject<GuildedSocketException> _onWebsocketError = new();
    #endregion

    #region Properties
    /// <summary>
    /// Gets the WebSocket that will be used by the <see cref="BaseGuildedConnection">client</see> to receive all Guilded events.
    /// </summary>
    /// <value>The WebSocket that will be used by the <see cref="BaseGuildedConnection">client</see> to receive all Guilded events</value>
    /// <seealso cref="BaseGuildedConnection" />
    /// <seealso cref="BaseGuildedService.Rest" />
    public WebsocketClient Websocket { get; }

    /// <summary>
    /// Gets the identifier of the last <see cref="GuildedSocketMessage">WebSocket message</see>.
    /// </summary>
    /// <remarks>
    /// <para>Allows you to set the identifier of the last message to get events that weren't received.</para>
    /// </remarks>
    /// <value>The identifier of the last <see cref="GuildedSocketMessage">WebSocket message</see></value>
    /// <seealso cref="BaseGuildedConnection" />
    /// <seealso cref="Websocket" />
    /// <seealso cref="BaseGuildedService.Rest" />
    protected string? LastMessageId { get; set; }

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> that is invoked when <see cref="Websocket" /> receives a <see cref="GuildedSocketMessage">message</see>.
    /// </summary>
    /// <remarks>
    /// <para>If event with opcode <c>8</c> or <c>9</c> is received, <see cref="WebsocketError" /> is invoked instead.</para>
    /// </remarks>
    /// <value>An <see cref="IObservable{T}">observable</see> that is invoked when <see cref="Websocket" /> receives a <see cref="GuildedSocketMessage">message</see></value>
    /// <seealso cref="BaseGuildedConnection" />
    /// <seealso cref="Websocket" />
    /// <seealso cref="LastMessageId" />
    public IObservable<GuildedSocketMessage> WebsocketMessage => _onWebsocketMessage.AsObservable();

    /// <summary>
    /// Gets an <see cref="IObservable{T}">observable</see> that is invoked when <see cref="Websocket" /> receives an <see cref="GuildedSocketException">error</see>.
    /// </summary>
    /// <value>An <see cref="IObservable{T}">observable</see> that is invoked when <see cref="Websocket" /> receives an <see cref="GuildedSocketException">error</see>.</value>
    /// <seealso cref="BaseGuildedConnection" />
    /// <seealso cref="Websocket" />
    /// <seealso cref="LastMessageId" />
    public IObservable<GuildedSocketException> WebsocketError => _onWebsocketError.AsObservable();
    #endregion

    #region Methods
    /// <summary>
    /// Used for when any WebSocket receives a message.
    /// </summary>
    /// <remarks>
    /// <para>An event handler method that gets called once any message is received from a WebSocket.</para>
    /// <para>Override this if you don't like how Guilded.NET handles events or need any additional changes/features to it.</para>
    /// </remarks>
    /// <param name="response">The response received from Guilded WebSocket</param>
    protected virtual void OnWebsocketResponse(ResponseMessage response)
    {
        GuildedSocketMessage? @event = Deserialize<GuildedSocketMessage>(response.Text);

        if (@event is null)
        {
            return;
        }
        else if (@event.Opcode == SocketOpcode.InvalidCursor || @event.Opcode == SocketOpcode.InternalError)
        {
            // If the error is related to the last message
            LastMessageId = null;

            string? message = @event.RawData?.Value<string>("message");

            // To not stop operations we use different observable instead of OnError. Might change back to OnError at some point.
            _onWebsocketError.OnNext(
                new GuildedSocketException(response, message!)
            );

            return;
        }
        else if (@event.MessageId is not null)
        {
            LastMessageId = @event.MessageId;
        }

        _onWebsocketMessage.OnNext(@event);
    }
    #endregion
}