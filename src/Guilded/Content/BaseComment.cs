using System;
using Guilded.Base;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a reply in any kind of <see cref="ChannelContent{TId, TServer}">channel content</see>.
/// </summary>
/// <seealso cref="DocComment" />
/// <seealso cref="TopicComment" />
/// <seealso cref="AnnouncementComment" />
/// <seealso cref="CalendarEventComment" />
/// <seealso cref="TitledContent{T}" />
public abstract class BaseComment : ContentModel, IModelHasId<uint>, ICreatableContent, IChannelBased, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="TopicComment">forum topic reply</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="TopicComment">forum topic reply</see></value>
    /// <seealso cref="BaseComment" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="CreatedBy" />
    public uint Id { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="ForumChannel">forum channel</see> where the <see cref="TopicComment">forum topic reply</see> was created.
    /// </summary>
    /// <value>The identifier of the <see cref="ForumChannel">forum channel</see> where the <see cref="TopicComment">forum topic reply</see> was created</value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="Id" />
    /// <seealso cref="CreatedBy" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the full-Markdown text contents of the <see cref="TopicComment">forum topic reply</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>A multi-line text formatted in extended Guilded Markdown</value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="Mentions" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="Id" />
    public string Content { get; }

    /// <summary>
    /// Gets the identifier of <see cref="User">user</see> that created the <see cref="TopicComment">forum topic</see>.
    /// </summary>
    /// <remarks>
    /// <para>If webhook or bot created this reaction, the value of this property will be <c>Ann6LewA</c>.</para>
    /// </remarks>
    /// <value>The <see cref="User">creator</see> identifier of the <see cref="TopicComment">forum topic</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="Content" />
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="TopicComment">forum topic reply</see> was created.
    /// </summary>
    /// <value>The creation date of the <see cref="TopicComment">forum topic reply</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="UpdatedAt" />
    /// <seealso cref="Content" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date when the <see cref="TopicComment">forum topic comment</see> was edited.
    /// </summary>
    /// <remarks>
    /// <para>Only returns the most recent update.</para>
    /// </remarks>
    /// <value>The edit date of the <see cref="TopicComment">forum topic comment</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="Content" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="BaseComment" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="BaseComment">content comment</see></param>
    /// <param name="channelId">The identifier of the <see cref="ServerChannel">channel</see> where the <see cref="BaseComment">content comment</see> was created</param>
    /// <param name="content">The full-Markdown text contents of the <see cref="BaseComment">content comment</see></param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the <see cref="BaseComment">content comment</see></param>
    /// <param name="createdAt">The date when the <see cref="BaseComment">content comment</see> was created</param>
    /// <param name="updatedAt">The date when the <see cref="BaseComment">content comment</see> was edited</param>
    /// <returns>New <see cref="BaseComment" /> JSON instance</returns>
    /// <seealso cref="BaseComment" />
    [JsonConstructor]
    public BaseComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        Guid channelId,

        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (Id, ChannelId, Content, CreatedBy, CreatedAt, UpdatedAt) = (id, channelId, content, createdBy, createdAt, updatedAt);
    #endregion
}