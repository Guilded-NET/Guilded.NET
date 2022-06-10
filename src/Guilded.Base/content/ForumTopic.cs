using System;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a thread or a post in <see cref="Servers.ChannelType.Forums">a forum channel</see>.
/// </summary>
/// <remarks>
/// <para>Currently can only be found as a return value from forum thread creation methods.</para>
/// </remarks>
/// <seealso cref="Message" />
/// <seealso cref="Doc" />
/// <seealso cref="ListItem" />
public class ForumTopic : TitledContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Servers.Webhook">the webhook</see> that created <see cref="ForumTopic">the forum thread</see>.
    /// </summary>
    /// <value><see cref="Servers.Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="ForumTopic" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="TitledContent.UpdatedAt" />
    public Guid? CreatedByWebhook { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ForumTopic" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the forum thread</param>
    /// <param name="channelId">The identifier of the channel where the forum thread is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the forum thread is</param>
    /// <param name="title">The title of the forum thread</param>
    /// <param name="content">The text contents of the forum thread</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the forum thread</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the forum thread</param>
    /// <param name="createdAt">the date when the forum thread was created</param>
    /// <param name="updatedAt">the date when the forum thread was edited</param>
    /// <returns>New <see cref="ForumTopic" /> JSON instance</returns>
    /// <seealso cref="ForumTopic" />
    [JsonConstructor]
    public ForumTopic(
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