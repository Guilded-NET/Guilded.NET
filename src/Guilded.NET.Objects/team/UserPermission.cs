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
        /// <summary>
        /// The date when this permission was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// The date when this permission was updated.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <summary>
        /// Denied permissions in this channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("denyPermissions")]
        public PermissionList DenyPermissions {
            get; set;
        }
        /// <summary>
        /// Allowed permissions in this channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("allowPermissions")]
        public PermissionList AllowPermissions {
            get; set;
        }
    }
}