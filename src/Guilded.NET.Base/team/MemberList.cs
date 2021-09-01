// using System.Collections.Generic;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Teams
// {
//     /// <summary>
//     /// A list of members, webhooks and bots.
//     /// </summary>
//     public class MemberList : BaseObject
//     {
//         /// <summary>
//         /// A list of all members in a team.
//         /// </summary>
//         /// <value>List of members</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<Member> Members
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A list of all channel webhooks.
//         /// </summary>
//         /// <value>List of webhooks</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<Webhook> Webhooks
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A list of flowbots in this team.
//         /// </summary>
//         /// <value>List of bots</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<Flowbot> Bots
//         {
//             get; set;
//         }
//     }
// }