using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base.Users
{
    using Content;

    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class ProfileUser : User
    {
        /// <summary>
        /// When user registered.
        /// </summary>
        /// <remarks>
        /// Tells us when this profile was created. In a sense, when this user registered to Guilded.
        /// </remarks>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
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
        /// If the user shares a team with you.
        /// </summary>
        /// <value>Teammate</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsTeammate
        {
            get; set;
        }
        /// <summary>
        /// If this user was blocked by client's user.
        /// </summary>
        /// <remarks>
        /// If you have blocked this user.
        /// </remarks>
        /// <value>Blocked by client's user</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsBlocked
        {
            get; set;
        }
        /// <summary>
        /// If client's user was blocked by this user.
        /// </summary>
        /// <remarks>
        /// If this user has blocked you.
        /// </remarks>
        /// <value>Blocked by this user</value>
        [JsonProperty(Required = Required.Always)]
        public bool IsBlockedBy
        {
            get; set;
        }
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        [JsonProperty(Required = Required.Always)]
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// All media posted in this profile.
        /// </summary>
        /// <remarks>
        /// All media that has been posted in the profile by the user. In other words, media tab.
        /// </remarks>
        /// <value>List of media</value>
        [JsonProperty(Required = Required.Always)]
        public IList<ProfileMedia> Media
        {
            get; set;
        }
        /// <summary>
        /// Stonks this user has.
        /// </summary>
        /// <remarks>
        /// Count of how many stonks this user has. Stonks show how many people were invited by this user.
        /// </remarks>
        /// <value>Stonk count</value>
        [JsonProperty(Required = Required.Always)]
        public uint Stonks
        {
            get; set;
        }
    }
}