using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    /// <summary>
    /// What roles were added/removed from a member.
    /// </summary>
    public class MemberRoleUpdate : BaseObject
    {
        /// <summary>
        /// ID of the user who was updated.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// A new list of roles this user has.
        /// </summary>
        /// <value>List of role IDs</value>
        [JsonProperty("roleIds", Required = Required.Always)]
        public IList<uint> RoleIds
        {
            get; set;
        }
    }
}