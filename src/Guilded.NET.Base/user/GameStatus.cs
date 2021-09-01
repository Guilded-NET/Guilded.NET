// using System;
// using System.ComponentModel;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Serialization;

// namespace Guilded.NET.Base.Users
// {
//     /// <summary>
//     /// A status of the game user is playing.
//     /// </summary>
//     public class GameStatus : ClientObject
//     {
//         /// <summary>
//         /// ID of the status.
//         /// </summary>
//         /// <value>Status ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public uint Id
//         {
//             get; set;
//         }
//         /// <summary>
//         /// ID of the game user is playing.
//         /// </summary>
//         /// <value>Game ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public uint GameId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A type of game presence.
//         /// </summary>
//         /// <value>"gamepresence"</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Type
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When the game started.
//         /// </summary>
//         /// <value>Game started at</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime StartedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// ID of the Guilded client this user is using.
//         /// </summary>
//         /// <value>Guilded client ID</value>
//         public Guid? GuildedClientId
//         {
//             get; set;
//         }
//     }
// }