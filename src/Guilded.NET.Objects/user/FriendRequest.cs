using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Users
{
    /// <summary>
    /// A list of requests.
    /// </summary>
    public class FriendRequests : ClientObject
    {
        /// <summary>
        /// A list of friend requests this user has sent.
        /// </summary>
        /// <value>Friend requests</value>
        [JsonProperty("requested", Required = Required.Always)]
        public IList<FriendUser> Requested
        {
            get; set;
        }
        /// <summary>
        /// A list of friend requests sent to this user.
        /// </summary>
        /// <value>Friend requests</value>
        [JsonProperty("pending", Required = Required.Always)]
        public IList<FriendUser> Pending
        {
            get; set;
        }
    }
}