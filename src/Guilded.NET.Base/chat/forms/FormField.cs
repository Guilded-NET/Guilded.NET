using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Forms
{
    /// <summary>
    /// A field in a form or a poll.
    /// </summary>
    public class FormField
    {
        /// <summary>
        /// What type of field it is(radio list, check list, dropdown, text area, etc.)
        /// </summary>
        /// <value>Form field type</value>
        [JsonProperty(Required = Required.Always)]
        public FormFieldType Type
        {
            get; set;
        }
        /// <summary>
        /// If field is necessary or not.
        /// </summary>
        /// <value>Optional</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsOptional
        {
            get; set;
        }
        /// <summary>
        /// A header of the field. Sometimes used as a name. Mostly empty.
        /// </summary>
        /// <value>Name</value>
        public string Header
        {
            get; set;
        }
        /// <summary>
        /// Label attached to the field. Mostly used as a name.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Label
        {
            get; set;
        }
        /// <summary>
        /// All options in radio, check lists and dropdowns.
        /// </summary>
        /// <value>List of field options</value>
        public IList<FieldOption> Options
        {
            get; set;
        }
        /// <summary>
        /// An ID of a field.
        /// </summary>
        /// <value>Field ID</value>
        public FormId FieldName
        {
            get; set;
        }
        /// <summary>
        /// A default option or a value in this field.
        /// </summary>
        /// <value>Null</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string DefaultValue
        {
            get; set;
        }
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="type">What type of field it is</param>
        /// <param name="isOptional">Whether it is optional to be used</param>
        /// <param name="options">The options of radio, dropdown and checkmark fields</param>
        public FormField(string title, FormFieldType type, bool isOptional, params FieldOption[] options)
        {
            // A field option number
            uint i = 1;
            // Assign a number to each field option
            foreach (FieldOption option in options)
                option.DefaultValue = $"Option {i++}";
            (Type, Label, Header, FieldName, Options, IsOptional, DefaultValue) = (
                type,
                type == FormFieldType.Text ? "Answer" : title,
                type == FormFieldType.Text ? title : "",
                FormId.Random,
                options,
                isOptional,
                null
            );
        }
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="type">What type of field it is</param>
        /// <param name="options">The options of radio, dropdown and checkmark fields</param>
        public FormField(string title, FormFieldType type, params FieldOption[] options) : this(title, type, false, options) { }
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="isOptional">Whether it is optional to be used</param>
        public FormField(string title, bool isOptional = false) : this(title, FormFieldType.Text, isOptional) { }
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="isOptional">Whether it is optional to be used</param>
        /// <param name="options">The options of radio, dropdown and checkmark fields</param>
        public FormField(string title, bool isOptional, params FieldOption[] options) : this(title, FormFieldType.Radios, isOptional, options) { }
        /// <summary>
        /// A field in a form or a poll.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="options">The options of radio, dropdown and checkmark fields</param>
        public FormField(string title, params FieldOption[] options) : this(title, false, options) { }
    }
}