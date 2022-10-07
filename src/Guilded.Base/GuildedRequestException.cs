using System;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// Represents an exception thrown by Guilded API when a request has invalid parameters.
/// </summary>
/// <remarks>
/// <para>This has these leading causes:</para>
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
/// <seealso cref="GuildedException" />
/// <seealso cref="GuildedRequestException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedResourceException" />
[Serializable]
public sealed class GuildedRequestException : GuildedException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedRequestException" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedRequestException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedRequestException" />
    /// <seealso cref="GuildedRequestException()" />
    /// <seealso cref="GuildedRequestException(string, Exception)" />
    /// <seealso cref="GuildedRequestException(string, string, RestResponse)" />
    public GuildedRequestException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedRequestException" /> with information from given parameters.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param>
    /// <returns>New <see cref="GuildedRequestException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedRequestException" />
    /// <seealso cref="GuildedRequestException()" />
    /// <seealso cref="GuildedRequestException(string)" />
    /// <seealso cref="GuildedRequestException(string, Exception)" />
    public GuildedRequestException(string code, string message, RestResponse response) : base(code, message, response) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedRequestException" /> with default message.
    /// </summary>
    /// <remarks>
    /// <para>Initializes a new instance of <see cref="GuildedRequestException" /> with default message:</para>
    /// <note>Unacceptable. The request was unacceptable. Invalid/bad parameters?</note>
    /// </remarks>
    /// <returns>New message-only <see cref="GuildedRequestException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedRequestException" />
    /// <seealso cref="GuildedRequestException(string)" />
    /// <seealso cref="GuildedRequestException(string, Exception)" />
    /// <seealso cref="GuildedRequestException(string, string, RestResponse)" />
    public GuildedRequestException() : this("Bad request. The request was unacceptable. Invalid/bad parameters?") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedRequestException" /> with an <paramref name="inner" /> exception explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    /// <returns>New message-only <see cref="GuildedRequestException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedRequestException" />
    /// <seealso cref="GuildedRequestException()" />
    /// <seealso cref="GuildedRequestException(string)" />
    /// <seealso cref="GuildedRequestException(string, string, RestResponse)" />
    public GuildedRequestException(string message, Exception inner) : base(message, inner) { }
    #endregion
}