using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// When user gets updated.
    /// </summary>
    /// <value>USER_UPDATED</value>
    public class UserUpdatedEvent: Event {
        /// <summary>
        /// When user gets updated.
        /// </summary>
        public UserUpdatedEvent() =>
            (Name, Subdomain, Email) = (null, null, null);
        /// <summary>
        /// ID of the user updated.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId {
            get; set;
        }
        /// <summary>
        /// A new name of the user
        /// </summary>
        /// <value>Null or name</value>
        [JsonProperty("name")]
        public string Name {
            get; set;
        } 
        /// <summary>
        /// A new email of client's user.
        /// </summary>
        /// <value>Null or email</value>
        [JsonProperty("email")]
        public string Email {
            get; set;
        } 
        /// <summary>
        /// A new subdomain for this user.
        /// </summary>
        /// <value>Null or subdomain</value>
        [JsonProperty("subdomain")]
        public string Subdomain {
            get; set;
        } 
    }
}