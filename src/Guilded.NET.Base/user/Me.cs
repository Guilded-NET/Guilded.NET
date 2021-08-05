using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Guilded.NET.Base.Users
{
    using Teams;
    /// <summary>
    /// Information about this user.
    /// </summary>
    public class Me : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// List of teams/guilds/server this user is currently in.
        /// </summary>
        /// <value>List of teams</value>
        [JsonProperty(Required = Required.Always)]
        public IList<BaseTeam> Teams
        {
            get; set;
        }
        /// <summary>
        /// The user itself.
        /// </summary>
        /// <value>User</value>
        [JsonProperty(Required = Required.Always)]
        public ThisUser User
        {
            get; set;
        }
        /// <summary>
        /// Message of the latest update on Guilded.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty(Required = Required.AllowNull)]
        public UpdateMessage UpdateMessage
        {
            get; set;
        }
        /// <summary>
        /// Custom emotes which can be used in Guilded by this user.
        /// </summary>
        /// <value>List of emotes</value>
        [JsonProperty("customReactions", Required = Required.Always)]
        public IList<BaseEmote> CustomEmotes
        {
            get; set;
        }
        /// <summary>
        /// How many times these emotes have been used.
        /// </summary>
        /// <value></value>
        [JsonProperty("reactionUsages", Required = Required.Always)]
        public IList<EmoteUse> EmoteUses
        {
            get; set;
        }
        /// <summary>
        /// A list of friends of this user, friend requests sent by user and friend requests sent to user.
        /// </summary>
        /// <value>List of friends</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Friend> Friends
        {
            get; set;
        }
        #endregion


        #region Additional
        /// <summary>
        /// Username of this user.
        /// </summary>
        /// <value>Name</value>
        public string Username => User.Username;
        /// <summary>
        /// ID of this user.
        /// </summary>
        /// <value>User ID</value>
        public GId Id => User.Id;
        /// <summary>
        /// Count of how many teams this client is in.
        /// </summary>
        /// <value>Count</value>
        public int TeamCount => Teams.Count;
        
        /*/// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public async Task ChangeNameAsync(string name) =>
            await ParentClient.ChangeNameAsync(name);*/
        /// <summary>
        /// Checks if this user is in a specific team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Is in a team with given ID</returns>
        public bool InTeam(GId teamId) =>
            !(Teams.FirstOrDefault(x => x.Id == teamId) is null);
        #endregion
    }
}