// using System;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// namespace Guilded.NET.Base.Users
// {
//     /// <summary>
//     /// A social media link.
//     /// </summary>
//     public class SocialLink : BaseObject
//     {
//         /// <summary>
//         /// Social media's name.
//         /// </summary>
//         /// <value>Social media name</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Type
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Handle/name of this user in this social media.
//         /// </summary>
//         /// <value>Social media handle</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Handle
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Profile picture of the user in that social media.
//         /// </summary>
//         /// <value>URL?</value>
//         public Uri ProfilePicture
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Additional information related to this social media link.
//         /// </summary>
//         /// <value>Additional information</value>
//         [JsonProperty(Required = Required.Always)]
//         public JObject AdditionalInfo
//         {
//             get; set;
//         }
//     }
// }