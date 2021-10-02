using System;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// A permission exception thrown by Guilded API.
    /// </summary>
    /// <remarks>
    /// <para>An exception thrown by Guilded API when request is invalid. This is caused if you are trying to access or do an action that requires permissions, but you don't have them. This can only be fixed by getting said permissions by a server staff and is usually not controlled by you.</para>
    /// </remarks>
    /// <seealso cref="GuildedException"/>
    /// <seealso cref="GuildedAuthorizationException"/>
    /// <seealso cref="GuildedRequestException"/>
    /// <seealso cref="GuildedResourceException"/>
    /// <seealso cref="GuildedWebsocketException"/>
    [Serializable]
    public sealed class GuildedPermissionException : GuildedException
    {
        /// <summary>
        /// Creates a new instance of <see cref="GuildedPermissionException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedPermissionException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedPermissionException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedPermissionException(string code, string message, IRestResponse response) : base(code, message, response) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedPermissionException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedPermissionException"/> with default message:</para>
        /// <blockquote>Forbidden. Guilded client is missing permissions.</blockquote>
        /// </remarks>
        public GuildedPermissionException() : this("Forbidden. Guilded client is missing permissions.") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedPermissionException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedPermissionException(string message, Exception inner) : base(message, inner) { }
    }
}