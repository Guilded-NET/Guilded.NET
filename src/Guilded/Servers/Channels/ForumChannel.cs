using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Servers;

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
    /// <param name="rootId">The identifier of the ancestor channel of the <see cref="ServerChannel">channel</see> that exist at the <see cref="Group">group</see> level</param>
    /// <param name="parentId">The identifier of the parent <see cref="ServerChannel">channel</see> of the <see cref="ServerChannel">channel</see></param>
    /// <param name="messageId">The identifier of the <see cref="Message">message</see> that hosts the thread</param>
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
        Guid? rootId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? parentId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Guid? messageId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? categoryId = null
    ) : base(id, groupId, serverId, type, name, createdBy, createdAt, updatedAt, archivedBy, archivedAt, topic, rootId, parentId, messageId, categoryId) { }
    #endregion

    #region Methods
    /// <inheritdoc cref="AbstractGuildedClient.GetTopicsAsync(Guid, uint?, DateTime?)" />
    /// <param name="limit">The limit of how many <see cref="Topic">topics</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched <see cref="Topic">topics</see></param>
    public Task<IList<TopicSummary>> GetTopicsAsync(uint? limit = null, DateTime? before = null) =>
        ParentClient.GetTopicsAsync(Id, limit, before);

    /// <inheritdoc cref="AbstractGuildedClient.GetTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to get</param>
    public Task<Topic> GetTopicAsync(uint topic) =>
        ParentClient.GetTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicAsync(Guid, string, string)" />
    /// <param name="title">The title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The content of the <see cref="Topic">forum topic</see></param>
    public Task<Topic> CreateTopicAsync(string title, string content) =>
        ParentClient.CreateTopicAsync(Id, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicAsync(Guid, uint, string, string)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to update</param>
    /// <param name="title">The new title of the <see cref="Topic">forum topic</see></param>
    /// <param name="content">The new contents of the <see cref="Topic">forum topic</see></param>
    public Task<Topic> UpdateTopicAsync(uint topic, string title, string content) =>
        ParentClient.UpdateTopicAsync(Id, topic, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to delete</param>
    public Task DeleteTopicAsync(uint topic) =>
        ParentClient.DeleteTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.PinTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to pin</param>
    public Task PinTopicAsync(uint topic) =>
        ParentClient.PinTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.UnpinTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unpin</param>
    public Task UnpinTopicAsync(uint topic) =>
        ParentClient.UnpinTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.LockTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to lock</param>
    public Task LockTopicAsync(uint topic) =>
        ParentClient.LockTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.UnlockTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unlock</param>
    public Task UnlockTopicAsync(uint topic) =>
        ParentClient.UnlockTopicAsync(Id, topic);

    /// <inheritdoc cref="AbstractGuildedClient.PinTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to pin</param>
    [Obsolete($"Use `{nameof(PinTopicAsync)}` instead")]
    public Task AddTopicPinAsync(uint topic) =>
        PinTopicAsync(topic);

    /// <inheritdoc cref="AbstractGuildedClient.UnpinTopicAsync(Guid, uint)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> to unpin</param>
    [Obsolete($"Use `{nameof(UnpinTopicAsync)}` instead")]
    public Task RemoveTopicPinAsync(uint topic) =>
        UnpinTopicAsync(topic);

    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> should be</param>
    /// <param name="content">The content of the <see cref="TopicComment">forum topic comment</see></param>
    public Task<TopicComment> CreateTopicCommentAsync(uint topic, string content) =>
        ParentClient.CreateTopicCommentAsync(Id, topic, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateTopicCommentAsync(Guid, uint, uint, string)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to update</param>
    /// <param name="content">The new acontent of the <see cref="TopicComment">forum topic comment</see></param>
    public Task<TopicComment> UpdateTopicCommentAsync(uint topic, uint topicComment, string content) =>
        ParentClient.UpdateTopicCommentAsync(Id, topic, topicComment, content);

    /// <inheritdoc cref="AbstractGuildedClient.CreateTopicCommentAsync(Guid, uint, string)" />
    /// <param name="topic">The identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic comment</see> is</param>
    /// <param name="topicComment">The identifier of the <see cref="TopicComment">forum topic comment</see> to delete</param>
    public Task DeleteTopicCommentAsync(uint topic, uint topicComment) =>
        ParentClient.DeleteTopicCommentAsync(Id, topic, topicComment);
    #endregion
}