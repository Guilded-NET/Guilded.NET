using System;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An exception thrown by Guilded API.
    /// </summary>
    /// <remarks>
    /// <para>An exception thrown by Guilded API when request is invalid.</para>
    /// <para>This is caused if you are trying to connect to Guilded or do an action with invalid
    /// or expired authentication token.</para>
    /// </remarks>
    [Serializable]
    public sealed class GuildedAuthenticationException : GuildedException
    {
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthenticationException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedAuthenticationException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthenticationException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedAuthenticationException(string code, string message, IRestResponse response) : base(code, message, response) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthenticationException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedAuthenticationException"/> with default message:</para>
        /// <para>"Invalid. Provided authentication token is invalid or expired."</para>
        /// </remarks>
        public GuildedAuthenticationException() : this("Invalid. Provided authentication token is invalid or expired.") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedAuthenticationException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedAuthenticationException(string message, Exception inner) : base(message, inner) { }
    }
}