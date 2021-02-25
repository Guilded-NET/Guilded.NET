using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms
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
        [JsonProperty("hideReorderHandle", Required = Required.Always)]
        public bool HideReorderHandle
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("hideOptionalFieldToggle", Required = Required.Always)]
        public bool HideOptionalFieldToggle
        {
            get; set;
        }
        /// <summary>
        /// Form field type options.
        /// </summary>
        /// <value>List of field types</value>
        [JsonProperty("fieldTypeOptions", Required = Required.Always)]
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
        /// Generates a form section with fields.
        /// </summary>
        /// <param name="fields">Fields to assign to a form section</param>
        /// <returns>A new form section</returns>
        public static FormSection Generate(params FormField[] fields) =>
            new FormSection
            {
                Fields = fields,
                HideReorderHandle = true,
                HideOptionalFieldToggle = true,
                FieldTypeOptions = fields.Select(x => x.Type).ToArray()
            };
    }
}