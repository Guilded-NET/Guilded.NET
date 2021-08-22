using System;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// A field in an embed.
    /// </summary>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedProvider"/>
    /// <seealso cref="EmbedAuthor"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedField : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The title of the embed.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The description text of the field.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.Always)]
        public string Value
        {
            get; set;
        }
        /// <summary>
        /// Whether the field should be inline with other fields.
        /// </summary>
        /// <value>Boolean</value>
        public bool Inline
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// A field in an embed.
        /// </summary>
        /// <param name="name">The title of the embed</param>
        /// <param name="value">The description text of the field</param>
        /// <param name="inline">Whether the field should be inline with other fields</param>
        public EmbedField(string name, string value, bool inline = false)
        {
            (Name, Value, Inline) = (name, value, inline);
            // If you try to set null title
            if(string.IsNullOrWhiteSpace(name)) throw new NullReferenceException($"Argument {nameof(name)} cannot be null, empty or whitespace.");
            // If you try to set null description
            else if(string.IsNullOrWhiteSpace(value)) throw new NullReferenceException($"Argument {nameof(value)} cannot be null, empty or whitespace.");
        }
        #endregion
    }
}