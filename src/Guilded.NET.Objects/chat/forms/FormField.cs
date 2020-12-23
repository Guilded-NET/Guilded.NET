using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// A field in a form or a poll.
    /// </summary>
    public class FormField {
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        public FormField() =>
            (Options, Header, DefaultValue) = (new List<FieldOption>(), "", null);
        [JsonProperty("grow")]
        public uint Grow {
            get; set;
        }
        /// <summary>
        /// What type of field it is(radio list, check list, dropdown, text area, etc.)
        /// </summary>
        /// <value>Form field type</value>
        [JsonProperty("type", Required = Required.Always)]
        public FormFieldType Type {
            get; set;
        }
        /// <summary>
        /// If field is necessary or not.
        /// </summary>
        /// <value>Optional</value>
        [JsonProperty("isOptional", Required = Required.Always)]
        public bool IsOptional {
            get; set;
        }
        /// <summary>
        /// A header of the field. Sometimes used as a name. Mostly empty.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("header")]
        public string Header {
            get; set;
        }
        /// <summary>
        /// Label attached to the field. Mostly used as a name 
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("label", Required = Required.Always)]
        public string Label {
            get; set;
        }
        /// <summary>
        /// All options in radio, check lists and dropdowns.
        /// </summary>
        /// <value>List of field options</value>
        [JsonProperty("options")]
        public IList<FieldOption> Options {
            get; set;
        }
        /// <summary>
        /// An ID of a field.
        /// </summary>
        /// <value>Field ID</value>
        [JsonProperty("fieldName")]
        public FormId FieldName {
            get; set;
        }
        /// <summary>
        /// A default option or a value in this field.
        /// </summary>
        /// <value>Null</value>
        [JsonProperty("defaultValue", Required = Required.AllowNull)]
        public string DefaultValue {
            get; set;
        }
    }
}