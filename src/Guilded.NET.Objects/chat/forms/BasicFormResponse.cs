using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// A form response which can be submitted
    /// </summary>
    public class BasicFormResponse: ClientObject {
        /// <summary>
        /// Form fields filled out values.
        /// </summary>
        /// <value>Form field values</value>
        [JsonProperty("values", Required = Required.Always)]
        public IDictionary<FormId, FormResponse> Values {
            get; set;
        }
    }
}