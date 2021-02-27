using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Objects.Users
{
    using Content;
    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class ProfileUser : BaseUser
    {
        /// <summary>
        /// A URL subdomain for this user.
        /// </summary>
        /// <value>Subdomain</value>
        [JsonProperty("subdomain", Required = Required.AllowNull)]
        public string Subdomain
        {
            get; set;
        }
        /// <summary>
        /// Your email address. This will not be visible in profiles of other people.
        /// </summary>
        /// <value>Email</value>
        [JsonProperty("email")]
        [DefaultValue(null)]
        public string Email
        {
            get; set;
        }
        /// <summary>
        /// Large version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureLg", Required = Required.AllowNull)]
        public Uri AvatarLarge
        {
            get; set;
        }
        /// <summary>
        /// Small version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureSm", Required = Required.AllowNull)]
        public Uri AvatarSmall
        {
            get; set;
        }
        /// <summary>
        /// Blurry version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureBlur", Required = Required.AllowNull)]
        public Uri AvatarBlurry
        {
            get; set;
        }
        /// <summary>
        /// Large version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerLg", Required = Required.AllowNull)]
        public Uri ProfileBannerLarge
        {
            get; set;
        }
        /// <summary>
        /// Small version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerSm", Required = Required.AllowNull)]
        public Uri ProfileBannerSmall
        {
            get; set;
        }
        /// <summary>
        /// User's steam ID.
        /// </summary>
        [JsonProperty("steamId", Required = Required.AllowNull)]
        public string SteamID
        {
            get; set;
        }
        /// <summary>
        /// Moderation status of the user.
        /// </summary>
        [JsonProperty("moderationStatus", Required = Required.AllowNull)]
        public string ModerationStatus
        {
            get; set;
        }
        /// <summary>
        /// User's profile <strong>about</strong> section.
        /// </summary>
        [JsonProperty("aboutInfo", Required = Required.AllowNull)]
        public About About
        {
            get; set;
        }
        /// <summary>
        /// Status message and emote this user has set.
        /// </summary>
        /// <value>User status</value>
        [JsonProperty("userStatus", Required = Required.AllowNull)]
        public UserStatus Status
        {
            get; set;
        }
        /// <summary>
        /// Date of last time user was online.
        /// </summary>
        [JsonProperty("lastOnline", Required = Required.Always)]
        public DateTime LastOnline
        {
            get; set;
        }
        /// <summary>
        /// Date of user's registration.
        /// </summary>
        [JsonProperty("joinDate", Required = Required.Always)]
        public DateTime JoinDate
        {
            get; set;
        }
        /// <summary>
        /// If the user shares a team with you.
        /// </summary>
        /// <value>Teammate</value>
        [JsonProperty("isTeammate", Required = Required.Always)]
        public bool IsTeammate
        {
            get; set;
        }
        /// <summary>
        /// If this user was blocked by client's user.
        /// </summary>
        /// <value>Blocked by client's user</value>
        [JsonProperty("isBlocked", Required = Required.Always)]
        public bool IsBlocked
        {
            get; set;
        }
        /// <summary>
        /// If client's user was blocked by this user.
        /// </summary>
        /// <value>Blocked by this user</value>
        [JsonProperty("isBlockedBy", Required = Required.Always)]
        public bool IsBlockedBy
        {
            get; set;
        }
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        [JsonProperty("socialLinks", Required = Required.Always)]
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// Games this user has on their profile.
        /// </summary>
        /// <value>List of game aliases</value>
        [JsonProperty("aliases", Required = Required.Always)]
        public IList<GameAlias> Aliases
        {
            get; set;
        }
        /// <summary>
        /// All media posted in this profile.
        /// </summary>
        /// <value>List of media</value>
        [JsonProperty("media", Required = Required.Always)]
        public IList<ProfileMedia> Media
        {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// If this user is banned from Guilded's services.
        /// </summary>
        /// <value>Account terminated</value>
        public bool IsBanned
        {
            get => ModerationStatus == "banned";
        }

        //=========================//
        //    Overrides
        //=========================//

        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() + 63;
    }
}