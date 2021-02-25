using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Objects
{
    using Chat;
    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class User : BaseUser
    {
        /// <summary>
        /// Guilded user. This is NOT Guild member.
        /// </summary>
        public User() =>
            Badges = new List<GlobalBadge>();
        /// <summary>
        /// Large version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureLg")]
        public Uri ProfilePictureLarge
        {
            get; set;
        }
        /// <summary>
        /// Small version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureSm")]
        public Uri ProfilePictureSmall
        {
            get; set;
        }
        /// <summary>
        /// Blurry version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureBlur")]
        public Uri ProfilePictureBlurry
        {
            get; set;
        }
        /// <summary>
        /// Large version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerLg")]
        public Uri ProfileBannerLarge
        {
            get; set;
        }
        /// <summary>
        /// Small version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerSm")]
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
        /// Moderation status of the user. If this user is banned, it will return `banned`
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
        /// A list of all global badges this user has.
        /// </summary>
        /// <value>List of badges</value>
        [JsonProperty("badges")]
        public IList<GlobalBadge> Badges
        {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Creates a mention based on a user.
        /// </summary>
        /// <value>User mention</value>
        [JsonIgnore]
        public Mention Mention
        {
            get => Mention.Generate(this);
        }
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
        /// Turns user to string.
        /// </summary>
        /// <returns>User as a string</returns>
        public override string ToString() => $"User({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) =>
            obj is User user && user?.Id == Id;
        /// <summary>
        /// Whether or not users are equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(User us0, User us1) => us0.Id == us1.Id;
        /// <summary>
        /// Whether or not users are not equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(User us0, User us1) => !(us0 == us1);
        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 590;
    }
}