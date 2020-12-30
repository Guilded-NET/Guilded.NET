using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    /// <summary>
    /// A social media link.
    /// </summary>
    public class SocialLink: BaseObject {
        /// <summary>
        /// Social media's name.
        /// </summary>
        /// <value>Social media name</value>
        [JsonProperty("type", Required = Required.Always)]
        public string Type {
            get; set;
        }
        /// <summary>
        /// Handle/name of this user in this social media.
        /// </summary>
        /// <value>Social media handle</value>
        [JsonProperty("handle", Required = Required.Always)]
        public string Handle {
            get; set;
        }
        /// <summary>
        /// Additional information related to this social media link.
        /// </summary>
        /// <value>Additional information</value>
        [JsonProperty("additionalInfo", Required = Required.Always)]
        public IDictionary<string, string> AdditionalInfo {
            get; set;
        }
    }
}