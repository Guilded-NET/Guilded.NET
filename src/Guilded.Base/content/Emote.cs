using System;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents an emoticon on Guilded. Either placed in <see cref="Message">some kind of content</see> or as <see cref="Reaction">a reaction</see>.
/// </summary>
public class Emote : BaseModel
{
    #region Properties
    /// <summary>
    /// Gets the identifier of <see cref="Emote">the emote</see>.
    /// </summary>
    /// <value><see cref="Id">Emote ID</see></value>
    /// <seealso cref="Emote" />
    /// <seealso cref="Name" />
    /// <seealso cref="Url" />
    public uint Id { get; }

    /// <summary>
    /// Gets the name of <see cref="Emote">the emote</see>.
    /// </summary>
    /// <remarks>
    /// <para>This will never hold any whitespace.</para>
    /// </remarks>
    /// <value>No-Whitespace Name</value>
    /// <seealso cref="Emote" />
    /// <seealso cref="Id" />
    /// <seealso cref="Url" />
    public string Name { get; }

    /// <summary>
    /// Gets the URL to <see cref="Emote">the emote's</see> image.
    /// </summary>
    /// <value>Media URL</value>
    /// <seealso cref="Emote" />
    /// <seealso cref="Id" />
    /// <seealso cref="Name" />
    public Uri Url { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Emote" /> from the specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of <see cref="Emote">the emote</see></param>
    /// <param name="name">The name of <see cref="Emote">the emote</see></param>
    /// <param name="url">The URL to <see cref="Emote">the emote's</see> image</param>
    /// <returns>New <see cref="Emote" /> JSON instance</returns>
    /// <seealso cref="Emote" />
    [JsonConstructor]
    public Emote(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        Uri url
    ) =>
        (Id, Name, Url) = (id, name, url);
    #endregion

    #region Methods
    /// <summary>
    /// Gets the string representation of <see cref="Emote">the emote</see>.
    /// </summary>
    /// <remarks>
    /// <para>Currently, this returns <c>:</c> followed by the name of the emote and <c>:</c>.</para>
    /// </remarks>
    /// <returns><see cref="Emote" /> as a string</returns>
    public override string ToString() =>
        $":{Name}:";
    #endregion
}