using System;
using Guilded.Base;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a reply in a <see cref="Topic">forum topic</see>.
/// </summary>
/// <seealso cref="Topic" />
/// <seealso cref="ForumChannel" />
/// <seealso cref="TitledContent" />
public class TopicComment : IModelHasId<uint>, ICreatableContent, IUpdatableContent
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="TopicComment">forum topic reply</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="TopicComment">forum topic reply</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="TopicId" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="CreatedBy" />
    public uint Id { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="ForumChannel">channel</see> where the <see cref="TopicComment">forum topic reply</see> exists.
    /// </summary>
    /// <value>The <see cref="ForumChannel">forum channel</see> identifier of the <see cref="TopicComment">forum topic reply</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="TopicId" />
    /// <seealso cref="Id" />
    /// <seealso cref="CreatedBy" />
    public Guid ChannelId { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Topic">forum topic</see> where the <see cref="TopicComment">forum topic reply</see> was created.
    /// </summary>
    /// <value>The <see cref="Topic">forum topic</see> identifier of the <see cref="TopicComment">forum topic reply</see></value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="Id" />
    /// <seealso cref="ChannelId" />
    /// <seealso cref="CreatedBy" />
    public uint TopicId { get; }

    /// <summary>
    /// Gets the text contents of the <see cref="TopicComment">forum topic reply</see>.
    /// </summary>
    /// <remarks>
    /// <para>The contents are formatted in Markdown. This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
    /// </remarks>
    /// <value>A multi-line text formatted in extended Guilded Markdown</value>
    /// <seealso cref="TopicComment" />
    /// <seealso cref="CreatedBy" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="Id" />
    /// <seealso cref="TopicId" />
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
    /// Initializes a new instance of <see cref="TopicComment" /> from the specified JSON properties.
    /// </summary>
    /// <returns>New <see cref="TopicComment" /> JSON instance</returns>
    /// <seealso cref="TopicComment" />
    [JsonConstructor]
    public TopicComment(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        uint forumTopicId,

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
        (Id, TopicId, ChannelId, Content, CreatedBy, CreatedAt, UpdatedAt) = (id, forumTopicId, channelId, content, createdBy, createdAt, updatedAt);
    #endregion
}