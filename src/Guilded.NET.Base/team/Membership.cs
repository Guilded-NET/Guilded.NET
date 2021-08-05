using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A group membership.
    /// </summary>
    public class Membership : BaseObject
    {
        /// <summary>
        /// Type of the membership.
        /// </summary>
        /// <value>Membership type</value>
        [JsonProperty(Required = Required.Always)]
        public MembershipType Type
        {
            get; set;
        }
        /// <summary>
        /// ID of the membership user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
    }
}