using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// Friendship status.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum FriendStatus
    {
        /// <summary>
        /// They are friends.
        /// </summary>
        Accepted,
        /// <summary>
        /// This user has sent a friend request and still wasn't accepted.
        /// </summary>
        Requested,
        /// <summary>
        /// Someone has sent a friend request to this user.
        /// </summary>
        Pending
    }
}