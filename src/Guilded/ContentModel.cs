using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Guilded.Base.Embeds;
using Guilded.Client;
using Newtonsoft.Json;

namespace Guilded;

/// <summary>
/// Represents an object that uses parent <see cref="AbstractGuildedClient">Guilded client</see> for its methods.
/// </summary>
public interface IHasParentClient
{
    /// <summary>
    /// Gets the parent <see cref="AbstractGuildedClient">client</see> that adopts <see cref="ContentModel">this object</see>.
    /// </summary>
    /// <value>Client</value>
    /// <seealso cref="ContentModel.OnDeserialized" />
    /// <seealso cref="AbstractGuildedClient" />
    AbstractGuildedClient ParentClient { get; }
}

/// <summary>
/// Represents a base for Guilded models that require a <see cref="ParentClient">client</see>.
/// </summary>
/// <remarks>
/// <para>This allows having methods like <see cref="Content.Message.CreateMessageAsync(string, IList{Embed}, IList{Guid}, bool, bool)" />, where it requires to call the parent client's methods.</para>
/// </remarks>
/// <seealso cref="AbstractGuildedClient" />
public abstract class ContentModel : IHasParentClient
{
#nullable disable

    #region Properties
    /// <inheritdoc />
    [JsonIgnore]
    public AbstractGuildedClient ParentClient { get; protected set; }
    #endregion

    #region Methods
    /// <summary>
    /// Adds a <see cref="ParentClient">parent client</see> if the context contains it.
    /// </summary>
    /// <param name="context">The context given by the serializer</param>
    /// <seealso cref="ParentClient" />
    /// <seealso cref="AbstractGuildedClient" />
    [OnDeserialized]
    internal void OnDeserialized(StreamingContext context)
    {
        if (context.Context is AbstractGuildedClient client)
            ParentClient = client;
    }
    #endregion

#nullable restore
}