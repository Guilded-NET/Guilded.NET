using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    /// <summary>
    /// How this member was updated.
    /// </summary>
    public class MemberUpdated : BaseObject
    {
        /// <summary>
        /// How this member was updated.
        /// </summary>
        public MemberUpdated() =>
            (Name, Nickname, Status) = (null, null, null);
        /// <summary>
        /// A name of this user was updated.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name")]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A nickname of this user was updated.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("nickname")]
        public string Nickname
        {
            get; set;
        }
        /// <summary>
        /// User status got updated.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus")]
        public UserStatus Status
        {
            get; set;
        }
    }
}