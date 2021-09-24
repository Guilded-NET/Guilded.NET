// using System;
// using System.Collections.Generic;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Serialization;

// namespace Guilded.NET.Base.Users
// {
//     /// <summary>
//     /// Guilded user. This is NOT Guild member.
//     /// </summary>
//     public class User : BaseUser
//     {
//         #region JSON properties
//         /// <summary>
//         /// A URL subdomain for this user.
//         /// </summary>
//         /// <value>Subdomain</value>
//         [JsonProperty(Required = Required.AllowNull)]
//         public string Subdomain
//         {
//             get; set;
//         }
//         public string Email
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Publicly available email.
//         /// </summary>
//         /// <remarks>
//         /// An email to your login/regular email that is available for public use. This is for contacting you.
//         /// </remarks>
//         /// <value>Email?</value>
//         public string ServiceEmail
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Large version of profile picture.
//         /// </summary>
//         [JsonProperty("profilePictureLg")]
//         public Uri ProfilePictureLarge
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Small version of profile picture.
//         /// </summary>
//         [JsonProperty("profilePictureSm")]
//         public Uri ProfilePictureSmall
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Blurry version of profile picture.
//         /// </summary>
//         [JsonProperty("profilePictureBlur")]
//         public Uri ProfilePictureBlurry
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Large version of profile banner.
//         /// </summary>
//         [JsonProperty("profileBannerLg")]
//         public Uri ProfileBannerLarge
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Small version of profile banner.
//         /// </summary>
//         [JsonProperty("profileBannerSm")]
//         public Uri ProfileBannerSmall
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The presence status of this user.
//         /// </summary>
//         /// <value>User presence?</value>
//         [JsonProperty("userPresenceStatus")]
//         public Presence? Presence
//         {
//             get; set;
//         }
//         /// <summary>
//         /// User status got updated.
//         /// </summary>
//         /// <value>User status</value>
//         [JsonProperty("userStatus")]
//         public UserStatus Status
//         {
//             get; set;
//         }
//         /// <summary>
//         /// User's steam ID.
//         /// </summary>
//         [JsonProperty(Required = Required.AllowNull)]
//         public string SteamId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Moderation status of the user. If this user is banned, it will return `banned`
//         /// </summary>
//         [JsonProperty(Required = Required.AllowNull)]
//         public string ModerationStatus
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Games this user has on their profile.
//         /// </summary>
//         /// <value>List of game aliases</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<GameAlias> Aliases
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Date of last time user was online.
//         /// </summary>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime LastOnline
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Date of user's registration.
//         /// </summary>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime JoinDate
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A list of all global badges this user has.
//         /// </summary>
//         /// <value>List of badges</value>
//         public IList<GlobalBadge> Badges
//         {
//             get; set;
//         } = new List<GlobalBadge>();
//         #endregion

//         #region Additional
//         /// <summary>
//         /// Creates a mention based on a user.
//         /// </summary>
//         /// <value>User mention</value>
//         [JsonIgnore]
//         public MemberMention Mention => new MemberMention(this);
//         /// <summary>
//         /// If this user is banned from Guilded's services.
//         /// </summary>
//         /// <value>Account terminated</value>
//         public bool IsBanned => ModerationStatus == "banned";
//         #endregion
//     }
// }