using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Users;
    public abstract partial class BaseGuildedClient
    {
        #region Profile info
        /// <summary>
        /// Gets user's social links.
        /// </summary>
        /// <remarks>
        /// <para>Gets user's(from <paramref name="userId"/>) social link based on given <paramref name="linkType"/>.</para>
        /// <para>This does not require any permissions to be given, as it is not team-based.</para>
        /// </remarks>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="linkType">The social link to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <returns>User's social link</returns>
        public abstract Task<SocialLink> GetSocialLinkAsync(GId userId, SocialLinkType linkType);
        #endregion

        /*/// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <returns>Current User</returns>
        Task<Me> GetThisUserAsync();
        /// <summary>
        /// Gets a list of friends, pending requests and requests sent.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <returns>Friend list</returns>
        Task<FriendList> GetFriendsAsync();
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <param name="name">New name</param>
        Task ChangeNameAsync(string name);
        /// <summary>
        /// Changes user's presence.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <param name="presence">New presence</param>
        Task ChangePresenceAsync(Presence presence);
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <param name="status">New status</param>
        Task ChangeStatusAsync(UserStatus status);
        /// <summary>
        /// Displays the give game as a status.
        /// </summary>
        /// <param name="gameId">ID of the game to set as playing</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Game status</returns>
        Task<GameStatus> StartGameAsync(uint? gameId);
        /// <summary>
        /// Gets all DM channels.
        /// </summary>
        /// <exception cref="GuildedException"/>
        /// <returns>Channel</returns>
        Task<IList<DMChannel>> GetDMChannelsAsync();
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Channel</returns>
        Task<DMChannel> CreateDMChannelAsync(params GId[] users);
        /// <summary>
        /// Accepts an invite.
        /// </summary>
        /// <param name="inviteId">ID of the invite to accept</param>
        /// <param name="teamId">ID of the team to accept invite of</param>
        /// <exception cref="GuildedException"/>
        Task AcceptInviteAsync(GId inviteId, GId teamId = null);
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <exception cref="GuildedException"/>
        /// <returns>User</returns>
        Task<User> GetUserAsync(GId id);
        /// <summary>
        /// Gets a profile of a user.
        /// </summary>
        /// <param name="userId">ID of the user to get profile of</param>
        /// <exception cref="GuildedException"/>
        /// <returns>User profile</returns>
        Task<ProfileUser> GetProfileAsync(GId userId);
        /// <summary>
        /// Gets a set amount of posts in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile posts from</param>
        /// <param name="maxPosts">How many posts it should get</param>
        /// <param name="offset">At which index it should start getting posts</param>
        /// <exception cref="GuildedException"/>
        /// <returns>List of posts</returns>
        Task<IList<ProfilePost>> GetProfilePostsAsync(GId userId, uint maxPosts = 7, uint offset = 0);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile post from</param>
        /// <param name="postId">ID of the post to get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Profile post</returns>
        Task<ProfilePost> GetProfilePostAsync(GId userId, uint postId);
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="postId">ID of the post to get</param>
        /// <exception cref="GuildedException"/>
        /// <returns>Profile post</returns>
        Task<IList<PostReply>> GetProfileRepliesAsync(uint postId)*/
    }
}