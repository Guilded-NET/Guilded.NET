using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms
{
    /// <summary>
    /// A field in form response.
    /// </summary>
    public class FormResponseField : BaseObject
    {
        /// <summary>
        /// Name of the option which was chosen.
        /// </summary>
        /// <value>Option name</value>
        [JsonProperty("optionName", Required = Required.Always)]
        public FormId OptionName
        {
            get; set;
        } = null;
        /// <summary>
        /// Text which was provided as a value.
        /// </summary>
        /// <value>Text</value>
        [JsonIgnore]
        public string TextValue
        {
            get; set;
        } = null;
    }
}