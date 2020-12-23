using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class User: ClientObject {
        /// <summary>
        /// Given ID of the user.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// Current name of the user.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Username {
            get; set;
        }
        /// <summary>
        /// Large version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureLg")]
        public Uri AvatarLarge {
            get; set;
        }
        /// <summary>
        /// User's current profile picture.
        /// </summary>
        [JsonProperty("profilePicture")]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// Small version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureSm")]
        public Uri AvatarSmall {
            get; set;
        }
        /// <summary>
        /// Blurry version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureBlur")]
        public Uri AvatarBlurry {
            get; set;
        }
        /// <summary>
        /// Large version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerLg")]
        public Uri BannerLarge {
            get; set;
        }
        /// <summary>
        /// Small version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerSm")]
        public Uri BannerSmall {
            get; set;
        }
        /// <summary>
        /// Blurry version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerBlur")]
        public Uri BannerBlurry {
            get; set;
        }
        /// <summary>
        /// User's steam ID.
        /// </summary>
        [JsonProperty("steamId", Required = Required.AllowNull)]
        public string SteamID {
            get; set;
        }
        /// <summary>
        /// Moderation status of the user.
        /// </summary>
        [JsonProperty("moderationStatus", Required = Required.AllowNull)]
        public string ModerationStatus {
            get; set;
        }
        /// <summary>
        /// User's profile <strong>about</strong> section.
        /// </summary>
        [JsonProperty("aboutInfo", Required = Required.AllowNull)]
        public About About {
            get; set;
        }
        /// <summary>
        /// Date of last time user was online.
        /// </summary>
        [JsonProperty("lastOnline", Required = Required.AllowNull)]
        public DateTime? LastOnline {
            get; set;
        }
        /// <summary>
        /// Date of user's registration.
        /// </summary>
        [JsonProperty("joinDate", Required = Required.Always)]
        public DateTime JoinDate {
            get; set;
        }
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
        public override bool Equals(object obj) {
            if(obj is User user) return user.Id == Id;
            else return false;
        }
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