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
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
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
        /// <summary>
        /// Generates a form field.
        /// </summary>
        /// <param name="title">Title of the form field</param>
        /// <param name="type">What type it is</param>
        /// <param name="isOptional">If it's optional</param>
        /// <param name="options">All of the field options(if it's a radio, checkmark or dropdown field)</param>
        /// <returns>A new field</returns>
        public static FormField Generate(string title, FormFieldType type, bool isOptional = false, params FieldOption[] options) {
            // A field option number
            uint i = 1;
            // Assign a number to each field option
            foreach(FieldOption option in options) {
                option.DefaultValue = $"Option {i}";
                i++;
            }
            // Returns a new field
            return new FormField {
                Type = type,
                Label = type == FormFieldType.Text ? "Answer" : title,
                Header = type == FormFieldType.Text ? title : "",
                FieldName = FormId.Random,
                Options = options,
                IsOptional = isOptional,
                Grow = 1,
                DefaultValue = null
            };
        }
    }
}