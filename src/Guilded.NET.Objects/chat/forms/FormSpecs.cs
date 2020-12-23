using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// Specifications of forms and polls. Contains fields.
    /// </summary>
    public class FormSpecs: BaseObject {
        /// <summary>
        /// Specifications of forms and polls. Contains fields.
        /// </summary>
        public FormSpecs() =>
            IsValid = null;
        /// <summary>
        /// If all forum fields are valid.
        /// </summary>
        /// <value>Valid</value>
        [JsonProperty("isValid")]
        public bool? IsValid {
            get; set;
        }
    }
}