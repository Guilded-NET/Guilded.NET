// using System;

// using Newtonsoft.Json;

// namespace Guilded.Base.Teams
// {
//     /// <summary>
//     /// Represents Guilded channel.
//     /// </summary>
//     public class TemporalChannel : TeamChannel
//     {
//         /// <summary>
//         /// ID of the channel which thread was created in.
//         /// </summary>
//         /// <value>Channel ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public Guid OriginatingChannelId { get; }
//         /// <summary>
//         /// Type of channel where thread is originating in.
//         /// </summary>
//         /// <value>Channel ID</value>
//         [JsonProperty("originatingChannelContentType", Required = Required.Always)]
//         public ChannelType OriginatingContentType { get; }
//         /// <summary>
//         /// ID of the message from which thread is originating.
//         /// </summary>
//         /// <value>Message ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public Guid ThreadMessageId { get; }
//     }
// }