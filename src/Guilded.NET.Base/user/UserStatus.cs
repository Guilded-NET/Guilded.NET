// using Newtonsoft.Json;

// using System.ComponentModel;

// namespace Guilded.NET.Base.Users
// {
//     using Chat;
//     /// <summary>
//     /// Status message of the user.
//     /// </summary>
//     public class UserStatus : BaseObject
//     {
//         /// <summary>
//         /// Status message content. Null if there's no content.
//         /// </summary>
//         /// <value>Message content?</value>
//         [JsonProperty(Required = Required.AllowNull)]
//         public MessageContent Content
//         {
//             get; set;
//         }
//         /// <summary>
//         /// ID of the emote in this status.
//         /// </summary>
//         /// <value>Emote ID?</value>
//         [JsonProperty("customReactionId", Required = Required.AllowNull)]
//         public uint? EmoteId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When it expires.
//         /// </summary>
//         /// <value>Milliseconds?</value>
//         [JsonProperty("expireInMs")]
//         public ulong? ExpiresIn
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Generates user status for setting your custom status.
//         /// </summary>
//         /// <param name="emoteId">ID of the emote it should use</param>
//         /// <param name="content">Content of the status</param>
//         /// <param name="expiresIn">When it should expire in milliseconds</param>
//         /// <returns>Generated status</returns>
//         public static UserStatus Generate(uint? emoteId = null, MessageContent content = null, ulong? expiresIn = 0) =>
//             new UserStatus
//             {
//                 EmoteId = emoteId,
//                 Content = content,
//                 ExpiresIn = expiresIn
//             };
//         /// <summary>
//         /// Generates user status for setting your custom status.
//         /// </summary>
//         /// <param name="emoteId">ID of the emote it should use</param>
//         /// <param name="content">Content of the status</param>
//         /// <param name="expiresIn">When it should expire in milliseconds</param>
//         /// <returns>Generated status</returns>
//         public static UserStatus Generate(uint? emoteId = null, string content = null, ulong? expiresIn = 0) =>
//             Generate(emoteId, content is null ? null : new MessageContent(new TextContainer(content)), expiresIn);
//         /// <summary>
//         /// Generates user status for setting your custom status.
//         /// </summary>
//         /// <param name="emote">ID of the emote it should use</param>
//         /// <param name="content">Content of the status</param>
//         /// <param name="expiresIn">When it should expire in milliseconds</param>
//         /// <returns>Generated status</returns>
//         public static UserStatus Generate(BaseEmote emote, MessageContent content = null, ulong? expiresIn = 0) =>
//             Generate(emote.Id, content, expiresIn);
//         /// <summary>
//         /// Generates user status for setting your custom status.
//         /// </summary>
//         /// <param name="emote">ID of the emote it should use</param>
//         /// <param name="content">Content of the status</param>
//         /// <param name="expiresIn">When it should expire in milliseconds</param>
//         /// <returns>Generated status</returns>
//         public static UserStatus Generate(BaseEmote emote, string content = null, ulong? expiresIn = 0) =>
//             Generate(emote.Id, content, expiresIn);
//     }
// }