using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    using Teams;
    /// <summary>
    /// A data of mention node.
    /// </summary>
    public class MentionData: BaseObject, IMention {
        /// <summary>
        /// Type of the mention.
        /// </summary>
        /// <value>person, here, everyone, role</value>
        [JsonProperty("type")]
        public string Type {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("matcher", Required = Required.Always)]
        public string Matcher {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// ID of the mention.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("id")]
        public string Id {
            get; set;
        }
        /// <summary>
        /// Description of the mention.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// Avatar of the user being mentioned.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// Colour of the mention.
        /// </summary>
        /// <value>Hex colour</value>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color {
            get; set;
        }
        /// <summary>
        /// Whether or not the Mentioned has a nickname.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("nickname", NullValueHandling = NullValueHandling.Ignore)]
        public bool Nickname {
            get; set;
        }
        /// <summary>
        /// Generates user mention for DMs.
        /// </summary>
        /// <param name="user">User to mention</param>
        /// <returns>Mention data</returns>
        public static MentionData Generate(BaseUser user) =>
            new MentionData {
                Type = "person",
                Matcher = "@" + user.Username.ToLower(),
                Color = null,
                Name = user.Username,
                Id = user.Id.ToString(),
                Avatar = user.ProfilePicture,
                Nickname = false
            };
        /// <summary>
        /// Generates member mention.
        /// </summary>
        /// <param name="member">User to mention</param>
        /// <param name="colour">Colour of the mention</param>
        /// <returns>Mention data</returns>
        public static MentionData Generate(TeamMember member, string colour) =>
            new MentionData {
                Type = "person",
                Matcher = "@" + member.Username.ToLower() + (member.Nickname != null ? $" @{member.Nickname}" : ""),
                Name = member.Nickname ?? member.Username,
                Color = colour,
                Id = member.Id.ToString(),
                Avatar = member.ProfilePicture,
                Nickname = member.Nickname != null
            };
        /// <summary>
        /// Generates @everyone mention.
        /// </summary>
        /// <returns>Mention data</returns>
        public static MentionData GenerateEveryone() =>
            new MentionData {
                Type = "everyone",
                Matcher = "@everyone",
                Color = "#ffffff",
                Name = "everyone",
                Id = "everyone",
                Description = "Notify everyone in the channel"
            };
        /// <summary>
        /// Generates @here mention.
        /// </summary>
        /// <returns>Mention data</returns>
        public static MentionData GenerateHere() =>
            new MentionData {
                Type = "here",
                Matcher = "@here",
                Color = "#f5c400",
                Name = "here",
                Id = "here",
                Description = "Notify everyone in this channel that is online and not idle"
            };
        /// <summary>
        /// Generates role mention.
        /// </summary>
        /// <param name="role">Role to mention</param>
        /// <returns>Mention data</returns>
        public static MentionData Generate(TeamRole role) =>
            new MentionData {
                Type = "role",
                Matcher = "@" + role.Name.ToLower(),
                Name = role.Name,
                Color = $"#{role.Color}",
                Id = role.Id.ToString()
            };
    }
}