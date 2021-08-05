using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// A friend, friend request sent to user or friend request sent by user.
    /// </summary>
    public class Friend : ClientObject
    {
        /// <summary>
        /// Friend's ID.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("friendUserId", Required = Required.Always)]
        public GId FriendId
        {
            get; set;
        }
        /// <summary>
        /// A friendship status.
        /// </summary>
        /// <remarks>
        /// <para>The status of friendship between this user and client's user account.</para>
        /// <para>Tells us whether:</para>
        /// <list type="bullet">
        ///     <item>This user and client's user are friends</item>
        ///     <item>This user sent a friend request to this client</item>
        ///     <item>This client sent a request to this user</item>
        /// </list>
        /// </remarks>
        /// <value>Friendship status</value>
        [JsonProperty("friendStatus", Required = Required.Always)]
        public FriendStatus Status
        {
            get; set;
        }
        /// <summary>
        /// When the friend request was sent.
        /// </summary>
        /// <remarks>
        /// When the user/you sent a friend request or accepted the friend request.
        /// </remarks>
        /// <value>Request created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
    }
}