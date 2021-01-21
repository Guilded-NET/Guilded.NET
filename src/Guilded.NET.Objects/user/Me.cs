using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    using Teams;
    using Converters;
    /// <summary>
    /// Information about this user.
    /// </summary>
    public class Me: ClientObject {
        /// <summary>
        /// List of teams/guilds/server this user is currently in.
        /// </summary>
        /// <value>List of teams</value>
        [JsonProperty("teams")]
        public IList<UserTeam> Teams {
            get; set;
        }
        /// <summary>
        /// The user itself.
        /// </summary>
        /// <value>User</value>
        [JsonProperty("user")]
        public ThisUser User {
            get; set;
        }
        /// <summary>
        /// I don't know what this is, honestly.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty("updateMessage")]
        public string UpdateMessage {
            get; set;
        }
        /// <summary>
        /// Custom emotes which can be used in Guilded by this user.
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(FlatConverter))]
        [JsonProperty("customReactions")]
        public IList<ChatEmote> CustomEmotes {
            get; set;
        }
        /// <summary>
        /// How many times these emotes have been used.
        /// </summary>
        /// <value></value>
        [JsonProperty("reactionUsages")]
        public IList<EmoteUse> EmoteUses {
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