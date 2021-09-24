using System;
using Websocket.Client;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An error received from Guilded WebSocket.
    /// </summary>
    /// <remarks>
    /// <para>An error that occurs involving WebSockets. This error can occur in these different ways:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Expired last event message identifier</term>
    ///         <description>When the passed <c>guilded-last-message-id</c> is expired or invalid.</description>
    ///     </item>
    /// </list>
    /// <para>In API, this is a WebSocket event with an opcode of <c>8</c> and no name.</para>
    /// </remarks>
    /// <seealso cref="Events.WelcomeEvent"/>
    /// <seealso cref="Events.ResumeEvent"/>
    /// <seealso cref="GuildedException"/>
    /// <seealso cref="GuildedAuthorizationException"/>
    /// <seealso cref="GuildedPermissionException"/>
    /// <seealso cref="GuildedRequestException"/>
    /// <seealso cref="GuildedResourceException"/>
    [Serializable]
    public class GuildedWebsocketException : Exception
    {
        /// <summary>
        /// The response message from Guilded WebSocket.
        /// </summary>
        /// <remarks>
        /// <para>WebSocket response message that holds the error found.</para>
        /// <para>Use this if further information is necessary.</para>
        /// </remarks>
        /// <value>WebSocket response</value>
        public ResponseMessage Response
        {
            get; set;
        }
        /// <summary>
        /// Creates a new empty instance of <see cref="GuildedWebsocketException"/>.
        /// </summary>
        public GuildedWebsocketException() { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedWebsocketException"/> with a message received.
        /// </summary>
        /// <param name="message">The message that was received from Guilded Websocket</param>
        public GuildedWebsocketException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedWebsocketException"/> from WebSocket response message.
        /// </summary>
        /// <param name="response">The response message from Guilded WebSocket</param>
        /// <param name="message">The message that was received from Guilded Websocket</param>
        public GuildedWebsocketException(ResponseMessage response, string message) : this(message) =>
            Response = response;
        /// <summary>
        /// Creates a new instance of <see cref="GuildedWebsocketException"/> with a message received.
        /// </summary>
        /// <param name="message">The message that was received from Guilded Websocket</param>
        /// <param name="inner">The inner exception of this error</param>
        public GuildedWebsocketException(string message, Exception inner) : base(message, inner) { }
        // protected GuildedWebsocketException(
        //     System.Runtime.Serialization.SerializationInfo info,
        //     System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}