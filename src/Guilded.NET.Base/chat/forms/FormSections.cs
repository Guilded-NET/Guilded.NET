using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Forms
{
    /// <summary>
    /// A section of a form field.
    /// </summary>
    public class FormSection : BaseObject
    {
        /// <summary>
        /// Hides reordering handle and disallows reordering.
        /// </summary>
        /// <value>Hide handle</value>
        [JsonProperty(Required = Required.Always)]
        public bool HideReorderHandle
        {
            get; set;
        } = true;
        /// <summary>
        /// Hides toggle that makes the field optional.
        /// </summary>
        /// <value>Hide optional button</value>
        [JsonProperty(Required = Required.Always)]
        public bool HideOptionalFieldToggle
        {
            get; set;
        } = true;
        /// <summary>
        /// Form field type options.
        /// </summary>
        /// <value>List of field types</value>
        [JsonProperty(Required = Required.Always)]
        public IList<FormFieldType> FieldTypeOptions
        {
            get; set;
        }
        /// <summary>
        /// All of the fields in this form section.
        /// </summary>
        /// <value>List of fields</value>
        [JsonProperty("fieldSpecs", Required = Required.Always)]
        public IList<FormField> Fields
        {
            get; set;
        }
        /// <summary>
        /// A section of a form field.
        /// </summary>
        /// <param name="fields">The list of fields in this form section</param>
        public FormSection(IList<FormField> fields) =>
            (Fields, FieldTypeOptions) = (fields, fields.Select(x => x.Type).ToArray());
        /// <summary>
        /// A section of a form field.
        /// </summary>
        /// <param name="field">The field in this form section</param>
        public FormSection(FormField field) : this(new List<FormField> { field }) { }
    }
}