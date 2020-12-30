using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Represents user permissions in the channel.
    /// </summary>
    public class UserPermission: BaseObject, IPermission {
        /// <summary>
        /// ID of the user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId {
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
        /// <inheritdoc/>
        [JsonProperty("denyPermissions")]
        public PermissionList DenyPermissions {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("allowPermissions")]
        public PermissionList AllowPermissions {
            get; set;
        }
    }
}