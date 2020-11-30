using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Represents member, or user of the team.
    /// </summary>
    public class TeamMember: ClientObject {
        /// <summary>
        /// ID of the member.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// Username of the user.
        /// </summary>
        /// <value>Username</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Username {
            get; set;
        }
        /// <summary>
        /// Nickname of the user. Can be null.
        /// </summary>
        /// <value>Username</value>
        [JsonProperty("nickname")]
        public string Nickname {
            get; set;
        }
        /// <summary>
        /// User badges in Guilded, like partner badge or staff badge. Can be null.
        /// </summary>
        /// <value>List of badges</value>
        [JsonProperty("badges")]
        public IList<string> Badges {
            get; set;
        }
        /// <summary>
        /// When this user joined the team.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("joinDate", Required = Required.Always)]
        public DateTime JoinDate {
            get; set;
        }
        /// <summary>
        /// When the member was last online.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("lastOnline")]
        public DateTime LastOnline {
            get; set;
        }
        /// <summary>
        /// Profile picture of the user.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profilePicture", Required = Required.AllowNull)]
        public Uri ProfilePicture {
            get; set;
        }
        /// <summary>
        /// User's blurry profile banner.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profileBannerBlur", Required = Required.AllowNull)]
        public Uri ProfileBanner {
            get; set;
        }
        /// <summary>
        /// About info of this user.
        /// </summary>
        /// <value>User about</value>
        [JsonProperty("aboutInfo", Required = Required.AllowNull)]
        public About AboutInfo {
            get; set;
        }
        /// <summary>
        /// Status message and emote.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus", Required = Required.AllowNull)]
        public UserStatus Status {
            get; set;
        }
        // TODO: Add SocialLink
        // [JsonProperty("socialLinks")]
        // public IList<SocialLink> SocialLinks {
        //     get; set;
        // }
        /// <summary>
        /// IDs of the roles this member has.
        /// </summary>
        /// <value>Role IDs</value>
        [JsonProperty("roleIds", Required = Required.AllowNull)]
        public IList<uint> RoleIds {
            get; set;
        }
        // TODO: Add Alias
        // [JsonProperty("aliases")]
        // public IList<Alias> Aliases {
        //     get; set;
        // }
        /// <summary>
        /// Presence of the user.
        /// </summary>
        /// <value>Presence</value>
        [JsonProperty("userPresenceStatus", Required = Required.Always)]
        public Presence Presence {
            get; set;
        }
        /// <summary>
        /// Member's XP count.
        /// </summary>
        /// <value>XP</value>
        [JsonProperty("teamXp", Required = Required.Always)]
        public long Xp {
            get; set;
        }
    }
}