using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// An option in radio, check list and dropdown fields.
    /// </summary>
    public class FieldOption {
        /// <summary>
        /// An option in radio, check list and dropdown fields.
        /// </summary>
        public FieldOption() =>
            DefaultValue = "Option 1";
        /// <summary>
        /// A name of the field option.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("label", Required = Required.Always)]
        public string Label {
            get; set;
        }
        /// <summary>
        /// A default option for this field.
        /// </summary>
        /// <value>Default option</value>
        [JsonProperty("defaultValue")]
        public string DefaultValue {
            get; set;
        }
        /// <summary>
        /// A value of this field.
        /// </summary>
        /// <value>Form name</value>
        [JsonProperty("value")]
        public FormId Value {
            get; set;
        }
        /// <summary>
        /// A name of the field this option is in.
        /// </summary>
        /// <value>Form name</value>
        [JsonProperty("fieldName")]
        public FormId FieldName {
            get; set;
        }
        /// <summary>
        /// A name of this option.
        /// </summary>
        /// <value>Form name</value>
        [JsonProperty("optionName")]
        public FormId OptionName {
            get; set;
        }
        /// <summary>
        /// Creates an option for dropdown, radio and checkmark fields.
        /// </summary>
        /// <param name="label">A name for this option</param>
        /// <returns>New option</returns>
        public static FieldOption Generate(string label) {
            // Creates a random form ID
            FormId rand = FormId.Random;
            // Creates an option
            return new FieldOption {
                FieldName = FormId.Random,
                Label = label,
                OptionName = rand,
                Value = rand
            };
        }
    }
}