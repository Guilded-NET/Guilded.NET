using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Guilded.Base.Events;

using Websocket.Client;


namespace Guilded.Base;

public abstract partial class BaseGuildedClient
{
    internal const int welcome_opcode = 1, close_opcode = 8;
    /// <summary>
    /// The default timespan between each interval in milliseconds.
    /// </summary>
    private const double DefaultHeartbeatInterval = 22500;
    /// <summary>
    /// The WebSocket that will be used by the client.
    /// </summary>
    /// <remarks>
    /// <para>The WebSocket that will be used by the client to receive all Guilded events and event messages.</para>
    /// </remarks>
    /// <seealso cref="Rest"/>
    /// <value>Main WebSocket</value>
    public WebsocketClient Websocket { get; set; }
    /// <summary>
    /// The identifier of the last WebSocket message.
    /// </summary>
    /// <remarks>
    /// <para>Allows you to set the identifier of the last message to get events that weren't received.</para>
    /// </remarks>
    /// <value>WebSocket Message ID?</value>
    public string? LastMessageId { get; set; }
    private readonly Subject<GuildedSocketMessage> OnWebsocketMessage = new();
    /// <summary>
    /// An event when WebSocket receives a message.
    /// </summary>
    /// <remarks>
    /// <para>An event when WebSocket receives any kind of message from Guilded.</para>
    /// <para>If event with opcode <c>8</c> is received, it is given as an exception instead.</para>
    /// </remarks>
    /// <exception cref="GuildedWebsocketException">Received when any kind of error is received. Handled through <see cref="Subject{T}.OnError(Exception)"/>.</exception>
    protected IObservable<GuildedSocketMessage> WebsocketMessage => OnWebsocketMessage.AsObservable();
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
        // Check for a welcome message to change hearbeat interval
        else if (@event.Opcode == welcome_opcode)
        {
            //Websocket.NativeClient.Options.KeepAliveInterval = TimeSpan.FromMilliseconds(@event.RawData.Value<double>("heartbeatIntervalMs"));
        }
        else if (@event.Opcode == close_opcode)
        {
            OnWebsocketMessage.OnError(
                new GuildedWebsocketException(response, @event!.RawData?.Value<string>("message")!)
            );
            return;
        }
        OnWebsocketMessage.OnNext(@event);
    }
}