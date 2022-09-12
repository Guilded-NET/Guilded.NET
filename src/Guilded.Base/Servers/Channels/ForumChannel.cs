using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base.Client;
using Guilded.Base.Content;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents a <see cref="ChannelType.List">list</see>-type channel.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="DocChannel" />
/// <seealso cref="ChatChannel" />
/// <seealso cref="VoiceChannel" />
/// <seealso cref="StreamChannel" />
/// <seealso cref="ListChannel" />
/// <seealso cref="MediaChannel" />
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="SchedulingChannel" />
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class ForumChannel : ServerChannel
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ForumChannel" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="ServerChannel">channel</see></param>
    /// <param name="groupId">The identifier of the parent group of the <see cref="ServerChannel">channel</see></param>
    /// <param name="serverId">The identifier of the parent <see cref="Server">server</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="type">The type of content <see cref="ServerChannel">channel</see> holds</param>
    /// <param name="name">The name of the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="ServerChannel">channel</see></param>
    /// <param name="createdAt">The date when the <see cref="ServerChannel">channel</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="ServerChannel">channel</see> was edited</param>
    /// <param name="archivedBy">The identifier of <see cref="User">user</see> that archived the <see cref="ServerChannel">channel</see></param>
    /// <param name="archivedAt">The date when the <see cref="ServerChannel">channel</see> was archived</param>
    /// <param name="topic">The topic describing what the <see cref="ServerChannel">channel</see> is about</param>
    /// <param name="parentId">The identifier of the parent <see cref="ServerChannel">channel</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="categoryId">The identifier of the parent category of the <see cref="ServerChannel">channel</see></param>
    /// <returns>New <see cref="ForumChannel" /> JSON instance</returns>
    /// <seealso cref="ForumChannel" />
    [JsonConstructor]
    public ForumChannel(
        [JsonProperty(Required = Required.Always)]
        Guid id,

        [JsonProperty(Required = Required.Always)]
        HashId groupId,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        ChannelType type,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? archivedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? archivedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? topic = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) : base(id, groupId, serverId, type, name, createdBy, createdAt, updatedAt, archivedBy, archivedAt, topic, parentId, categoryId) { }
    #endregion

    #region Methods
    /// <inheritdoc cref="BaseGuildedClient.GetTopicsAsync(Guid, uint?, DateTime?)" />
    /// <param name="limit">The limit of how many <see cref="Topic">topics</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="Topic">topics</see></param>
    public Task<IList<TopicSummary>> GetTopicsAsync(uint? limit = null, DateTime? before = null) =>
        ParentClient.GetTopicsAsync(Id, limit, before);

    /// <inheritdoc cref="BaseGuildedClient.GetTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get</param>
    public Task<Topic> GetTopicAsync(uint topic) =>
        ParentClient.GetTopicAsync(Id, topic);

    /// <inheritdoc cref="BaseGuildedClient.CreateTopicAsync(Guid, string, string)" />
    /// <param name="title">The title of <see cref="Topic">the forum topic</see></param>
    /// <param name="content">The content of <see cref="Topic">the forum topic</see></param>
    public Task<Topic> CreateTopicAsync(string title, string content) =>
        ParentClient.CreateTopicAsync(Id, title, content);

    /// <inheritdoc cref="BaseGuildedClient.UpdateTopicAsync(Guid, uint, string, string)" />
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to update</param>
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The new contents of the <see cref="Topic">forum topic</see></param>
    public Task<Topic> UpdateTopicAsync(uint topic, string title, string content) =>
        ParentClient.UpdateTopicAsync(Id, topic, title, content);

    /// <inheritdoc cref="BaseGuildedClient.DeleteTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to delete</param>
    public Task DeleteTopicAsync(uint topic) =>
        ParentClient.DeleteTopicAsync(Id, topic);

    /// <inheritdoc cref="BaseGuildedClient.AddTopicPinAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to pin</param>
    public Task AddTopicPinAsync(uint topic) =>
        ParentClient.AddTopicPinAsync(Id, topic);

    /// <inheritdoc cref="BaseGuildedClient.RemoveTopicPinAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">topic</see> to unpin</param>
    public Task RemoveTopicPinAsync(uint topic) =>
        ParentClient.RemoveTopicPinAsync(Id, topic);
    #endregion
}