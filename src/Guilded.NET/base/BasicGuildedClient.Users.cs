using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

namespace Guilded.NET
{
    using API;

    using Objects;
    using Objects.Chat;
    using Objects.Content;

    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient: IGuildedClient {
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Task[Current User]</returns>
        public async Task<Me> GetThisUserAsync() =>
            await FromObject<Me>(Endpoint.ME);
        /// <summary>
        /// Gets user this client is using. Sync version of <see cref="GetThisUserAsync"/>.
        /// </summary>
        /// <returns>Current User</returns>
        public Me GetThisUser() =>
            GetThisUserAsync().GetAwaiter().GetResult();
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(GId id) =>
            await FromObject<User>(new Endpoint($"/users/{id}", Method.GET), "user");
        /// <summary>
        /// Gets user with given ID. Sync version of <see cref="GetUserAsync"/>.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        public User GetUser(GId id) =>
            GetUserAsync(id).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        public async Task<DMChannel> CreateDMChannelAsync(params GId[] users) =>
            await FromObject<DMChannel>(new Endpoint($"users/{Me.User.Id}/channels", Method.POST), "channel", new JsonBody(new { users = users.Select(id => new { id }) }, Converters));
        /// <summary>
        /// Creates a new DM channel. Sync version of <see cref="CreateDMChannelAsync"/>.
        /// </summary>
        /// <param name="users">What users it should add. 1 for normal DMs, 2 for DM group</param>
        /// <returns>Channel</returns>
        public DMChannel CreateDMChannel(params GId[] users) =>
            CreateDMChannelAsync(users).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all DM channels.
        /// </summary>
        /// <returns>Channel</returns>
        public async Task<IList<DMChannel>> GetDMChannelsAsync() =>
            await FromObject<IList<DMChannel>>(new Endpoint($"users/{Me.User.Id}/channels", Method.GET), "channels");
        /// <summary>
        /// Gets all DM channels. Sync version of <see cref="GetDMChannelsAsync"/>.
        /// </summary>
        /// <returns>Channel</returns>
        public IList<DMChannel> GetDMChannels() =>
            GetDMChannelsAsync().GetAwaiter().GetResult();
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>Async task</returns>
        public async Task ChangeNameAsync(string name) {
            // Sets a new name of this user
            await ExecuteRequest(new Endpoint($"users/{Me.User.Id}/profilev2", Method.POST), new JsonBody(new { name }));
            // Changes current user username, so it doesn't remain outdated
            Me.User.Username = name;
        }
        /// <summary>
        /// Changes the name of the user. Sync version of <see cref="ChangeNameAsync"/>.
        /// </summary>
        /// <param name="name">New name</param>
        public void ChangeName(string name) => ChangeNameAsync(name).GetAwaiter().GetResult();
        /// <summary>
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        /// <returns>Async task</returns>
        public async Task ClearNotificationsAsync(Guid channelId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/seen", Method.POST));
        /// <summary>
        /// Clears all notifications in a specific channel. Sync version of <see cref="ClearNotificationsAsync"/>.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        public void ClearNotifications(Guid channelId) =>
            ClearNotificationsAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Accepts an invite.
        /// </summary>
        /// <param name="id">ID of the invite to accept</param>
        /// <returns>Async task</returns>
        public async Task AcceptInviteAsync(GId id) =>
            await ExecuteRequest(new Endpoint($"invites/{id}", Method.POST), new JsonBody(new { type = "consume" }));
        /// <summary>
        /// Accepts an invite.
        /// </summary>
        /// <param name="id">ID of the invite to accept</param>
        public void AcceptInvite(GId id) =>
            AcceptInviteAsync(id).GetAwaiter().GetResult();
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        public async Task<IList<Message>> GetMessagesAsync(Guid channel, uint limit) =>
            await FromObject<IList<Message>>(new Endpoint($"channels/{channel}/messages?limit={limit}", Method.GET), "messages");
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        public IList<Message> GetMessages(Guid channel, uint limit) =>
            GetMessagesAsync(channel, limit).GetAwaiter().GetResult();

        /// <summary>
        /// Changes user's presence.
        /// </summary>
        /// <param name="presence">New presence</param>
        /// <returns>Async task</returns>
        public async Task<object> ChangePresenceAsync(Presence presence) =>
            await ExecuteRequest(Endpoint.PRESENCE, new JsonBody(new { status = (int)presence }));
        /// <summary>
        /// Changes user's presence. Sync version of <see cref="ChangePresenceAsync"/>.
        /// </summary>
        /// <param name="presence">New presence</param>
        public void ChangePresence(Presence presence) =>
            ChangePresenceAsync(presence).GetAwaiter().GetResult();
        /// <summary>
        /// Changes user's status message and emote.
        /// </summary>
        /// <param name="status">New status</param>
        /// <returns>Async task</returns>
        public async Task ChangeStatusAsync(UserStatus status) =>
            await ExecuteRequest(Endpoint.STATUS, new JsonBody(JsonConvert.SerializeObject(status, Converters)));
        /// <summary>
        /// Changes user's status message and emote. Sync version of <see cref="ChangeStatusAsync"/>.
        /// </summary>
        /// <param name="status">New status</param>
        public void ChangeStatus(UserStatus status) =>
            ChangeStatusAsync(status).GetAwaiter().GetResult();
        /// <summary>
        /// Add a reaction to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to add a reaction on</param>
        /// <param name="emoteId">ID of the emote to add</param>
        public async Task AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/messages/{messageId}/reactions/{emoteId}", Method.POST));
        /// <summary>
        /// Add a reaction to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to add a reaction on</param>
        /// <param name="emoteId">ID of the emote to add</param>
        public void AddReaction(Guid channelId, Guid messageId, uint emoteId) =>
            AddReactionAsync(channelId, messageId, emoteId).GetAwaiter().GetResult();
        /// <summary>
        /// Removes a reaction from a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to remove a reaction from</param>
        /// <param name="emoteId">ID of the emote to remove</param>
        public async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/messages/{messageId}/reactions/{emoteId}", Method.DELETE));
        /// <summary>
        /// Removes a reaction from a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel where the message is in</param>
        /// <param name="messageId">ID of the message to remove a reaction from</param>
        /// <param name="emoteId">ID of the emote to remove</param>
        public void RemoveReaction(Guid channelId, Guid messageId, uint emoteId) =>
            RemoveReactionAsync(channelId, messageId, emoteId).GetAwaiter().GetResult();
        
        /// <summary>
        /// Gets a profile of a user.
        /// </summary>
        /// <param name="userId">ID of the user to get profile of</param>
        /// <returns>User profile</returns>
        public async Task<ProfileUser> GetProfileAsync(GId userId) =>
            await FromObject<ProfileUser>(new Endpoint($"users/{userId}/profilev3", Method.GET));
        /// <summary>
        /// Gets a profile of a user.
        /// </summary>
        /// <param name="userId">ID of the user to get profile of</param>
        /// <returns>User profile</returns>
        public ProfileUser GetProfile(GId userId) =>
            GetProfileAsync(userId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a set amount of posts in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile posts from</param>
        /// <param name="maxPosts">How many posts it should get</param>
        /// <param name="offset">At which index it should start getting posts</param>
        /// <returns>List of posts</returns>
        public async Task<IList<ProfilePost>> GetProfilePostsAsync(GId userId, uint maxPosts = 5, uint offset = 0) =>
            await FromArray<ProfilePost>(new Endpoint($"users/{userId}/posts?maxPosts={maxPosts}&offset={offset}", Method.GET));
        /// <summary>
        /// Gets a set amount of posts in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile posts from</param>
        /// <param name="maxPosts">How many posts it should get</param>
        /// <param name="offset">At which index it should start getting posts</param>
        /// <returns>List of posts</returns>
        public IList<ProfilePost> GetProfilePosts(GId userId, uint maxPosts = 5, uint offset = 0) =>
            GetProfilePostsAsync(userId, maxPosts, offset).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile post from</param>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        public async Task<ProfilePost> GetProfilePostAsync(GId userId, uint postId) =>
            await FromObject<ProfilePost>(new Endpoint($"users/{userId}/posts/{postId}", Method.GET));
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="userId">ID of the user to get profile post from</param>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        public ProfilePost GetProfilePost(GId userId, uint postId) =>
            GetProfilePostAsync(userId, postId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        public async Task<IList<PostReply>> GetProfileRepliesAsync(uint postId) =>
            await FromArray<PostReply>(new Endpoint($"content/profilePost/{postId}/replies", Method.GET));
        /// <summary>
        /// Gets a specific post in user's profile.
        /// </summary>
        /// <param name="postId">ID of the post to get</param>
        /// <returns>Profile post</returns>
        public IList<PostReply> GetProfileReplies(uint postId) =>
            GetProfileRepliesAsync(postId).GetAwaiter().GetResult();
        
        
    }
}