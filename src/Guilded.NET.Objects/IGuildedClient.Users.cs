using System.Threading.Tasks;
using System.Collections.Generic;

namespace Guilded.NET.Objects
{
    using Users;
    using Content;
    public partial interface IGuildedClient
    {
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Current User</returns>
        Task<Me> GetThisUserAsync();
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Current User</returns>
        Me GetThisUser();
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>Async task</returns>
        Task ChangeNameAsync(string name);
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        void ChangeName(string name);
        /// <summary>
        /// Changes user's presence.
        /// </summary>
        /// <param name="presence">New presence</param>
        /// <returns>Async task</returns>
        Task<object> ChangePresenceAsync(Presence presence);
        /// <summary>
        /// Changes user's presence.
        /// </summary>
        /// <param name="presence">New presence</param>
        void ChangePresence(Presence presence);
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <param name="status">New status</param>
        /// <returns>Async task</returns>
        Task ChangeStatusAsync(UserStatus status);
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <param name="status">New status</param>
        void ChangeStatus(UserStatus status);
        /// <summary>
        /// Gets all DM channels.
        /// </summary>
        /// <returns>Channel</returns>
        Task<IList<DMChannel>> GetDMChannelsAsync();
        /// <summary>
        /// Gets all DM channels.
        /// </summary>
        /// <returns>Channel</returns>
        IList<DMChannel> GetDMChannels();
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        Task<DMChannel> CreateDMChannelAsync(params GId[] users);
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        DMChannel CreateDMChannel(params GId[] users);
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(GId id);
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        User GetUser(GId id);
        /// <summary>
        /// Gets a profile of a user.
        /// </summary>
        /// <param name="userId">ID of the user to get profile of</param>
        /// <returns>User profile</returns>
        Task<ProfileUser> GetProfileAsync(GId userId);
        /// <summary>
        /// Gets a profile of a user.
        /// </summary>
        /// <param name="userId">ID of the user to get profile of</param>
        /// <returns>User profile</returns>
        ProfileUser GetProfile(GId userId);
        /// <summary>
        /// Gets a set amount of posts in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile posts from</param>
        /// <param name="maxPosts">How many posts it should get</param>
        /// <param name="offset">At which index it should start getting posts</param>
        /// <returns>List of posts</returns>
        Task<IList<ProfilePost>> GetProfilePostsAsync(GId userId, uint maxPosts, uint offset);
        /// <summary>
        /// Gets a set amount of posts in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile posts from</param>
        /// <param name="maxPosts">How many posts it should get</param>
        /// <param name="offset">At which index it should start getting posts</param>
        /// <returns>List of posts</returns>
        IList<ProfilePost> GetProfilePosts(GId userId, uint maxPosts, uint offset);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile post from</param>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        Task<ProfilePost> GetProfilePostAsync(GId userId, uint postId);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile post from</param>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        ProfilePost GetProfilePost(GId userId, uint postId);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        Task<IList<PostReply>> GetProfileRepliesAsync(uint postId);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        IList<PostReply> GetProfileReplies(uint postId);
    }
}