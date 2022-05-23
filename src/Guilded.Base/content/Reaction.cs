using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a <see cref="ChannelContent{T, S}">content</see> reaction.
/// </summary>
/// <seealso cref="Message" />
/// <seealso cref="Doc" />
/// <seealso cref="ForumThread" />
public class Reaction : ClientObject, IWebhookCreatable, ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the emote that was reacted with.
    /// </summary>
    /// <value>Emote ID</value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="ServerId" />
    public uint Id { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Reaction">the reaction</see> where the content is.
    /// </summary>
    /// <remarks>
    /// <para>As some of the content are bound to servers and some can be global, the identifier of <see cref="Reaction">the reaction</see> may be <see langword="null" />.</para>
    /// </remarks>
    /// <value>Server ID?</value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="Id" />
    public HashId? ServerId { get; }

    /// <summary>
    /// Gets the date when <see cref="Reaction">the reaction</see> was created.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedByWebhook" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created <see cref="Reaction">the reaction</see>.
    /// </summary>
    /// <remarks>
    /// <para>If <see cref="Webhook">a webhook</see> created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedByWebhook" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the identifier of <see cref="Webhook">the webhook</see> that created <see cref="Reaction">the reaction</see>.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="Reaction" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    public Guid? CreatedByWebhook { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Reaction" /> with provided details.
    /// </summary>
    /// <param name="id">The identifier of the emote reacted with</param>
    /// <param name="serverId">The identifier of the server where the reaction is</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the reaction</param>
    /// <param name="createdByWebhookId">The identifier of <see cref="Servers.Webhook">the webhook</see> creator of the reaction</param>
    /// <param name="createdAt">the date when the reaction was created</param>
    /// <returns>New <see cref="Reaction" /> JSON instance</returns>
    /// <seealso cref="Reaction" />
    [JsonConstructor]
    public Reaction(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? serverId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null
    ) =>
        (Id, ServerId, CreatedAt, CreatedBy, CreatedByWebhook) = (id, serverId, createdAt, createdBy, createdByWebhookId);
    #endregion

    #region Overrides
    /// <summary>
    /// Returns whether this and <paramref name="obj" /> are equal to each other.
    /// </summary>
    /// <param name="obj">Another object to compare</param>
    /// <returns>Are equal</returns>
    public override bool Equals(object? obj) =>
        obj is Reaction reaction && Id == reaction.Id;

    /// <summary>
    /// Gets a hashcode of this object.
    /// </summary>
    /// <returns>HashCode</returns>
    public override int GetHashCode() =>
        HashCode.Combine(Id, CreatedAt);
    #endregion
}