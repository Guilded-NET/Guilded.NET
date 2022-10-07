using System;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// Represents an exception thrown by Guilded API.
/// </summary>
/// <remarks>
/// <para>This should not usually be thrown and instead, children types of <see cref="GuildedException" /> should be thrown instead. If you see it get thrown, this is most likely due to internal server error or some kind of other unknown exception.</para>
/// <para>If you get internal server error, make sure you are doing everything right as documented or noted.</para>
/// </remarks>
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedRequestException" />
/// <seealso cref="GuildedResourceException" />
/// <seealso cref="GuildedRequestException" />
[Serializable]
public class GuildedException : Exception
{
    #region Properties
    /// <summary>
    /// Gets the code name of Guilded error.
    /// </summary>
    /// <value>Code name</value>
    public string? Code { get; }

    /// <summary>
    /// Gets the response that was received from Guilded API.
    /// </summary>
    /// <value>REST Response</value>
    public RestResponse? Response { get; }

    /// <summary>
    /// Gets the HTTP status that was found in the response.
    /// </summary>
    /// <value>HTTP status</value>
    public HttpStatusCode? StatusCode => Response?.StatusCode;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedException" /> with only a <paramref name="message" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedException">Guilded exception</see> instance</returns>
    /// <seealso cref="GuildedException" />
    /// <seealso cref="GuildedException()" />
    /// <seealso cref="GuildedException(string, Exception)" />
    /// <seealso cref="GuildedException(string, string, RestResponse)" />
    public GuildedException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedException" /> from a <paramref name="response" />.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param>
    /// <returns>New <see cref="GuildedException">Guilded exception</see> instance</returns>
    /// <seealso cref="GuildedException" />
    /// <seealso cref="GuildedException()" />
    /// <seealso cref="GuildedException(string)" />
    /// <seealso cref="GuildedException(string, Exception)" />
    public GuildedException(string code, string message, RestResponse? response) : this(message) =>
        (Code, Response) = (code, response);

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedException" /> with a default message.
    /// </summary>
    /// <remarks>
    /// <para>This is the message that will be used:</para>
    /// <code>Guilded exception was thrown.</code>
    /// </remarks>
    /// <returns>New message-only <see cref="GuildedException">Guilded exception</see> instance</returns>
    /// <seealso cref="GuildedException" />
    /// <seealso cref="GuildedException(string)" />
    /// <seealso cref="GuildedException(string, Exception)" />
    /// <seealso cref="GuildedException(string, string, RestResponse)" />
    public GuildedException() : this("Guilded exception was thrown.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedException" /> with an <paramref name="inner" /> exception explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    /// <returns>New message-only <see cref="GuildedException">Guilded exception</see> instance</returns>
    /// <seealso cref="GuildedException" />
    /// <seealso cref="GuildedException()" />
    /// <seealso cref="GuildedException(string)" />
    /// <seealso cref="GuildedException(string, string, RestResponse)" />
    public GuildedException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedException" /> with serialization information.
    /// </summary>
    /// <param name="info">The information about serialization that errored</param>
    /// <param name="context">The streaming context of the serialization</param>
    /// <returns>New message-only <see cref="GuildedException">Guilded exception</see> instance</returns>
    /// <seealso cref="GuildedException" />
    /// <seealso cref="GuildedException()" />
    /// <seealso cref="GuildedException(string)" />
    /// <seealso cref="GuildedException(string, Exception)" />
    /// <seealso cref="GuildedException(string, string, RestResponse)" />
    protected GuildedException(
        SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    #endregion

    #region Methods
    /// <summary>
    /// Returns string representation of the exception thrown.
    /// </summary>
    /// <returns><see cref="GuildedException" /> as string</returns>
    public override string ToString() =>
        $"Guilded API has thrown an error:\n[{Code}:{StatusCode}]: {Message}\n{StackTrace}";
    #endregion
}