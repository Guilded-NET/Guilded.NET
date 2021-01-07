using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using Guilded.NET.Objects.Converters;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Guilded team/guild/server.
    /// </summary>
    public class Team: ClientObject {
        //=======================//
        //   Team IDs
        //=======================//
        
        /// <summary>
        /// ID of the team.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public GId Id {
            get; set;
        }
        /// <summary>
        /// ID of the owner.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("ownerId", Required = Required.Always)]
        public GId OwnerId {
            get; set;
        }
        //=======================//
        //   Name and Info
        //=======================//
        
        /// <summary>
        /// Name of the team.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Team subdomain used in a unique link.
        /// </summary>
        /// <value>Subdomain</value>
        [JsonProperty("subdomain", Required = Required.Always)]
        public string Subdomain {
            get; set;
        }
        /// <summary>
        /// Team's "biography".
        /// </summary>
        /// <value>Biography</value>
        [JsonProperty("bio", Required = Required.AllowNull)]
        public string Bio {
            get; set;
        }
        /// <summary>
        /// Description of this team.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("bio", Required = Required.AllowNull)]
        public string Description {
            get; set;
        }
        //=======================//
        //   Icons & Banners
        //=======================//
        
        /// <summary>
        /// URL of team's avatar.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("profilePicture")]
        public Uri Avatar {
            get; set;
        }
        /// <summary>
        /// URL of team's dash image.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("teamDashImage", Required = Required.AllowNull)]
        public Uri DashImage {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(small version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageSm")]
        public Uri HomeBannerSmall {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(medium version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageMd")]
        public Uri HomeBannerMedium {
            get; set;
        }
        /// <summary>
        /// URL of team's banner(large version).
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("homeBannerImageLarge")]
        public Uri HomeBannerLarge {
            get; set;
        }
        //=======================//
        //   Is...
        //=======================//

        /// <summary>
        /// Whether the team is recruiting or not.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isRecruiting", Required = Required.Always)]
        public bool IsRecruiting {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is verified.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isVerified", Required = Required.Always)]
        public bool IsVerified {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is public.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPublic", Required = Required.Always)]
        public bool IsPublic {
            get; set;
        }
        /// <summary>
        /// Whether or not the team is pro(has paid?).
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPro", Required = Required.Always)]
        public bool IsPro {
            get; set;
        }
        /// <summary>
        /// If this user is following this team.
        /// </summary>
        /// <value>Following</value>
        [JsonProperty("userFollowsTeam", Required = Required.Always)]
        public bool IsFollowing {
            get; set;
        }
        /// <summary>
        /// If this user applied for this server.
        /// </summary>
        /// <value>Applied</value>
        [JsonProperty("userIsApplication", Required = Required.Always)]
        public bool IsApplicant {
            get; set;
        }
        /// <summary>
        /// If this user was invited.
        /// </summary>
        /// <value>Invited</value>
        [JsonProperty("isUserInvited", Required = Required.Always)]
        public bool IsInvited {
            get; set;
        }
        /// <summary>
        /// If this used was banned from this team.
        /// </summary>
        /// <value>Banned</value>
        [JsonProperty("isUserBannedFromTeam", Required = Required.Always)]
        public bool IsBanned {
            get; set;
        }
        //=======================//
        //   Discord
        //=======================//

        /// <summary>
        /// If it should sync Discord roles.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("autoSyncDiscordRoles", Required = Required.Always)]
        public bool AutoSyncDiscordRoles {
            get; set;
        }
        /// <summary>
        /// If this team has imported a Discord server through Discord integration.
        /// </summary>
        /// <value>Imported Discord server</value>
        [JsonProperty("hasImportedDiscordServer", Required = Required.Always)]
        public bool ImportedDiscord {
            get; set;
        }
        /// <summary>
        /// ID of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server ID</value>
        [JsonProperty("discordGuildId", Required = Required.AllowNull)]
        public ulong? DiscordId {
            get; set;
        }
        /// <summary>
        /// Name of the Discord server this team has imported.
        /// </summary>
        /// <value>(Nullable) Discord Server Name</value>
        [JsonProperty("discordServerName", Required = Required.AllowNull)]
        public string DiscordName {
            get; set;
        }
        //=======================//
        //   Members & Followers
        //=======================//

        /// <summary>
        /// All roles in this team.
        /// </summary>
        /// <value>IDictionary of role id, role</value>
        [JsonProperty("rolesById", Required = Required.Always)]
        public IDictionary<string, TeamRole> RolesById {
            get; set;
        }
        /// <summary>
        /// All of the members in the team.
        /// </summary>
        /// <value>List of members or list of list of members</value>
        [JsonConverter(typeof(FlatConverter))]
        [JsonProperty("members", Required = Required.Always)]
        public IList<TeamMember> Members {
            get; set;
        }
        /// <summary>
        /// Count of various things in this team.
        /// </summary>
        /// <value>Team measurements</value>
        [JsonProperty("measurements", Required = Required.Always)]
        public TeamMeasurements Measurements {
            get; set;
        }
        //======================//
        //   Team features
        //=======================//

        /// <summary>
        /// A main/home group in this team.
        /// </summary>
        /// <value>Group</value>
        [JsonProperty("baseGroup", Required = Required.Always)]
        public Group BaseGroup {
            get; set;
        }
        /// <summary>
        /// A list of all webhooks in this team.
        /// </summary>
        /// <value>List of webhooks</value>
        [JsonProperty("webhooks", Required = Required.Always)]
        public IList<Webhook> Webhooks {
            get; set;
        }
        /// <summary>
        /// All games in this team.
        /// </summary>
        /// <value>Team game IDs</value>
        [JsonProperty("games", Required = Required.Always)]
        public IList<uint> Games {
            get; set;
        }
        //======================//
        //   Other
        //=======================//
        
        /// <summary>
        /// Team's selected timezone.
        /// </summary>
        /// <value>Timezone</value>
        [JsonProperty("timezone", Required = Required.AllowNull)]
        public string Timezone {
            get; set;
        }
        //=====================//
        //   Methods
        //=====================//
        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <returns>Channel, category and thread list</returns>
        public async Task<Channels> GetChannelsAsync() =>
            await ParentClient.GetChannelsAsync(Id);
        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <returns>Channel, category and thread list</returns>
        public Channels GetChannels() =>
            ParentClient.GetChannels(Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <returns>Group list</returns>
        public async Task<IList<Group>> GetGroupsAsync() =>
            await ParentClient.GetGroupsAsync(Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <returns>Group list</returns>
        public IList<Group> GetGroups() =>
            ParentClient.GetGroups(Id);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <returns>Team owner</returns>
        public async Task<User> GetOwnerAsync() =>
            await ParentClient.GetUserAsync(OwnerId);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <returns>Team owner</returns>
        public User GetOwner() =>
            ParentClient.GetUser(OwnerId);
        /// <summary>
        /// If the team has a member with given ID.
        /// </summary>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Has a member with given ID</returns>
        public bool HasMember(GId memberId) =>
            Members.FirstOrDefault(x => x.Id == memberId) != null;

        /// <summary>
        /// Turns team to string.
        /// </summary>
        /// <returns>Team as a string</returns>
        public override string ToString() => $"Team({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is Team team) return team.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not teams are equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(Team tm0, Team tm1) => tm0.Id == tm1.Id;
        /// <summary>
        /// Whether or not teams are not equal.
        /// </summary>
        /// <param name="tm0">First team to be compared</param>
        /// <param name="tm1">Second team to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(Team tm0, Team tm1) => !(tm0 == tm1);
        /// <summary>
        /// Gets team hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() + 300;
    }
}