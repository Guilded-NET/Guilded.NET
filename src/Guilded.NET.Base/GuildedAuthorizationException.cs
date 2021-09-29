using System;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An authorization exception thrown by Guilded API.
    /// </summary>
    /// <remarks>
    /// <para>An exception thrown by Guilded API when the request is invalid. This is caused if you are trying to connect to Guilded or do an action with invalid or expired authentication token. The only solution is to create a new auth and use it instead.</para>
    /// </remarks>
    /// <seealso cref="GuildedException"/>
    /// <seealso cref="GuildedPermissionException"/>
    /// <seealso cref="GuildedRequestException"/>
    /// <seealso cref="GuildedResourceException"/>
    /// <seealso cref="GuildedWebsocketException"/>
    [Serializable]
    public sealed class GuildedAuthorizationException : GuildedException
    {
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthorizationException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedAuthorizationException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthorizationException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedAuthorizationException(string code, string message, IRestResponse response) : base(code, message, response) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthorizationException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedAuthorizationException"/> with default message:</para>
        /// <para>"Invalid. Provided authentication token is invalid or expired."</para>
        /// </remarks>
        public GuildedAuthorizationException() : this("Invalid. Provided authentication token is invalid or expired.") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthorizationException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedAuthorizationException(string message, Exception inner) : base(message, inner) { }
    }
}