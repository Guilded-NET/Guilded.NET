using System;
using Guilded.Base;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents the summary of the <see cref="ItemSummary">list item's</see> note.
/// </summary>
public class ItemNoteSummary : ICreatableContent, IUpdatableContent
{
    #region Properties Who, when
    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> who created <see cref="ItemNote">the note</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ItemNote">the note</see> was created.
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// The identifier of the <see cref="User">user</see> who updated <see cref="ItemNote">the note</see>.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when <see cref="ItemNote">the note</see> was edited.
    /// </summary>
    /// <value>Date</value>
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemNoteSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> that created the note</param>
    /// <param name="createdAt">the date when the note was created</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> that updated the note</param>
    /// <param name="updatedAt">the date when the note was edited</param>
    /// <returns>New <see cref="ItemNoteSummary" /> JSON instance</returns>
    /// <seealso cref="ItemNoteSummary" />
    [JsonConstructor]
    public ItemNoteSummary(
        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) = (createdAt, createdBy, updatedAt, updatedBy);
    #endregion
}

/// <summary>
/// Represents the full information about the <see cref="Item">list item's</see> note.
/// </summary>
public class ItemNote : ItemNoteSummary, IContentMarkdown
{
    #region Properties
    /// <summary>
    /// Gets the contents of <see cref="ItemNote">the note</see> in the <see cref="Item">list item</see>.
    /// </summary>
    /// <value>Markdown string</value>
    public string Content { get; set; }

    /// <summary>
    /// Gets <see cref="Content.Mentions">the mentions</see> found in the <see cref="Content">content</see>.
    /// </summary>
    /// <value><see cref="Content.Mentions" />?</value>
    public Mentions? Mentions { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemNote" /> from the specified JSON properties.
    /// </summary>
    /// <param name="content">The contents of the note</param>
    /// <param name="createdBy">The identifier of <see cref="User">user</see> creator of the list item's note</param>
    /// <param name="createdAt">the date when the list item's note was created</param>
    /// <param name="updatedBy">The identifier of <see cref="User">user</see> that updated the note</param>
    /// <param name="updatedAt">the date when the note was edited</param>
    /// <param name="mentions"><see cref="Mentions">The mentions</see> found in <see cref="Message">the content</see></param>
    /// <returns>New <see cref="ItemNote" /> JSON instance</returns>
    /// <seealso cref="ItemNote" />
    [JsonConstructor]
    public ItemNote(
        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        HashId? updatedBy = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Mentions? mentions = null
    ) : base(createdBy, createdAt, updatedBy, updatedAt) =>
        (Content, Mentions) = (content, mentions);
    #endregion
}