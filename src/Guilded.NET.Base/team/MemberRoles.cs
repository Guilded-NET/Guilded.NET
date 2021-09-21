using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Defines a role list holder and their role list.
    /// </summary>
    /// <remarks>
    /// <para>Defines a receiving a role holder <see cref="UserId"/> and their current role list <see cref="RoleIds"/>.
    /// Roles that were added or removed, or previous role list are not provided. If necessary, previous
    /// role list should be cached before-hand.</para>
    /// </remarks>
    public class MemberRoles
    {
        /// <summary>
        /// The identifier of the role holder.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user that has received all roles in <see cref="RoleIds"/> list.</para>
        /// </remarks>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// The list of roles <see cref="UserId"/> holds.
        /// </summary>
        /// <remarks>
        /// <para>The list of roles that <see cref="UserId"/> is currently holding.</para>
        /// <para>Received or removed roles are not provided, so caching of previous
        /// role list is necessary if previous role list is needed.</para>
        /// </remarks>
        /// <value>List of role IDs</value>
        [JsonProperty(Required = Required.Always)]
        public IList<uint> RoleIds
        {
            get; set;
        }
    }
}