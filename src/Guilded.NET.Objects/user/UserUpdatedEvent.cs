using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Users;
    /// <summary>
    /// When user gets updated.
    /// </summary>
    /// <value>USER_UPDATED</value>
    public class UserUpdatedEvent : Event
    {
        /// <summary>
        /// ID of the user updated.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// A new name of the user
        /// </summary>
        /// <value>Null or name</value>
        [JsonProperty("name")]
        [DefaultValue(null)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A new email of client's user.
        /// </summary>
        /// <value>Null or email</value>
        [JsonProperty("email")]
        [DefaultValue(null)]
        public string Email
        {
            get; set;
        }
        /// <summary>
        /// A new subdomain for this user.
        /// </summary>
        /// <value>Null or subdomain</value>
        [JsonProperty("subdomain")]
        [DefaultValue(null)]
        public string Subdomain
        {
            get; set;
        }
        /// <summary>
        /// A new about info(tagline, about section) for this user.
        /// </summary>
        /// <value>Null or about info</value>
        [JsonProperty("aboutInfo")]
        [DefaultValue(null)]
        public About About
        {
            get; set;
        }
    }
}