using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// A section of a form field.
    /// </summary>
    public class FormSections {
        /// <summary>
        /// Header for form fields(question 1, question 2, question 3, ...)
        /// </summary>
        /// <value>Header</value>
        [JsonProperty("header")]
        public string Header {
            get; set;
        }
        /// <summary>
        /// All of the fields in this form section.
        /// </summary>
        /// <value>List of fields</value>
        [JsonProperty("fieldSpecs", Required = Required.Always)]
        public IList<FormField> Fields {
            get; set;
        }
    }
}