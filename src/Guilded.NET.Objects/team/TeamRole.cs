using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Represents role in teams.
    /// </summary>
    public class TeamRole: ClientObject {
        /// <summary>
        /// ID of the role.
        /// </summary>
        /// <value>Role ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id {
            get; set;
        }
        /// <summary>
        /// Name of the role.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Colour of the role.
        /// </summary>
        /// <value>Hex colour</value>
        [JsonProperty("color", Required = Required.AllowNull)]
        public string Color {
            get; set;
        } = null;
        /// <summary>
        /// Priority of the role.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority")]
        public uint? Priority {
            get; set;
        } = null;
        /// <summary>
        /// Whether or not it's a base role/default role.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isBase", Required = Required.Always)]
        public bool BaseRole {
            get; set;
        } = false;
        /// <summary>
        /// ID of the team this role is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// When it was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When it was last updated.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("updatedAt", Required = Required.Always)]
        public DateTime UpdatedAt {
            get; set;
        }
        /// <summary>
        /// Whether it should be mentionable.
        /// </summary>
        /// <value></value>
        [JsonProperty("isMentionable", Required = Required.Always)]
        public bool Mentionable {
            get; set;
        } = false;
        /// <summary>
        /// Whether it should be self assignable.
        /// </summary>
        /// <value></value>
        [JsonProperty("isSelfAssignable", Required = Required.Always)]
        public bool SelfAssignable {
            get; set;
        } = false;
        /// <summary>
        /// Whether it should be displayed seperately from other roles.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isDisplayedSeparately", Required = Required.Always)]
        public bool DisplayedSeparately {
            get; set;
        }
        /// <summary>
        /// ID of the Discord role it's synced with.
        /// </summary>
        /// <value>Discord role ID</value>
        [JsonProperty("discordRoleId")]
        public ulong? DiscordRoleId {
            get; set;
        }
        /// <summary>
        /// Permissions of this role.
        /// </summary>
        /// <value>Permissions</value>
        [JsonProperty("permissions", Required = Required.Always)]
        public PermissionList Permissions {
            get; set;
        }
    }
}