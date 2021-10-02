using System;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// A field in an embed.
    /// </summary>
    /// <remarks>
    /// <para>Displays a field with its own description/value and title/name. Fields can be both inline and blocks. They are only inline if <see cref="Inline"/> parameter is <see langword="true"/>.</para>
    /// </remarks>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedField : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The title of the embed.
        /// </summary>
        /// <remarks>
        /// <para>The name or the title of the field that is displayed above <see cref="Value"/>.</para>
        /// <para>The provided Markdown is ignored.</para>
        /// </remarks>
        /// <value>Title</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The description text of the field.
        /// </summary>
        /// <remarks>
        /// <para>The description or the value of the field.</para>
        /// <para>This allows any given Markdown.</para>
        /// </remarks>
        /// <value>Description</value>
        [JsonProperty(Required = Required.Always)]
        public string Value
        {
            get; set;
        }
        /// <summary>
        /// Whether the field should be inline with other fields.
        /// </summary>
        /// <remarks>
        /// <para>Determines whether the field will be next to other fields or below/above them.</para>
        /// <para>If the value is <see langword="true"/>, the field will be displayed next to other fields</para>
        /// </remarks>
        /// <value>Boolean</value>
        public bool Inline
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EmbedField"/>, which is optionally inline.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new field with the name <paramref name="name"/> and a value <paramref name="value"/>.</para>
        /// </remarks>
        /// <param name="name">The title of the embed</param>
        /// <param name="value">The description text of the field</param>
        /// <param name="inline">Whether the field should be inline with other fields</param>
        public EmbedField(string name, string value, bool inline = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NullReferenceException($"Argument {nameof(name)} cannot be null, empty or whitespace.");
            else if (string.IsNullOrWhiteSpace(value))
                throw new NullReferenceException($"Argument {nameof(value)} cannot be null, empty or whitespace.");

            (Name, Value, Inline) = (name, value, inline);
        }
        #endregion
    }
}