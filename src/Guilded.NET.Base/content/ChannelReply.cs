// using System.ComponentModel;
// using System.Threading.Tasks;
// using System.Collections.Generic;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Content
// {
//     using Teams;
//     /// <summary>
//     /// A reply to a forum post, media, a document or an announcement.
//     /// </summary>
//     public class ChannelReply : Reply
//     {
//         /// <summary>
//         /// Reactions in this reply.
//         /// </summary>
//         /// <value>Reactions</value>
//         public IList<Reaction> Reactions
//         {
//             get; set;
//         } = new List<Reaction>();
//         /// <summary>
//         /// A team this channel reply is in.
//         /// </summary>
//         /// <value>Team ID</value>
//         public GId TeamId
//         {
//             get; set;
//         }
//         /*/// <summary>
//         /// Gets team where this reply is in.
//         /// </summary>
//         /// <returns>Team</returns>
//         public async Task<Team> GetTeamAsync() =>
//             TeamId is null ? null : await ParentClient.GetTeamAsync(TeamId);
//         /// <summary>
//         /// Gets team where this reply is in.
//         /// </summary>
//         /// <returns>Team info</returns>
//         public async Task<Team> GetTeamInfoAsync() =>
//             TeamId is null ? null : await ParentClient.GetTeamInfoAsync(TeamId);*/
//     }
// }