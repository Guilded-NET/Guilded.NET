// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Users
// {
//     using Content;
//     /// <summary>
//     /// A base for all users.
//     /// </summary>
//     public class BaseUser : ClientObject
//     {
//         /// <summary>
//         /// Given ID of the user.
//         /// </summary>
//         [JsonProperty(Required = Required.Always)]
//         public GId Id
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Current name of the user.
//         /// </summary>
//         [JsonProperty("name", Required = Required.Always)]
//         public string Username
//         {
//             get; set;
//         }
//         /// <summary>
//         /// User's current profile picture.
//         /// </summary>
//         public Uri ProfilePicture
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Blurry version of profile banner.
//         /// </summary>
//         [JsonProperty("profileBannerBlur")]
//         public Uri ProfileBannerBlurry
//         {
//             get; set;
//         }

//         /*/// <summary>
//         /// Gets this member as a user.
//         /// </summary>
//         /// <returns>User</returns>
//         public async Task<User> AsUserAsync() =>
//             await ParentClient.GetUserAsync(Id);
//         /// <summary>
//         /// Creates a new DM channel.
//         /// </summary>
//         /// <returns>Channel</returns>
//         public async Task<DMChannel> CreateDMAsync() =>
//             await ParentClient.CreateDMChannelAsync(Id);
//         /// <summary>
//         /// Gets a profile of a user.
//         /// </summary>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         /// <returns>User profile</returns>
//         public async Task<ProfileUser> GetProfileAsync() =>
//             await ParentClient.GetProfileAsync(Id);
//         /// <summary>
//         /// Gets a set amount of posts in user's profile.
//         /// </summary>
//         /// <param name="maxPosts">How many posts it should get</param>
//         /// <param name="offset">At which index it should start getting posts</param>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         /// <returns>List of posts</returns>
//         public async Task<IList<ProfilePost>> GetProfilePostsAsync(uint maxPosts = 7, uint offset = 0) =>
//             await ParentClient.GetProfilePostsAsync(Id, maxPosts, offset);
//         /// <summary>
//         /// Gets a specific post in user's profile.
//         /// </summary>
//         /// <param name="postId">ID of the post to get</param>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         /// <returns>Profile post</returns>
//         public async Task<ProfilePost> GetProfilePostAsync(uint postId) =>
//             await ParentClient.GetProfilePostAsync(Id, postId);

//         /// <summary>
//         /// Kicks a member from a server.
//         /// </summary>
//         /// <param name="teamId">ID of the team to kick user from</param>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         public async Task KickAsync(GId teamId) =>
//             await ParentClient.KickMemberAsync(teamId, Id);
//         /// <summary>
//         /// Bans this member from a team.
//         /// </summary>
//         /// <param name="teamId">ID of the team to ban user in</param>
//         /// <param name="reason">Reason for banning this user</param>
//         /// <param name="deleteHistoryOption">Either 7(for 1 week) or 24(for 1 day)</param>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         public async Task BanAsync(GId teamId, string reason, uint deleteHistoryOption) =>
//             await ParentClient.BanMemberAsync(teamId, Id, reason, deleteHistoryOption);
//         /// <summary>
//         /// Unbans this member in a team.
//         /// </summary>
//         /// <param name="teamId">ID of the team to unban user from</param>
//         /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
//         public async Task UnbanAsync(GId teamId) =>
//             await ParentClient.UnbanMemberAsync(teamId, Id);*/
//     }
// }