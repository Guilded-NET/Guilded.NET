using System;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents a <see cref="ChannelType.List">list</see>-type channel.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="CalendarChannel" />
/// <seealso cref="AnnouncementChannel" />
/// <seealso cref="MediaChannel" />
/// <seealso cref="DocChannel" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="ChatChannel" />
/// <seealso cref="VoiceChannel" />
/// <seealso cref="StreamChannel" />
/// <seealso cref="ListChannel" />
/// <seealso cref="ChannelType" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
public class SchedulingChannel : ServerChannel
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="SchedulingChannel" /> from the specified JSON properties.
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
    /// <returns>New <see cref="SchedulingChannel" /> JSON instance</returns>
    /// <seealso cref="SchedulingChannel" />
    [JsonConstructor]
    public SchedulingChannel(
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
}