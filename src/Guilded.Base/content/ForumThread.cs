using System;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a thread or a post in a forum channel.
/// </summary>
/// <remarks>
/// <para>Currently can only be found as a return value from forum thread creation methods.</para>
/// </remarks>
/// <seealso cref="Message"/>
/// <seealso cref="Doc"/>
/// <seealso cref="ListItemBase{T}"/>
public class ForumThread : TitledContent
{
    #region JSON properties
    /// <summary>
    /// Gets the identifier of the webhook that created the forum thread.
    /// </summary>
    /// <remarks>
    /// <note type="note">Currently, only chat messages can be created by Webhooks.</note>
    /// </remarks>
    /// <value>Webhook ID?</value>
    public Guid? CreatedByWebhook { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ForumThread"/> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the forum thread</param>
    /// <param name="channelId">The identifier of the channel where the forum thread is</param>
    /// <param name="serverId">The identifier of the server where the forum thread is</param>
    /// <param name="title">The title of the forum thread</param>
    /// <param name="content">The text contents of the forum thread</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the forum thread</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the forum thread</param>
    /// <param name="createdAt">the date when the forum thread was created</param>
    /// <param name="updatedAt">the date when the forum thread was edited</param>
    [JsonConstructor]
    public ForumThread(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string title,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, serverId, title, content, createdBy, createdAt, updatedAt) =>
        CreatedByWebhook = createdByWebhookId;
    #endregion
}