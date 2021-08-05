using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Users;
    /// <summary>
    /// Represents member, or user of the team.
    /// </summary>
    public class TeamMember : BaseUser
    {
        #region JSON properties
        /// <summary>
        /// Nickname of the user. Can be null.
        /// </summary>
        /// <value>Username</value>
        public string Nickname
        {
            get; set;
        }
        /// <summary>
        /// User badges in Guilded, like partner badge or staff badge. Can be null.
        /// </summary>
        /// <value>List of badges</value>
        public IList<GlobalBadge> Badges
        {
            get; set;
        }
        /// <summary>
        /// When this user joined the team.
        /// </summary>
        /// <value>Date</value>
        public DateTime? JoinDate
        {
            get; set;
        }
        /// <summary>
        /// When the member was last online.
        /// </summary>
        /// <value>Date</value>
        public DateTime? LastOnline
        {
            get; set;
        }
        /// <summary>
        /// Status message and emote.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus")]
        public UserStatus Status
        {
            get; set;
        }
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// IDs of the roles this member has.
        /// </summary>
        /// <value>Role IDs</value>
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(new uint[] { })]
        public IList<uint> RoleIds
        {
            get; set;
        }
        /// <summary>
        /// Games this user has on their profile.
        /// </summary>
        /// <value>List of game aliases</value>
        public IList<GameAlias> Aliases
        {
            get; set;
        }
        /// <summary>
        /// Presence of the user.
        /// </summary>
        /// <value>Presence</value>
        [JsonProperty("userPresenceStatus")]
        public Presence? Presence
        {
            get; set;
        }
        /// <summary>
        /// Member's XP count.
        /// </summary>
        /// <value>XP</value>
        public long? Xp
        {
            get; set;
        }
        #endregion

        
        #region Additional
        /// <summary>
        /// Gets either a nickname or a username of this member.
        /// </summary>
        /// <value>Nickname or username</value>
        [JsonIgnore]
        public string DisplayName => Nickname ?? Username;
        #endregion
    }
}