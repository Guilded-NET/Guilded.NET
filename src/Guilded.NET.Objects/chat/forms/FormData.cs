using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// A form along with a form response.
    /// </summary>
    public class FormData: ClientObject {
        /// <summary>
        /// A form in Guilded.
        /// </summary>
        /// <value>Form</value>
        [JsonProperty("customForm", Required = Required.Always)]
        public GuildedForm Form {
            get; set;
        }
        /// <summary>
        /// A response of this form.
        /// </summary>
        /// <value>Form response</value>
        [JsonProperty("response", Required = Required.Always)]
        public FormResponse Response {
            get; set;
        }
    }
}