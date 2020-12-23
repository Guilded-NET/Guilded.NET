using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    public class Membership: BaseObject {
        /// <summary>
        /// Type of the membership.
        /// </summary>
        /// <value>Membership type</value>
        [JsonProperty("type", Required = Required.Always)]
        public MembershipType Type {
            get; set;
        }
        /// <summary>
        /// ID of the membership user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserID {
            get; set;
        }
    }
}