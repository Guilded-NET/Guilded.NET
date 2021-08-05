using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class Team : BaseTeam
    {
        /// <summary>
        /// Team's "biography".
        /// </summary>
        /// <value>Biography</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Bio
        {
            get; set;
        }
        /// <summary>
        /// Description of this team.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// If this user is following this team.
        /// </summary>
        /// <value>Following</value>
        [JsonProperty("userFollowsTeam", Required = Required.Always)]
        public bool IsFollowing
        {
            get; set;
        }
        /// <summary>
        /// If this user has applied for this server.
        /// </summary>
        /// <value>Applied</value>
        [JsonProperty("isUserApplicant")]
        [DefaultValue(false)]
        public bool IsApplicant
        {
            get; set;
        }
        /// <summary>
        /// If this user was invited.
        /// </summary>
        /// <value>Invited</value>
        [JsonProperty("isUserInvited")]
        [DefaultValue(false)]
        public bool IsInvited
        {
            get; set;
        }
        /// <summary>
        /// If this used was banned from this team.
        /// </summary>
        /// <value>Banned</value>
        [JsonProperty("isUserBannedFromTeam")]
        [DefaultValue(false)]
        public bool IsBanned
        {
            get; set;
        }
    }
}