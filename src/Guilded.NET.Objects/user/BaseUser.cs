using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// A base for all users.
    /// </summary>
    public class BaseUser: ClientObject {
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
        public Uri ProfilePicture {
            get; set;
        }
        /// <summary>
        /// Blurry version of profile banner.
        /// </summary>
        [JsonProperty("profileBannerBlur")]
        public Uri ProfileBannerBlurry {
            get; set;
        }

        /// <summary>
        /// Gets this member as a user.
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> AsUserAsync() =>
            await ParentClient.GetUserAsync(Id);
        /// <summary>
        /// Gets this member as a user.
        /// </summary>
        /// <returns>User</returns>
        public User AsUser() =>
            ParentClient.GetUser(Id);
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <returns>Channel</returns>
        public async Task<DMChannel> CreateDMAsync() =>
            await ParentClient.CreateDMChannelAsync(Id);
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <returns>Channel</returns>
        public DMChannel CreateDM() =>
            ParentClient.CreateDMChannel(Id);

        /// <summary>
        /// Turns user to string.
        /// </summary>
        /// <returns>User as a string</returns>
        public override string ToString() => $"User {Username}({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is BaseUser user) return user.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not users are equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(BaseUser us0, BaseUser us1) => us0.Id == us1.Id;
        /// <summary>
        /// Whether or not users are not equal.
        /// </summary>
        /// <param name="us0">First user to be compared</param>
        /// <param name="us1">Second user to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(BaseUser us0, BaseUser us1) => !(us0 == us1);
        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 12;
    }
}