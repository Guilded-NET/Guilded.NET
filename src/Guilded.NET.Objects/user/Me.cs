using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Objects {
    using Converters;

    using Teams;
    /// <summary>
    /// Information about this user.
    /// </summary>
    public class Me: ClientObject {
        /// <summary>
        /// List of teams/guilds/server this user is currently in.
        /// </summary>
        /// <value>List of teams</value>
        [JsonProperty("teams", Required = Required.Always)]
        public IList<BaseTeam> Teams {
            get; set;
        }
        /// <summary>
        /// The user itself.
        /// </summary>
        /// <value>User</value>
        [JsonProperty("user", Required = Required.Always)]
        public ThisUser User {
            get; set;
        }
        /// <summary>
        /// Message of the latest update on Guilded.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("updateMessage", Required = Required.AllowNull)]
        public JObject UpdateMessage {
            get; set;
        }
        /// <summary>
        /// Custom emotes which can be used in Guilded by this user.
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(FlatConverter))]
        [JsonProperty("customReactions", Required = Required.Always)]
        public IList<ChatEmote> CustomEmotes {
            get; set;
        }
        /// <summary>
        /// How many times these emotes have been used.
        /// </summary>
        /// <value></value>
        [JsonProperty("reactionUsages", Required = Required.Always)]
        public IList<EmoteUse> EmoteUses {
            get; set;
        }
        /// <summary>
        /// A list of friends of this user, friend requests sent by user and friend requests sent to user.
        /// </summary>
        /// <value>List of friends</value>
        [JsonProperty("friends", Required = Required.Always)]
        public IList<Friend> Friends {
            get; set;
        }
        /// <summary>
        /// Username of this user.
        /// </summary>
        /// <value>Name</value>
        public string Username {
            get => User.Username;
        }
        /// <summary>
        /// ID of this user.
        /// </summary>
        /// <value>User ID</value>
        public GId Id {
            get => User.Id;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public async Task ChangeNameAsync(string name) =>
            await ParentClient.ChangeNameAsync(name);
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public void ChangeName(string name) =>
            ParentClient.ChangeName(name);
        /// <summary>
        /// Checks if this user is in a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Is in a team with given ID</returns>
        public bool InTeam(GId teamId) =>
            Teams.FirstOrDefault(x => x.Id == teamId) != null;
    }
}