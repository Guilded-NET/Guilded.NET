using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Users
{
    /// <summary>
    /// User in a friend list.
    /// </summary>
    public class FriendUser : BaseUser
    {
        /// <summary>
        /// Small version of profile picture.
        /// </summary>
        [JsonProperty("profilePictureSm", Required = Required.AllowNull)]
        public Uri AvatarSmall
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
        /// When is the last time this user was online.
        /// </summary>
        /// <value>Last online at</value>
        [JsonProperty("lastOnline", Required = Required.AllowNull)]
        public DateTime LastOnline
        {
            get; set;
        }
        /// <summary>
        /// Gets user hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() + 19;
    }
}