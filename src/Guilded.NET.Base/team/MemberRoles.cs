using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Defines a role list holder and their role list.
    /// </summary>
    public class MemberRoles
    {
        /// <summary>
        /// The identifier of the role holder.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// The list of roles <see cref="UserId"/> holds.
        /// </summary>
        /// <value>List of role IDs</value>
        [JsonProperty(Required = Required.Always)]
        public IList<uint> RoleIds
        {
            get; set;
        }
    }
}