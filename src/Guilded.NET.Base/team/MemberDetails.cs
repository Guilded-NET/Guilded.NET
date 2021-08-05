using System;
using System.ComponentModel;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Users;
    /// <summary>
    /// Team member details, such as XP, status and stonks.
    /// </summary>
    public class MemberDetails : ClientObject
    {
        /// <summary>
        /// User status of this member.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus")]
        public UserStatus Status
        {
            get; set;
        }
        /// <summary>
        /// Member's social media links.
        /// </summary>
        /// <value></value>
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// What game is the user playing.
        /// </summary>
        /// <value>Transient/game status</value>
        [JsonProperty("userTransientStatus")]
        public GameStatus GameStatus
        {
            get; set;
        }
        /// <summary>
        /// All profile flairs this user has.
        /// </summary>
        /// <value>List of profile flairs</value>
        [JsonProperty("flairInfos")]
        public IList<FlairInfo> Flairs
        {
            get; set;
        }
        /// <summary>
        /// How much team XP this user has.
        /// </summary>
        /// <value>Team XP amount</value>
        [JsonProperty(Required = Required.Always)]
        public long TeamXp
        {
            get; set;
        }
        /// <summary>
        /// When's the last time the member has been online.
        /// </summary>
        /// <value>Last online</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime LastOnline
        {
            get; set;
        }
        /// <summary>
        /// When the user has joined this team.
        /// </summary>
        /// <value>Joined at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime JoinDate
        {
            get; set;
        }
    }
}