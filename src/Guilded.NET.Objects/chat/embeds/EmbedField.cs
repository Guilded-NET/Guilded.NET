using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Field of an embed.
    /// </summary>
    public class EmbedField : BaseObject
    {
        /// <summary>
        /// Embed field title.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Embed field description.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("value", Required = Required.Always)]
        public string Value
        {
            get; set;
        }
        /// <summary>
        /// Whether or not the field should be inline with other fields.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("inline", NullValueHandling = NullValueHandling.Ignore)]
        public bool Inline
        {
            get; set;
        } = false;
        /// <summary>
        /// Generates an embed field.
        /// </summary>
        /// <param name="title">Title of the field</param>
        /// <param name="description">Description of the field</param>
        /// <param name="inline">If field should be inline</param>
        /// <returns>New field</returns>
        public static EmbedField Generate(string title, string description, bool inline = false) =>
            new EmbedField
            {
                Name = title,
                Value = description,
                Inline = inline
            };
    }
}