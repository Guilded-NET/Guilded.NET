using System;
using Guilded.Base.Servers;
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
/// <seealso cref="GuildedTooManyRequestsException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedResourceException" />
/// <seealso cref="GuildedWebsocketException" />
[Serializable]
public sealed class GuildedTooManyRequestsException : GuildedException
{
    #region Properties
    /// <summary>
    /// Gets the time to wait after another request.
    /// </summary>
    /// <value>Time to wait</value>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="IsFromSlowmode" />
    public TimeSpan TimeLeft { get; }

    /// <summary>
    /// Gets whether <see cref="GuildedTooManyRequestsException">too many request error</see> was caused by <see cref="ServerChannel">channel's slowmode settingss</see>.
    /// </summary>
    /// <value><see cref="GuildedTooManyRequestsException">Error</see> comes from <see cref="ServerChannel">channel's slowmode</see></value>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="TimeLeft" />
    public bool IsFromSlowmode { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedTooManyRequestsException" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedTooManyRequestsException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="GuildedTooManyRequestsException()" />
    /// <seealso cref="GuildedTooManyRequestsException(string, Exception)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, string, RestResponse, TimeSpan, bool)" />
    public GuildedTooManyRequestsException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedTooManyRequestsException" /> with information from given parameters.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="retryAfter">The time to wait after another request</param>
    /// <param name="isFromSlowmode">Whether <see cref="GuildedTooManyRequestsException">too many request error</see> was caused by <see cref="ServerChannel">channel's slowmode settingss</see></param>
    /// <returns>New <see cref="GuildedTooManyRequestsException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="GuildedTooManyRequestsException()" />
    /// <seealso cref="GuildedTooManyRequestsException(string)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, Exception)" />
    public GuildedTooManyRequestsException(string message, TimeSpan retryAfter, bool isFromSlowmode) : this("TooManyRequests", message, null, retryAfter, isFromSlowmode) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedTooManyRequestsException" /> with information from given parameters.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param>
    /// <param name="retryAfter">The time to wait after another request</param>
    /// <param name="isFromSlowmode">Whether <see cref="GuildedTooManyRequestsException">too many request error</see> was caused by <see cref="ServerChannel">channel's slowmode settingss</see></param>
    /// <returns>New <see cref="GuildedTooManyRequestsException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="GuildedTooManyRequestsException()" />
    /// <seealso cref="GuildedTooManyRequestsException(string)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, Exception)" />
    public GuildedTooManyRequestsException(string code, string message, RestResponse? response, TimeSpan retryAfter, bool isFromSlowmode) : base(code, message, response) =>
        (TimeLeft, IsFromSlowmode) = (retryAfter, isFromSlowmode);

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedTooManyRequestsException" /> with default message.
    /// </summary>
    /// <remarks>
    /// <para>Initializes a new instance of <see cref="GuildedTooManyRequestsException" /> with default message:</para>
    /// <note>Unacceptable. The request was unacceptable. Invalid/bad parameters?</note>
    /// </remarks>
    /// <returns>New message-only <see cref="GuildedTooManyRequestsException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="GuildedTooManyRequestsException(string)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, Exception)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, string, RestResponse, TimeSpan, bool)" />
    public GuildedTooManyRequestsException() : this("Too many requests has been sent from this client.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedTooManyRequestsException" /> with an <paramref name="inner">inner exception</paramref> explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    /// <returns>New message-only <see cref="GuildedTooManyRequestsException">bad request exception</see> instance</returns>
    /// <seealso cref="GuildedTooManyRequestsException" />
    /// <seealso cref="GuildedTooManyRequestsException()" />
    /// <seealso cref="GuildedTooManyRequestsException(string)" />
    /// <seealso cref="GuildedTooManyRequestsException(string, string, RestResponse, TimeSpan, bool)" />
    public GuildedTooManyRequestsException(string message, Exception inner) : base(message, inner) { }
    #endregion
}