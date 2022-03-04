// namespace Guilded.Base.Users
// {
//     /// <summary>
//     /// Information about this user.
//     /// </summary>
//     public class Me : ClientObject
//     {
//         #region JSON properties
//         /// <summary>
//         /// List of teams/guilds/server this user is currently in.
//         /// </summary>
//         /// <value>List of teams</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<BaseTeam> Teams { get; }
//         /// <summary>
//         /// The user itself.
//         /// </summary>
//         /// <value>User</value>
//         [JsonProperty(Required = Required.Always)]
//         public ThisUser User { get; }
//         /// <summary>
//         /// Custom emotes which can be used in Guilded by this user.
//         /// </summary>
//         /// <value>List of emotes</value>
//         [JsonProperty("customReactions", Required = Required.Always)]
//         public IList<BaseEmote> CustomEmotes { get; }
//         /// <summary>
//         /// How many times these emotes have been used.
//         /// </summary>
//         /// <value></value>
//         [JsonProperty("reactionUsages", Required = Required.Always)]
//         public IList<EmoteUse> EmoteUses { get; }
//         /// <summary>
//         /// A list of friends of this user, friend requests sent by user and friend requests sent to user.
//         /// </summary>
//         /// <value>List of friends</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<Friend> Friends { get; }
//         #endregion

//         #region Additional
//         /// <summary>
//         /// Username of this user.
//         /// </summary>
//         /// <value>Name</value>
//         [JsonIgnore]
//         public string Username => User.Username;
//         /// <summary>
//         /// ID of this user.
//         /// </summary>
//         /// <value>User ID</value>
//         [JsonIgnore]
//         public Hashid Id => User.Id;
//         /// <summary>
//         /// Count of how many teams this client is in.
//         /// </summary>
//         /// <value>Count</value>
//         [JsonIgnore]
//         public int TeamCount => Teams.Count;

//         /// <summary>
//         /// Checks if this user is in a specific team.
//         /// </summary>
//         /// <param name="teamId">ID of the team</param>
//         /// <returns>Is in a team with given ID</returns>
//         public bool InTeam(HashId teamId) =>
//             Teams.Any(team => team.Id == teamId);
//         #endregion
//     }
// }