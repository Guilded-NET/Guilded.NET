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
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="MediaChannel" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="ChatChannel" />
/// <seealso cref="VoiceChannel" />
/// <seealso cref="StreamChannel" />
/// <seealso cref="ListChannel" />
/// <seealso cref="SchedulingChannel" />
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class DocChannel : ServerChannel
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="DocChannel" /> from the specified JSON properties.
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
    /// <returns>New <see cref="DocChannel" /> JSON instance</returns>
    /// <seealso cref="DocChannel" />
    [JsonConstructor]
    public DocChannel(
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
    /// <inheritdoc cref="AbstractGuildedClient.GetDocsAsync(Guid, uint?, DateTime?)" />
    /// <param name="limit">The limit of how many <see cref="Doc">documents</see> to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of the fetched <see cref="Doc">documents</see></param>
    public Task<IList<Doc>> GetDocsAsync(uint? limit = null, DateTime? before = null) =>
        ParentClient.GetDocsAsync(Id, limit, before);

    /// <inheritdoc cref="AbstractGuildedClient.GetDocAsync(Guid, uint)" />
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to get</param>
    public Task<Doc> GetDocAsync(uint doc) =>
        ParentClient.GetDocAsync(Id, doc);

    /// <inheritdoc cref="AbstractGuildedClient.CreateDocAsync(Guid, string, string)" />
    /// <param name="title">The title of the <see cref="Doc">document</see></param>
    /// <param name="content">The Markdown content of the <see cref="Doc">document</see></param>
    public Task<Doc> CreateDocAsync(string title, string content) =>
        ParentClient.CreateDocAsync(Id, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.UpdateDocAsync(Guid, uint, string, string)" />
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to update/edit</param>
    /// <param name="title">The new title of this <see cref="Doc">document</see></param>
    /// <param name="content">The new Markdown content of this <see cref="Doc">document</see></param>
    public Task<Doc> UpdateDocAsync(uint doc, string title, string content) =>
        ParentClient.UpdateDocAsync(Id, doc, title, content);

    /// <inheritdoc cref="AbstractGuildedClient.DeleteDocAsync(Guid, uint)" />
    /// <param name="doc">The identifier of the <see cref="Doc">document</see> to delete</param>
    public Task DeleteDocAsync(uint doc) =>
        ParentClient.DeleteDocAsync(Id, doc);
    #endregion
}