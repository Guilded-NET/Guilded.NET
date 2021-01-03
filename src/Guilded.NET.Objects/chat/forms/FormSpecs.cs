using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// Specifications of forms and polls. Contains fields.
    /// </summary>
    public class FormSpecs: BaseObject {
        /// <summary>
        /// Specifications of forms and polls. Contains fields.
        /// </summary>
        public FormSpecs() =>
            IsValid = true;
        /// <summary>
        /// A list of all form sections.
        /// </summary>
        /// <value>List of sections</value>
        [JsonProperty("sections", Required = Required.Always)]
        public IList<FormSection> Sections {
            get; set;
        }
        /// <summary>
        /// If all forum fields are valid.
        /// </summary>
        /// <value>Valid</value>
        [JsonProperty("isValid", Required = Required.Always)]
        public bool IsValid {
            get; set;
        }
        /// <summary>
        /// Generates a form spec from form sections.
        /// </summary>
        /// <param name="sections">Sections to include</param>
        /// <returns>New form spec</returns>
        public static FormSpecs Generate(params FormSection[] sections) =>
            new FormSpecs {
                Sections = sections
            };
        /// <summary>
        /// Generates a form spec from form fields.
        /// </summary>
        /// <param name="fields">Fields to create sections from</param>
        /// <returns>New form spec</returns>
        public static FormSpecs Generate(params FormField[] fields) => Generate(fields.Select(x => FormSection.Generate(x)).ToArray());
    }
}