using System;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// A bad request exception thrown by Guilded API.
    /// </summary>
    /// <remarks>
    /// <para>An exception thrown by Guilded API when the request is invalid/bad. This has these leading causes:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Invalid/bad parameters</term>
    ///         <description>The parameters of given method were invalid or expired</description>
    ///     </item>
    ///     <item>
    ///         <term>Outdated methods</term>
    ///         <description>If you are using newer version of API and older incompatible methods, this can lead to an exception. This would rarely be the case, but still a possibility.</description>
    ///     </item>
    ///     <item>
    ///         <term>Guilded.NET related issue</term>
    ///         <description>This could be related to Guilded.NET itself, especially if new breaking update came and Guilded.NET hasn't changed methods yet.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="GuildedException"/>
    /// <seealso cref="GuildedAuthorizationException"/>
    /// <seealso cref="GuildedPermissionException"/>
    /// <seealso cref="GuildedResourceException"/>
    /// <seealso cref="GuildedWebsocketException"/>
    [Serializable]
    public sealed class GuildedRequestException : GuildedException
    {
        /// <summary>
        /// Creates a new instance of <see cref="GuildedRequestException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedRequestException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedRequestException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedRequestException(string code, string message, RestResponse response) : base(code, message, response) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedRequestException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedRequestException"/> with default message:</para>
        /// <note>Unacceptable. The request was unacceptable. Invalid/bad parameters?</note>
        /// </remarks>
        public GuildedRequestException() : this("Bad request. The request was unacceptable. Invalid/bad parameters?") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedRequestException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedRequestException(string message, Exception inner) : base(message, inner) { }
    }
}