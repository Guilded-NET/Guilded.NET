using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Users;
    /// <summary>
    /// A member in member list.
    /// </summary>
    public class Member : ClientObject
    {
        /// <summary>
        /// User ID of this member.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId Id
        {
            get; set;
        }
        /// <summary>
        /// The name of this user.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A profile picture of this user.
        /// </summary>
        /// <value>Profile picture?</value>
        public Uri ProfilePicture
        {
            get; set;
        }
        /// <summary>
        /// A nickname of this member.
        /// </summary>
        /// <value>Name?</value>
        public string Nickname
        {
            get; set;
        }
        /// <summary>
        /// A list of roles this user has.
        /// </summary>
        /// <value>List of role IDs</value>
        [JsonProperty("roleIds")]
        public IList<uint> Roles
        {
            get; set;
        } = new List<uint>();
        /// <summary>
        /// A list of global badges this user has.
        /// </summary>
        /// <value>List of global badges</value>
        public IList<GlobalBadge> Badges
        {
            get; set;
        } = new List<GlobalBadge>();
        /// <summary>
        /// A presence of this user.
        /// </summary>
        /// <value>User presence</value>
        [JsonProperty("userPresenceStatus")]
        public Presence? Presence
        {
            get; set;
        }
        /// <summary>
        /// A membership of this member. NOTE: If this value isn't null or admin, please contact Guilded.NET developer about this.
        /// </summary>
        /// <value>["admin", ???]?</value>
        [JsonProperty("membershipRole")]
        public string MembershipRole
        {
            get; set;
        }
        /// <summary>
        /// Gets a nickname if it exists. Else, it gets a username.
        /// </summary>
        /// <value>Name</value>
        [JsonIgnore]
        public string DisplayName => Nickname ?? Name;
        /// <summary>
        /// If this user is an admin of the server. This is seems to be true when the member is an owner of the team.
        /// </summary>
        /// <value>Member is owner</value>
        [JsonIgnore]
        public bool IsAdmin => MembershipRole == "admin";
        
        /*/// <summary>
        /// Bans the member from the team.
        /// </summary>
        /// <param name="teamId">ID of the team to ban member from</param>
        /// <param name="reason">Reason why the member got banned</param>
        /// <param name="deleteHistoryOption">How much of history should be deleted</param>
        public async Task BanAsync(GId teamId, string reason, uint deleteHistoryOption) =>
            await ParentClient.BanMemberAsync(teamId, Id, reason, deleteHistoryOption);
        /// <summary>
        /// Kicks the member from the team.
        /// </summary>
        /// <param name="teamId">ID of the team to kick member from</param>
        public async Task KickAsync(GId teamId) =>
            await ParentClient.KickMemberAsync(teamId, Id);
        /// <summary>
        /// Changes member's team-wide nickname.
        /// </summary>
        /// <param name="teamId">ID of the team to change nickname in</param>
        /// <param name="nickname">A new nickname of the member</param>
        public async Task SetNicknameAsync(GId teamId, string nickname) =>
            await ParentClient.SetNicknameAsync(teamId, Id, nickname);
        /// <summary>
        /// Gets the details of this member.
        /// </summary>
        /// <param name="teamId">ID of the team to get details from</param>
        /// <returns>Details</returns>
        public async Task<MemberDetails> GetDetailsAsync(GId teamId) =>
            (await ParentClient.GetMemberDetailsAsync(teamId, new GId[] { Id }))[Id];*/
    }
}