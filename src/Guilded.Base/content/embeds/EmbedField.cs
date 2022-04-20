using System;
using Newtonsoft.Json;

namespace Guilded.Base.Embeds;

/// <summary>
/// Represents a field with its own <see cref="Name">title</see> in an <see cref="Embed">embed</see>.
/// </summary>
/// <seealso cref="EmbedFooter"/>
/// <seealso cref="EmbedAuthor"/>
/// <seealso cref="EmbedMedia"/>
public class EmbedField : BaseObject
{
    #region JSON properties
    /// <summary>
    /// Gets the title of an <see cref="Embed">embed's</see> field.
    /// </summary>
    /// <remarks>
    /// <para>The provided Markdown is ignored.</para>
    /// </remarks>
    /// <value>Title</value>
    public string Name { get; set; }
    /// <summary>
    /// Gets the text contents of an <see cref="Embed">embed's</see> field.
    /// </summary>
    /// <remarks>
    /// <para>This allows any given Markdown.</para>
    /// </remarks>
    /// <value>Markdown string</value>
    public string Value { get; set; }
    /// <summary>
    /// Gets whether the field should be inline with other fields.
    /// </summary>
    /// <remarks>
    /// <para>If the value is <see langword="true"/>, the field will be displayed next to other fields</para>
    /// </remarks>
    /// <value>Field is inline</value>
    public bool Inline { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="EmbedField"/>, which is optionally inline.
    /// </summary>
    /// <param name="name">The title of an <see cref="Embed">embed's</see> field</param>
    /// <param name="value">The text contents of an <see cref="Embed">embed's</see> field</param>
    /// <param name="inline">Whether the field should be inline with other fields</param>
    /// <exception cref="ArgumentNullException">Either <paramref name="name"/> or <paramref name="value"/> are <see langword="null"/></exception>
    [JsonConstructor]
    public EmbedField(
        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        string value,

        bool inline = false
    )
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        else if (value is null)
            throw new ArgumentNullException(nameof(value));

        (Name, Value, Inline) = (name, value, inline);
    }
    #endregion
}