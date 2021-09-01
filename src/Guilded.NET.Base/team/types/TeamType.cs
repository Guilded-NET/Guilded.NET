// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;

// namespace Guilded.NET.Base.Teams
// {
//     /// <summary>
//     /// A type of a team(clan, community, guild, etc.).
//     /// </summary>
//     /// <seealso cref="ChannelType"/>
//     /// <seealso cref="SectionType"/>
//     [JsonConverter(typeof(StringEnumConverter), true)]
//     public enum TeamType
//     {
//         /// <summary>
//         /// A small team of a certain project or organization.
//         /// </summary>
//         Team,
//         /// <summary>
//         /// Organization of any kind.
//         /// </summary>
//         Organization,
//         /// <summary>
//         /// A community centered around certain topic, game or project.
//         /// </summary>
//         Community,
//         /// <summary>
//         /// A clan for a game.
//         /// </summary>
//         Clan,
//         /// <summary>
//         /// A guild for a game.
//         /// </summary>
//         Guild,
//         /// <summary>
//         /// A server for circle of friends.
//         /// </summary>
//         Friends,
//         /// <summary>
//         /// A server related to streaming/streamer.
//         /// </summary>
//         Streaming,
//         /// <summary>
//         /// Any other type of the server.
//         /// </summary>
//         Other
//     }
// }