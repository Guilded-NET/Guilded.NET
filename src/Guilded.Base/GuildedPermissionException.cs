using System;
using RestSharp;

namespace Guilded.Base;

/// <summary>
/// Represents an exception thrown by Guilded API when the client has no permission to perform a request.
/// </summary>
/// <remarks>
/// <para>This is caused if you are trying to access or do an action that requires permissions, but you don't have them. This can only be fixed by getting said permissions by <see cref="T:Guilded.Servers.Server">a server</see> staff and is usually not controlled by you.</para>
/// </remarks>
/// <seealso cref="GuildedException" />
/// <seealso cref="GuildedPermissionException" />
/// <seealso cref="GuildedRequestException" />
/// <seealso cref="GuildedResourceException" />
[Serializable]
public sealed class GuildedPermissionException : GuildedException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedPermissionException" />.
    /// </summary>
    /// <param name="message">The message explaining the error</param>
    /// <returns>New message-only <see cref="GuildedPermissionException">permission exception</see> instance</returns>
    /// <seealso cref="GuildedPermissionException" />
    /// <seealso cref="GuildedPermissionException()" />
    /// <seealso cref="GuildedPermissionException(string, Exception)" />
    /// <seealso cref="GuildedPermissionException(string, string, RestResponse)" />
    public GuildedPermissionException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedPermissionException" /> with information from given parameters.
    /// </summary>
    /// <param name="code">The name of the error from Guilded API</param>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="response">The response that was received from Guilded API</param>
    /// <returns>New <see cref="GuildedPermissionException">permission exception</see> instance</returns>
    /// <seealso cref="GuildedPermissionException" />
    /// <seealso cref="GuildedPermissionException()" />
    /// <seealso cref="GuildedPermissionException(string)" />
    /// <seealso cref="GuildedPermissionException(string, Exception)" />
    public GuildedPermissionException(string code, string message, RestResponse response) : base(code, message, response) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedPermissionException" /> with default message.
    /// </summary>
    /// <remarks>
    /// <para>Initializes a new instance of <see cref="GuildedPermissionException" /> with default message:</para>
    /// <note>Forbidden. Guilded client is missing permissions.</note>
    /// </remarks>
    /// <returns>New message-only <see cref="GuildedPermissionException">permission exception</see> instance</returns>
    /// <seealso cref="GuildedPermissionException" />
    /// <seealso cref="GuildedPermissionException(string)" />
    /// <seealso cref="GuildedPermissionException(string, Exception)" />
    /// <seealso cref="GuildedPermissionException(string, string, RestResponse)" />
    public GuildedPermissionException() : this("Forbidden. Guilded client is missing permissions.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GuildedPermissionException" /> with an <paramref name="inner" /> exception explaining more.
    /// </summary>
    /// <param name="message">The description of the error from Guilded API</param>
    /// <param name="inner">Inner exception explaining more</param>
    /// <returns>New message-only <see cref="GuildedPermissionException">permission exception</see> instance</returns>
    /// <seealso cref="GuildedPermissionException" />
    /// <seealso cref="GuildedPermissionException()" />
    /// <seealso cref="GuildedPermissionException(string)" />
    /// <seealso cref="GuildedPermissionException(string, string, RestResponse)" />
    public GuildedPermissionException(string message, Exception inner) : base(message, inner) { }
    #endregion
}