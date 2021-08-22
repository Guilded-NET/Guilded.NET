using System;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An exception thrown by Guilded API.
    /// </summary>
    [Serializable]
    public sealed class GuildedResourceException : GuildedException
    {
        /// <summary>
        /// Creates a new instance of <see cref="GuildedResourceException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedResourceException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedResourceException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedResourceException(string code, string message, IRestResponse response) : base(code, message, response) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedResourceException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedResourceException"/> with default message:</para>
        /// <para>"Not found. Given item has not been found."</para>
        /// </remarks>
        public GuildedResourceException() : this("Not found. Given item has not been found.") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedResourceException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedResourceException(string message, Exception inner) : base(message, inner) { }
    }
}