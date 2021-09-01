// using System;
// using System.Linq;
// using System.ComponentModel;
// using System.Threading.Tasks;
// using System.Collections.Generic;

// using Guilded.NET.Base.Chat;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Users
// {
//     using Teams;
//     /// <summary>
//     /// Represents DMs and DM groups.
//     /// </summary>
//     public class DMChannel : BaseChannel<Guid>
//     {
//         /// <summary>
//         /// Who created this channel.
//         /// </summary>
//         /// <value>Created by</value>
//         [JsonProperty(Required = Required.Always)]
//         public GId CreatedBy
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Who created this channel.
//         /// </summary>
//         /// <value>Created by</value>
//         [JsonProperty(Required = Required.Always)]
//         public GId OwnerId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When this channel got deleted.
//         /// </summary>
//         /// <value>Deleted at</value>
//         [JsonProperty(Required = Required.AllowNull)]
//         public DateTime? DeletedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// All users in this DM channel.
//         /// </summary>
//         /// <value>DM channel users</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<DMUser> Users
//         {
//             get; set;
//         }
//         /// <summary>
//         /// If this DM channel is a group or default.
//         /// </summary>
//         /// <value>Type</value>
//         [JsonProperty(Required = Required.Always)]
//         public DMType DmType
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Last message posted in this channel.
//         /// </summary>
//         /// <value>Last message</value>
//         public Message LastMessage
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Type of this channel.
//         /// </summary>
//         /// <value>Content Type</value>
//         [JsonProperty("contentType", Required = Required.Always)]
//         public ChannelType Type
//         {
//             get; set;
//         }
//     }
// }