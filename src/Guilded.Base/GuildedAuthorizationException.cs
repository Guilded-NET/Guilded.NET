using System;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// An authorization exception thrown by Guilded API.
/// </summary>
/// <remarks>
/// <para>This is caused if you are trying to connect to Guilded or do an action with invalid or expired authentication token. The only solution is to create a new auth and use it instead.</para>
/// </remarks>
/// <seealso cref="GuildedException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedRequestException" />
/// <seealso cref="GuildedResourceException" />
/// <seealso cref="GuildedWebsocketException" />
[Serializable]
public sealed class GuildedAuthorizationException : GuildedException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedAuthorizationException" /> with only a <paramref name="message" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedAuthorizationException">authorization exception</see> instance</returns>
    /// <seealso cref="GuildedAuthorizationException" />
    /// <seealso cref="GuildedAuthorizationException()" />
    /// <seealso cref="GuildedAuthorizationException(string, Exception)" />
    /// <seealso cref="GuildedAuthorizationException(string, string, RestResponse)" />
    public GuildedAuthorizationException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedAuthorizationException" /> from a <paramref name="response" />.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param>
    /// <returns>New <see cref="GuildedAuthorizationException">authorization exception</see> instance</returns>
    /// <seealso cref="GuildedAuthorizationException" />
    /// <seealso cref="GuildedAuthorizationException()" />
    /// <seealso cref="GuildedAuthorizationException(string)" />
    /// <seealso cref="GuildedAuthorizationException(string, Exception)" />
    public GuildedAuthorizationException(string code, string message, RestResponse response) : base(code, message, response) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedAuthorizationException" /> with a <see cref="Exception.Message">default message</see>.
    /// </summary>
    /// <remarks>
    /// <para>The message will be rendered as:</para>
    /// <code>Invalid. Provided authentication token is invalid or expired.</code>
    /// </remarks>
    /// <returns>New message-only <see cref="GuildedAuthorizationException">authorization exception</see> instance</returns>
    /// <seealso cref="GuildedAuthorizationException" />
    /// <seealso cref="GuildedAuthorizationException(string)" />
    /// <seealso cref="GuildedAuthorizationException(string, Exception)" />
    /// <seealso cref="GuildedAuthorizationException(string, string, RestResponse)" />
    public GuildedAuthorizationException() : this("Invalid. Provided authentication token is invalid or expired.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedAuthorizationException" /> with <paramref name="inner">inner exception</paramref> explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    /// <returns>New message-only <see cref="GuildedAuthorizationException">authorization exception</see> instance</returns>
    /// <seealso cref="GuildedAuthorizationException" />
    /// <seealso cref="GuildedAuthorizationException()" />
    /// <seealso cref="GuildedAuthorizationException(string)" />
    /// <seealso cref="GuildedAuthorizationException(string, string, RestResponse)" />
    public GuildedAuthorizationException(string message, Exception inner) : base(message, inner) { }
    #endregion
}