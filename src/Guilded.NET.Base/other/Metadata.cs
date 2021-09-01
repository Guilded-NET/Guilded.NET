// using System;
// using System.Text;
// using System.Collections.Generic;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base
// {
//     using Chat;
//     using Teams;
//     using Users;
//     using Content;
//     /// <summary>
//     /// Metadata of given route.
//     /// </summary>
//     public class Metadata : BaseObject
//     {
//         #region General
//         /// <summary>
//         /// Title of the route's metadata.
//         /// </summary>
//         /// <value>Title</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Title
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Description of the route's metadata.
//         /// </summary>
//         /// <value>Description</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Description
//         {
//             get; set;
//         }
//         #endregion

//         #region Route References

//         #region Team
//         /// <summary>
//         /// The team referenced in the route.
//         /// </summary>
//         /// <value>Team</value>
//         public Team Team
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The invite referenced in the route.
//         /// </summary>
//         /// <value>Invite information</value>
//         [JsonProperty("inviteInfo")]
//         public MetadataInvite Invite
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The group referenced in the route.
//         /// </summary>
//         /// <value>Group</value>
//         public Group Group
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The channel referenced in the route.
//         /// </summary>
//         /// <value>Channel | ThreadChannel</value>
//         public TeamChannel Channel
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The user's profile referenced in the route.
//         /// </summary>
//         /// <value>User</value>
//         public User User
//         {
//             get; set;
//         }
//         #endregion

//         #endregion

//         #region Misc
//         /// <summary>
//         /// The identifier of users who are part of the metadata.
//         /// </summary>
//         /// <value>List of user IDs</value>
//         public IList<GId> UserIds
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Similar URL that could be used.
//         /// </summary>
//         /// <value>URL path</value>
//         [JsonProperty(Required = Required.Always)]
//         public string CanonicalUrl
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The original URL used for the route.
//         /// </summary>
//         /// <value>URL path</value>
//         [JsonProperty(Required = Required.Always)]
//         public string OriginalUrl
//         {
//             get; set;
//         }
//         #endregion
//     }
// }