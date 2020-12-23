using System.Threading.Tasks;
using System.Collections.Generic;

namespace Guilded.NET.Util {
    using Objects.Teams;
    using Objects.Chat;
    using System.Linq;
    using Objects.Permissions;
    using Guilded.NET.Objects;
    using System.Threading;

    /// <summary>
    /// Utilities for team related things.
    /// </summary>
    public static class TeamUtil {
        /// <summary>
        /// Gets team channels.
        /// </summary>
        /// <param name="team">Team itself</param>
        /// <param name="client">Client to get channels with</param>
        public static async Task<Channels> GetChannelsAsync(this Team team) =>
            await team.ParentClient.GetChannelsAsync(team.Id);
        /// <summary>
        /// Gets team channels. Sync version of <see cref="GetChannelsAsync"/>.
        /// </summary>
        /// <param name="team">Team itself</param>
        /// <param name="client">Client to get channels with</param>
        public static Channels GetChannels(this Team team) =>
            team.ParentClient.GetChannels(team.Id);
        /// <summary>
        /// Gets team groups.
        /// </summary>
        /// <param name="team">Team itself</param>
        /// <param name="client">Client to get groups with</param>
        public static async Task<IList<Group>> GetGroupsAsync(this Team team) =>
            await team.ParentClient.GetGroupsAsync(team.Id);
        /// <summary>
        /// Gets team groups. Sync version of <see cref="GetGroupsAsync"/>.
        /// </summary>
        /// <param name="team">Team itself</param>
        /// <param name="client">Client to get groups with</param>
        public static IList<Group> GetGroups(this Team team) =>
            team.ParentClient.GetGroups(team.Id);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <param name="team">Team to get owner from</param>
        /// <returns>Team owner</returns>
        public static async Task<User> GetOwnerAsync(this Team team) =>
            await team.ParentClient.GetUserAsync(team.OwnerId);
        /// <summary>
        /// Gets owner of this team as an user.
        /// </summary>
        /// <param name="team">Team to get owner from</param>
        /// <returns>Team owner</returns>
        public static User GetOwner(this Team team) =>
            team.ParentClient.GetUser(team.OwnerId);
        /// <summary>
        /// If the team has a member with given ID.
        /// </summary>
        /// <param name="team">Team to check if it has a member</param>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Has a member with given ID</returns>
        public static bool HasMember(this Team team, GId memberId) =>
            team.Members.FirstOrDefault(x => x.Id == memberId) != null;
        /// <summary>
        /// Creates a mentioned based on a given role.
        /// </summary>
        /// <param name="role">Role to mention</param>
        /// <returns>Role mention</returns>
        public static Mention CreateMention(this TeamRole role) =>
            Mention.Generate(role);
    }
}