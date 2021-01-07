using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Interface for team channels and categories.
    /// </summary>
    /// <typeparam name="T">Type of channel's ID</typeparam>
    public abstract class TeamChannel<T>: BaseChannel<T> {
        /// <summary>
        /// Priority/sort index of this channel.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority", Required = Required.AllowNull)]
        public long? Priority {
            get; set;
        }
        /// <summary>
        /// Name of this channel.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Permissions of the roles in this channel.
        /// </summary>
        /// <value>Role Permissions</value>
        [JsonProperty("rolesById", Required = Required.Always)]
        public IDictionary<string, ChannelPermission> RolePermissions {
            get; set;
        }
        /// <summary>
        /// Permissions of the users in this channel.
        /// </summary>
        /// <value>User Permissions</value>
        [JsonProperty("userPermissions", Required = Required.AllowNull)]
        public IList<UserPermission> UserPermissions {
            get; set;
        }
        /// <summary>
        /// ID of team this channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the group this channel is in.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId", Required = Required.Always)]
        public GId GroupId {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamAsync() =>
            await ParentClient.GetTeamAsync(TeamId);
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <returns>Team</returns>
        public Team GetTeam() =>
            ParentClient.GetTeam(TeamId);
        
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <returns>Team</returns>
        public async Task<Group> GetGroupAsync() =>
            (await ParentClient.GetGroupsAsync(TeamId)).FirstOrDefault(x => x.Id == GroupId);
        /// <summary>
        /// Gets the team of a channel.
        /// </summary>
        /// <returns>Team</returns>
        public Group GetGroup() =>
            ParentClient.GetGroups(TeamId).FirstOrDefault(x => x.Id == GroupId);

        //=========================//
        //    Overrides
        //=========================//

        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is TeamChannel<T> ch) return ch.TeamId == TeamId && Equals(ch.Id, Id);
            else return false;
        }
        /// <summary>
        /// Whether or not channels are equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(TeamChannel<T> ch0, TeamChannel<T> ch1) => ch0.TeamId == ch1.TeamId && Equals(ch0.Id, ch1.Id);
        /// <summary>
        /// Whether or not channels are not equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(TeamChannel<T> ch0, TeamChannel<T> ch1) => !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (TeamId.GetHashCode() + Id.GetHashCode() + 2000) / 2;
    }
}