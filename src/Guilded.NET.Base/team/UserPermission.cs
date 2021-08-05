using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Represents user permissions in the channel.
    /// </summary>
    public class UserPermission : PermissionBase, IPermission
    {
        /// <summary>
        /// ID of the user.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
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