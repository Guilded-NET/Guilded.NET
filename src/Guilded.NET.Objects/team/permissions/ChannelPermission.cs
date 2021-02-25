using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Permissions
{
    /// <summary>
    /// Represents permission in a channel.
    /// </summary>
    public class ChannelPermission : BaseObject, IPermission
    {
        /// <summary>
        /// Id of the team this permission's channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the permission's role.
        /// </summary>
        /// <value></value>
        [JsonProperty("teamRoleId", Required = Required.Always)]
        public ulong TeamRoleId
        {
            get; set;
        }
        /// <summary>
        /// The date when this permission was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// The date when this permission was updated.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// Denied permissions in this channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("denyPermissions")]
        public PermissionList DenyPermissions
        {
            get; set;
        }
        /// <summary>
        /// Allowed permissions in this channel.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("allowPermissions")]
        public PermissionList AllowPermissions
        {
            get; set;
        }
    }
}