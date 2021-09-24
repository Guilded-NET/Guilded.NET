// using System;
// using System.Drawing;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Teams
// {
//     using Permissions;
//     /// <summary>
//     /// Represents role in teams.
//     /// </summary>
//     public class TeamRole : ClientObject
//     {
//         /// <summary>
//         /// ID of the role.
//         /// </summary>
//         /// <value>Role ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public uint Id
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Name of the role.
//         /// </summary>
//         /// <value>Name</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Name
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Colour of the role.
//         /// </summary>
//         /// <value>Hex colour</value>
//         [JsonProperty(Required = Required.Always)]
//         public Color Color
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Priority of the role.
//         /// </summary>
//         /// <value>Priority</value>
//         public long? Priority
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Whether it's a base role/default role.
//         /// </summary>
//         /// <value>Boolean</value>
//         [JsonProperty("isBase", Required = Required.Always)]
//         public bool BaseRole
//         {
//             get; set;
//         }
//         /// <summary>
//         /// ID of the team this role is in.
//         /// </summary>
//         /// <value>Team ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public GId TeamId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When it was created.
//         /// </summary>
//         /// <value>Date</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime CreatedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// When it was last updated.
//         /// </summary>
//         /// <value>Date</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime UpdatedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Whether it should be mentionable.
//         /// </summary>
//         /// <value></value>
//         [JsonProperty("isMentionable", Required = Required.Always)]
//         public bool Mentionable
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Whether it should be self assignable.
//         /// </summary>
//         /// <value></value>
//         [JsonProperty("isSelfAssignable", Required = Required.Always)]
//         public bool SelfAssignable
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Whether it should be displayed seperately from other roles.
//         /// </summary>
//         /// <value>Boolean</value>
//         [JsonProperty("isDisplayedSeparately", Required = Required.Always)]
//         public bool DisplayedSeparately
//         {
//             get; set;
//         }
//         /// <summary>
//         /// ID of the Discord role it's synced with.
//         /// </summary>
//         /// <value>Discord role ID</value>
//         public ulong? DiscordRoleId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Permissions of this role.
//         /// </summary>
//         /// <value>Permissions</value>
//         [JsonProperty(Required = Required.Always)]
//         public PermissionList Permissions
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Creates a mentioned based on this role.
//         /// </summary>
//         /// <returns>Role mention</returns>
//         public MemberMention CreateMention() =>
//             new MemberMention(this);
//     }
// }