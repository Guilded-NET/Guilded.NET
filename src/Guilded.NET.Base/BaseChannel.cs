// using System;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base
// {
//     /// <summary>
//     /// Interface for DM channels, normal channels and categories.
//     /// </summary>
//     public abstract class BaseChannel : ClientObject
//     {
//         /// <summary>
//         /// When the channel was created.
//         /// </summary>
//         /// <value>Date</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime CreatedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When the channel was updated.
//         /// </summary>
//         /// <value>Date</value>
//         [JsonProperty(Required = Required.AllowNull)]
//         public DateTime? UpdatedAt
//         {
//             get; set;
//         }
//     }
//     /// <summary>
//     /// Interface for DM channels, normal channels and categories.
//     /// </summary>
//     /// <typeparam name="T">Type of channel's ID</typeparam>
//     public class BaseChannel<T> : BaseChannel
//     {
//         /// <summary>
//         /// ID of this channel.
//         /// </summary>
//         /// <value>Channel ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public T Id
//         {
//             get; set;
//         }
//     }
// }
