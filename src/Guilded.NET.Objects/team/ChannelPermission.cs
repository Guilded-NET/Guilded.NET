using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Represents permission in a channel.
    /// </summary>
    public class ChannelPermission: PermissionBase, IPermission {
        /// <summary>
        /// Id of the team this permission's channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the role this permission was assigned to.
        /// </summary>
        /// <value></value>
        [JsonProperty("teamRoleId", Required = Required.Always)]
        public ulong TeamRoleId {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
    }
}