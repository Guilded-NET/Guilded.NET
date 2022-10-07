using System;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// Represents an exception thrown by Guilded API when a request has invalid path.
/// </summary>
/// <remarks>
/// <para>This has these leading causes:</para>
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
/// <seealso cref="GuildedException" />
/// <seealso cref="GuildedResourceException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedRequestException" />
[Serializable]
public sealed class GuildedResourceException : GuildedException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedResourceException" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedResourceException">conflicting resource exception</see> instance</returns>
    /// <seealso cref="GuildedResourceException" />
    /// <seealso cref="GuildedResourceException()" />
    /// <seealso cref="GuildedResourceException(string, Exception)" />
    /// <seealso cref="GuildedResourceException(string, string, RestResponse)" />
    public GuildedResourceException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedResourceException" /> with information from given parameters.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param><returns>New message-only <see cref="GuildedResourceException">conflicting resource exception</see> instance</returns>
    /// <seealso cref="GuildedResourceException" />
    /// <seealso cref="GuildedResourceException()" />
    /// <seealso cref="GuildedResourceException(string)" />
    /// <seealso cref="GuildedResourceException(string, Exception)" />
    public GuildedResourceException(string code, string message, RestResponse response) : base(code, message, response) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedResourceException" /> with default message.
    /// </summary>
    /// <remarks>
    /// <para>Initializes a new instance of <see cref="GuildedResourceException" /> with default message:</para>
    /// <note>Not found. Given item has not been found.</note>
    /// </remarks><returns>New message-only <see cref="GuildedResourceException">conflicting resource exception</see> instance</returns>
    /// <seealso cref="GuildedResourceException" />
    /// <seealso cref="GuildedResourceException(string)" />
    /// <seealso cref="GuildedResourceException(string, Exception)" />
    /// <seealso cref="GuildedResourceException(string, string, RestResponse)" />
    public GuildedResourceException() : this("Not found. Given item has not been found.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedResourceException" /> with an <paramref name="inner" /> exception explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param><returns>New message-only <see cref="GuildedResourceException">conflicting resource exception</see> instance</returns>
    /// <seealso cref="GuildedResourceException" />
    /// <seealso cref="GuildedResourceException()" />
    /// <seealso cref="GuildedResourceException(string)" />
    /// <seealso cref="GuildedResourceException(string, string, RestResponse)" />
    public GuildedResourceException(string message, Exception inner) : base(message, inner) { }
    #endregion
}