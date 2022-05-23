using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Guilded.Base;

/// <summary>
/// Represents a base for Guilded models that require a <see cref="ParentClient">client</see>.
/// </summary>
/// <remarks>
/// <para>This allows having methods like <see cref="Content.Message.CreateMessageAsync(string)" />, where it requires to call the parent client's methods.</para>
/// </remarks>
/// <seealso cref="BaseGuildedClient" />
/// <seealso cref="BaseObject" />
public abstract class ClientObject : BaseObject
{
#nullable disable

    #region Properties
    /// <summary>
    /// Gets the parent client that adopts <see cref="ClientObject">this object</see>.
    /// </summary>
    /// <value>Client</value>
    /// <seealso cref="OnDeserialized" />
    /// <seealso cref="BaseGuildedClient" />
    [JsonIgnore]
    public BaseGuildedClient ParentClient { get; private set; }
    #endregion

    #region Methods
    /// <summary>
    /// Adds a <see cref="ParentClient">parent client</see> if the context contains it.
    /// </summary>
    /// <param name="context">The context given by the serializer</param>
    /// <seealso cref="ParentClient" />
    /// <seealso cref="BaseGuildedClient" />
    [OnDeserialized]
    internal void OnDeserialized(StreamingContext context)
    {
        if (context.Context is BaseGuildedClient client)
            ParentClient = client;
    }
    #endregion

#nullable restore
}