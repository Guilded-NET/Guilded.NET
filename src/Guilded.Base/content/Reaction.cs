using System;

using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a <see cref="ChannelContent{T, S}">content</see> reaction.
/// </summary>
/// <seealso cref="Message"/>
/// <seealso cref="Doc"/>
/// <seealso cref="ForumThread"/>
public class Reaction : ClientObject, IWebhookCreatable, ICreatableContent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the emote reacted with.
    /// </summary>
    /// <value>Emote ID</value>
    public uint Id { get; }
    /// <summary>
    /// Gets the identifier of the server where the content is.
    /// </summary>
    /// <remarks>
    /// <para>As some of the content are bound to servers and some can be global, the identifier of the server may be <see langword="null" />.</para>
    /// </remarks>
    /// <value>Server ID?</value>
    public HashId? ServerId { get; }
    /// <summary>
    /// Gets the date of when the reaction was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }
    /// <summary>
    /// Gets the identifier of the user that created the reaction.
    /// </summary>
    /// <remarks>
    /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>User ID</value>
    public HashId CreatedBy { get; }
    /// <summary>
    /// Gets the identifier of the webhook that created the reaction.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the webhook that posted created this reaction.</para>
    /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
    /// </remarks>
    /// <value>Webhook ID?</value>
    public Guid? CreatedByWebhook { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Reaction"/> with provided details.
    /// </summary>
    /// <param name="id">The identifier of the emote reacted with</param>
    /// <param name="serverId">The identifier of the server where the reaction is</param>
    /// <param name="createdBy">The identifier of the user creator of the reaction</param>
    /// <param name="createdByWebhookId">The identifier of the webhook creator of the reaction</param>
    /// <param name="createdAt">The date of when the reaction was created</param>
    [JsonConstructor]
    public Reaction(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty]
        HashId? serverId,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty]
        Guid? createdByWebhookId,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt
    ) =>
        (Id, ServerId, CreatedAt, CreatedBy, CreatedByWebhook) = (id, serverId, createdAt, createdBy, createdByWebhookId);
    #endregion

    #region Overrides
    /// <summary>
    /// Returns whether this and <paramref name="obj"/> are equal to each other.
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