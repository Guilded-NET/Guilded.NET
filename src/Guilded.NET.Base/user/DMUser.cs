using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class DMUser : BaseUser
    {
        /// <summary>
        /// When the user was added to DMs.
        /// </summary>
        /// <value>Added</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime AddedAt
        {
            get; set;
        }
        /// <summary>
        /// When the user was removed from DMs.
        /// </summary>
        /// <value>Removed</value>
        public DateTime? RemovedAt
        {
            get; set;
        }
        /// <summary>
        /// Status message and emote of the user.
        /// </summary>
        /// <value>Status</value>
        [JsonProperty("userStatus")]
        public UserStatus UserStatus
        {
            get; set;
        }
        /// <summary>
        /// DM channel ID this user is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// Whether this user is owner of the DMs.
        /// </summary>
        /// <value>DM owner</value>
        public bool IsOwner
        {
            get; set;
        }
        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() + 15;
    }
}