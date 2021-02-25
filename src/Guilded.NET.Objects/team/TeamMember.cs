using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams
{
    /// <summary>
    /// Represents member, or user of the team.
    /// </summary>
    public class TeamMember : BaseUser
    {
        /// <summary>
        /// Nickname of the user. Can be null.
        /// </summary>
        /// <value>Username</value>
        [JsonProperty("nickname", Required = Required.AllowNull)]
        public string Nickname
        {
            get; set;
        }
        /// <summary>
        /// User badges in Guilded, like partner badge or staff badge. Can be null.
        /// </summary>
        /// <value>List of badges</value>
        [JsonProperty("badges", Required = Required.AllowNull)]
        public IList<GlobalBadge> Badges
        {
            get; set;
        }
        /// <summary>
        /// When this user joined the team.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("joinDate", Required = Required.Always)]
        public DateTime JoinDate
        {
            get; set;
        }
        /// <summary>
        /// When the member was last online.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("lastOnline", Required = Required.Always)]
        public DateTime LastOnline
        {
            get; set;
        }
        /// <summary>
        /// Status message and emote.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus", Required = Required.AllowNull)]
        public UserStatus Status
        {
            get; set;
        }
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        [JsonProperty("socialLinks", Required = Required.AllowNull)]
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// IDs of the roles this member has.
        /// </summary>
        /// <value>Role IDs</value>
        [JsonProperty("roleIds", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(new uint[] { })]
        public IList<uint> RoleIds
        {
            get; set;
        }
        /// <summary>
        /// Games this user has on their profile.
        /// </summary>
        /// <value>List of game aliases</value>
        [JsonProperty("aliases", Required = Required.AllowNull)]
        public IList<GameAlias> Aliases
        {
            get; set;
        }
        /// <summary>
        /// Presence of the user.
        /// </summary>
        /// <value>Presence</value>
        [JsonProperty("userPresenceStatus", Required = Required.Always)]
        public Presence Presence
        {
            get; set;
        }
        /// <summary>
        /// Member's XP count.
        /// </summary>
        /// <value>XP</value>
        [JsonProperty("teamXp", Required = Required.Always)]
        public long Xp
        {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//
        /// <summary>
        /// Gets either a nickname or a username of this member.
        /// </summary>
        /// <value>Nickname or username</value>
        [JsonIgnore]
        public string DisplayName
        {
            get => Nickname ?? Username;
        }

        /// <summary>
        /// Gets user's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() + 71;
    }
}