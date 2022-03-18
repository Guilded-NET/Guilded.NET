using System;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// A resource exception thrown by Guilded API.
/// </summary>
/// <remarks>
/// <para>An exception thrown by Guilded API when the request has invalid path. This has these leading causes:</para>
/// <list type="bullet">
///     <item>
///         <term>Invalid/bad parameters</term>
///         <description>The parameters of given method were invalid or expired</description>
///     </item>
///     <item>
///         <term>Guilded.NET related issue</term>
///         <description>This could be related to Guilded.NET itself, especially if new breaking update came and Guilded.NET hasn't updated endpoints.</description>
///     </item>
/// </list>
/// </remarks>
/// <seealso cref="GuildedException"/>
/// <seealso cref="GuildedAuthorizationException"/>
/// <seealso cref="GuildedPermissionException"/>
/// <seealso cref="GuildedRequestException"/>
/// <seealso cref="GuildedWebsocketException"/>
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
    public GuildedResourceException(string code, string message, RestResponse response) : base(code, message, response) { }
    /// <summary>
    /// Creates a new instance of <see cref="GuildedResourceException"/> with default message.
    /// </summary>
    /// <remarks>
    /// <para>Creates a new instance of <see cref="GuildedResourceException"/> with default message:</para>
    /// <note>Not found. Given item has not been found.</note>
    /// </remarks>
    public GuildedResourceException() : this("Not found. Given item has not been found.") { }
    /// <summary>
    /// Creates a new instance of <see cref="GuildedResourceException"/> with inner exception explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    public GuildedResourceException(string message, Exception inner) : base(message, inner) { }
}