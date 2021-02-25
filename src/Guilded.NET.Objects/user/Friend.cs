using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// A friend, friend request sent to user or friend request sent by user.
    /// </summary>
    public class Friend: ClientObject {
        /// <summary>
        /// Friend's ID.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("friendUserId", Required = Required.Always)]
        public GId FriendId {
            get; set;
        }
        /// <summary>
        /// A friendship status. If user has a pending friend request, if user has sent a friend request or if friend request got accepted.
        /// </summary>
        /// <value>Friendship status</value>
        [JsonProperty("friendStatus", Required = Required.Always)]
        public FriendStatus Status {
            get; set;
        }
        /// <summary>
        /// When the friend request was sent.
        /// </summary>
        /// <value>Request created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
    }
}