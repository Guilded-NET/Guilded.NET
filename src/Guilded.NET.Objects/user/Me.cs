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
        /// <summary>
        /// Returns every emote which contains given parts in the name. Alternative to Regex.
        /// </summary>
        /// <param name="start">The start of the string it should match</param>
        /// <param name="end">The end of the string it should match</param>
        /// <param name="contains">Every part in the string it should contain</param>
        /// <returns>Emotes which have given parts in their names</returns>
        /// <example>
        /// Emotes: 
        /// `abcdefghi`, `defghi`, `abc`
        /// <code>
        /// EmotesWith(start: "abc")
        /// // Matches: `abcdefghi`, `abc`
        /// </code>
        /// <code>
        /// EmotesWith(end: "ghi")
        /// // Matches: `abcdefghi`, `defghi`
        /// </code>
        /// <code>
        /// EmotesWith("bc")
        /// // Matches: `abcdefghi`, `abc`
        /// </code>
        /// <code>
        /// // Emote which starts with `a`, ends with `i`, has `d` and `e`
        /// EmotesWith(start: "a", end: "i", "d", "e")
        /// // Matches: `abcdefghi`
        /// </code>
        /// </example>
        public IList<ChatEmote> EmotesWith(string start = null, string end = null, params string[] contains) =>
            CustomEmotes
            // Checks if emote's name starts with given start and end
            .Where(x =>
                x.Name.StartsWith(start ?? "") && x.Name.EndsWith(end ?? "")
            // Checks if emote's name contains every given string
            ).Where(x => {
                // Gets how many characters to trim at the start and at the end
                int startIndex = start?.Length ?? 0,
                    endCount = end?.Length ?? 0;
                // Trims the given start and end
                string trimmed = x.Name[startIndex..(x.Name.Length - endCount)];
                // Checks if trimmed includes every `contains` argument member and removes it
                foreach(string c in contains)
                    // If trimmed does not contain given string, return false
                    if(!trimmed.Contains(c)) return false;
                    // If it does, remove it from the string
                    else trimmed = trimmed.Remove(trimmed.IndexOf(c), c.Length);
                // If everything is in the emote's name, return true
                return true;
            }).ToList();
    }
}