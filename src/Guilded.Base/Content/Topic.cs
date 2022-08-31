using System;
using System.Threading.Tasks;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a summary of a <see cref="Topic">topic</see> in a <see cref="ChannelType.Forums">forum channel</see>.
/// </summary>
/// <remarks>
/// <para>This summary does not contain the <see cref="Topic.Content">content</see> of the <see cref="Topic">topic</see> and only the <see cref="TitledContent.Title">title</see></para>
/// </remarks>
/// <seealso cref="Topic" />
/// <seealso cref="Doc" />
/// <seealso cref="Message" />
/// <seealso cref="ListItem" />
/// <seealso cref="CalendarEvent" />
public class TopicSummary : TitledContent, IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Webhook">the webhook</see> that created <see cref="TopicSummary">the forum thread</see>.
    /// </summary>
    /// <value><see cref="Webhook.Id">Webhook ID</see>?</value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedBy" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="TitledContent.UpdatedAt" />
    public Guid? CreatedByWebhook { get; }

    /// <summary>
    /// Gets the date when the <see cref="TopicSummary">topic</see> was bumped.
    /// </summary>
    /// <value>Date</value>
    /// <seealso cref="TopicSummary" />
    /// <seealso cref="ChannelContent{TId, TServer}.CreatedAt" />
    /// <seealso cref="TitledContent.UpdatedAt" />
    public DateTime BumpedAt { get; }

    /// <inheritdoc />
    public Mentions? Mentions { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="TopicSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the forum thread</param>
    /// <param name="channelId">The identifier of the channel where the forum thread is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the forum thread is</param>
    /// <param name="title">The title of the forum thread</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the forum thread</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the forum thread</param>
    /// <param name="createdAt">The date when the forum thread was created</param>
    /// <param name="bumpedAt">The date when the <see cref="TopicSummary">topic</see> was bumped</param>
    /// <param name="updatedAt">The date when the forum thread was edited</param>
    /// <param name="mentions">The <see cref="Mentions">mentions</see> found in <see cref="Message.Content">the content</see></param>
    /// <returns>New <see cref="TopicSummary" /> JSON instance</returns>
    /// <seealso cref="TopicSummary" />
    [JsonConstructor]
    public TopicSummary(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string title,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(Required = Required.Always)]
        DateTime bumpedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null
    ) : base(id, channelId, serverId, title, createdBy, createdAt, updatedAt) =>
        (Mentions, BumpedAt, CreatedByWebhook) = (mentions, bumpedAt, createdByWebhookId);
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.UpdateTopicAsync(Guid, uint, string, string)" />
    /// <param name="title">The new title of the <see cref="Topic">topic</see></param>
    /// <param name="content">The Markdown content of the <see cref="Topic">topic</see></param>
    public Task<Topic> UpdateAsync(string title, string content) =>
        ParentClient.UpdateTopicAsync(ChannelId, Id, title, content);

    /// <inheritdoc cref="BaseGuildedClient.DeleteTopicAsync(Guid, uint)" />
    public Task DeleteAsync() =>
        ParentClient.DeleteTopicAsync(ChannelId, Id);
    #endregion
}

/// <summary>
/// Represents the full information of a topic/post/thread in a <see cref="ChannelType.Forums">forum channel</see>.
/// </summary>
/// <seealso cref="TopicSummary" />
/// <seealso cref="Doc" />
/// <seealso cref="Message" />
/// <seealso cref="ListItem" />
/// <seealso cref="CalendarEvent" />
public class Topic : TopicSummary
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the <see cref="Topic">topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    /// <seealso cref="Topic" />
    /// <seealso cref="TitledContent.Title" />
    public string Content { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Topic" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the forum thread</param>
    /// <param name="channelId">The identifier of the channel where the forum thread is</param>
    /// <param name="serverId">The identifier of <see cref="Server">the server</see> where the forum thread is</param>
    /// <param name="title">The title of the forum thread</param>
    /// <param name="content">The text contents of the <see cref="Topic">topic</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the forum thread</param>
    /// <param name="createdByWebhookId">The identifier of the webhook that created the forum thread</param>
    /// <param name="createdAt">The date when the forum thread was created</param>
    /// <param name="bumpedAt">The date when the <see cref="Topic">topic</see> was bumped</param>
    /// <param name="updatedAt">The date when the forum thread was edited</param>
    /// <returns>New <see cref="Topic" /> JSON instance</returns>
    /// <seealso cref="Topic" />
    [JsonConstructor]
    public Topic(
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

        [JsonProperty(Required = Required.Always)]
        DateTime bumpedAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? createdByWebhookId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) : base(id, channelId, serverId, title, createdBy, createdAt, bumpedAt, createdByWebhookId, updatedAt) =>
        Content = content;
    #endregion
}

