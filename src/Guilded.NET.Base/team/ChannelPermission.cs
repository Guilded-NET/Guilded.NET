using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Represents permission in a channel.
    /// </summary>
    public class ChannelPermission : PermissionBase, IPermission
    {
        /// <summary>
        /// Id of the team this permission's channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the role this permission was assigned to.
        /// </summary>
        /// <value></value>
        [JsonProperty("teamRoleId", Required = Required.Always)]
        public ulong RoleId
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? UpdatedAt
        {
            get; set;
        }
    }
}