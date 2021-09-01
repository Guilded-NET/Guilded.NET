// using System.Collections.Generic;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Users
// {
//     /// <summary>
//     /// A list of friends and friend requests.
//     /// </summary>
//     public class FriendList : ClientObject
//     {
//         /// <summary>
//         /// A list of friends this user has.
//         /// </summary>
//         /// <value>Friends</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<FriendUser> Friends
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A list of pending and requested friend requests.
//         /// </summary>
//         /// <value>Friend requests</value>
//         [JsonProperty(Required = Required.Always)]
//         public FriendRequests Requests
//         {
//             get; set;
//         }
//     }
// }