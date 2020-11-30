using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Guilded user. This is NOT Guild member.
    /// </summary>
    public class DMUser: ClientObject {
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
        /// User's current profile picture.
        /// </summary>
        [JsonProperty("profilePicture")]
        public Uri Avatar {
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
        /// When the user was added to DMs.
        /// </summary>
        /// <value>Added</value>
        [JsonProperty("addedAt", Required = Required.Always)]
        public DateTime AddedAt {
            get; set;
        }
        /// <summary>
        /// When the user was removed from DMs.
        /// </summary>
        /// <value>Removed</value>
        [JsonProperty("removedAt")]
        public DateTime? RemovedAt {
            get; set;
        }
        /// <summary>
        /// Status message and emote of the user.
        /// </summary>
        /// <value>Status</value>
        [JsonProperty("userStatus")]
        public UserStatus UserStatus {
            get; set;
        }
        /// <summary>
        /// DM channel ID this user is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// Whether or not this user is owner of the DMs.
        /// </summary>
        /// <value>DM owner</value>
        [JsonProperty("isOwner")]
        public bool IsOwner {
            get; set;
        }
        /// <summary>
        /// Turns user to string.
        /// </summary>
        /// <returns>User as a string</returns>
        public override string ToString() => $"DM User({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is DMUser user) return user.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not users are equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(DMUser us0, DMUser us1) => us0.Id == us1.Id;
        /// <summary>
        /// Whether or not users are not equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(DMUser us0, DMUser us1) => !(us0 == us1);
        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 570;
    }
}