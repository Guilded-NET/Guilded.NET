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
    /// Gets the identifier of the <see cref="User">user</see> who created the <see cref="ItemNote">note</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="User">user</see> who created the <see cref="ItemNote">note</see></value>
    public HashId CreatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="ItemNote">note</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="ItemNote">note</see> was created</value>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="User">user</see> who updated the <see cref="ItemNote">note</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="User">user</see> who updated the <see cref="ItemNote">note</see></value>
    public HashId? UpdatedBy { get; }

    /// <summary>
    /// Gets the date when the <see cref="ItemNote">note</see> was edited.
    /// </summary>
    /// <value>The date when the <see cref="ItemNote">note</see> was edited</value>
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemNoteSummary" /> from the specified JSON properties.
    /// </summary>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> who created the <see cref="ItemNote">note</see></param>
    /// <param name="createdAt">The date when the <see cref="ItemNote">note</see> was created</param>
    /// <param name="updatedBy">The identifier of the <see cref="User">user</see> who updated the <see cref="ItemNote">note</see></param>
    /// <param name="updatedAt">The date when the <see cref="ItemNote">note</see> was edited</param>
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
    /// Gets the contents of the <see cref="ItemNote">note</see> in the <see cref="Item">list item</see>.
    /// </summary>
    /// <value>The contents of the <see cref="ItemNote">note</see> in the <see cref="Item">list item</see></value>
    public string Content { get; set; }

    /// <summary>
    /// Gets the <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see>.
    /// </summary>
    /// <value>The <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see></value>
    public Mentions? Mentions { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="ItemNote" /> from the specified JSON properties.
    /// </summary>
    /// <param name="content">The contents of the <see cref="ItemNote">note</see> in the <see cref="Item">list item</see></param>
    /// <param name="createdBy">The identifier of the <see cref="User">user</see> who created the <see cref="ItemNote">note</see></param>
    /// <param name="createdAt">The date when the <see cref="ItemNote">note</see> was created</param>
    /// <param name="updatedBy">The identifier of the <see cref="User">user</see> who updated the <see cref="ItemNote">note</see></param>
    /// <param name="updatedAt">The date when the <see cref="ItemNote">note</see> was edited</param>
    /// <param name="mentions">The <see cref="Content.Mentions">mentions</see> found in the <see cref="Content">content</see></param>
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