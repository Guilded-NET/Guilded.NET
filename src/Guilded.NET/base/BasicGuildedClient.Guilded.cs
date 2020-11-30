using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using Objects.Chat;
    using Objects;
    using API;
    using Objects.Teams;
    using Guilded.NET.Objects.Content;
    using Guilded.NET.Util;

    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient: IGuildedClient {
        /// <summary>
        /// A random for generating IDs.
        /// </summary>
        protected Random RandomId;
        T Convert<T>(string content) where T: class =>
            content != null ? JsonConvert.DeserializeObject<T>(content, Converters) : null;
        /// <summary>
        /// Gets user this client is using.
        /// </summary>
        /// <returns>Task[Current User]</returns>
        public async Task<Me> GetThisUserAsync() =>
            Convert<Me>((await ExecuteRequest(Endpoint.ME)).Content);
        /// <summary>
        /// Gets user this client is using. Sync version of <see cref="GetThisUserAsync"/>.
        /// </summary>
        /// <returns>Current User</returns>
        public Me GetThisUser() =>
            GetThisUserAsync().GetAwaiter().GetResult();
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message</param>
        /// <returns>Async task</returns>
        public async Task<object> SendMessageAsync(Guid channel, NewMessage message) =>
            await ExecuteRequest(new Endpoint($"channels/{channel}/messages", Method.POST), new JsonBody(JsonConvert.SerializeObject(message, Converters)));
        /// <summary>
        /// Sends a message into the chat. Sync version of <see cref="SendMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        public void SendMessage(Guid channel, NewMessage message) =>
            SendMessageAsync(channel, message).GetAwaiter().GetResult();
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public async Task<object> EditMessageAsync(Guid channel, Guid messageId, MessageContent content) {
            // Serializes new content and adds it to new object with property `content`
            string serialized = $"{{\"content\": {content.Serialize(GuildedSerializer)}}}";
            // Edits a message
            return await ExecuteRequest(new Endpoint($"channels/{channel}/messages/{messageId}", Method.PUT), new JsonBody(serialized));
        }
        /// <summary>
        /// Edits message of the bot posted in the chat. Sync version of <see cref="EditMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public void EditMessage(Guid channel, Guid messageId, MessageContent content) =>
            EditMessageAsync(channel, messageId, content).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public async Task<object> DeleteMessageAsync(Guid channel, Guid messageId) =>
            await ExecuteRequest(new Endpoint($"channels/{channel}/messages/{messageId}", Method.DELETE));
        /// <summary>
        /// Deletes a message posted in the chat. Sync version of <see cref="DeleteMessageAsync"/>.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">ID of the message to delete</param>
        public void DeleteMessage(Guid channel, Guid messageId) =>
            DeleteMessageAsync(channel, messageId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets user with given ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(GId id) =>
            JObject.Parse((await ExecuteRequest(new Endpoint($"/users/{id}", Method.GET))).Content)["user"].ToObject<User>(GuildedSerializer);
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
        public async Task<DMChannel> CreateDMChannelAsync(params GId[] users) {
            // Turns all user IDs to objects
            IEnumerable<string> ids = users.Select(x => $"{{\"id\": \"{x}\"}}");
            // Get response
            IRestResponse<object> channel = await ExecuteRequest(new Endpoint($"users/{CurrentUser.Id}/channels", Method.POST), new JsonBody($"{{\"users\": [{string.Join(", ", ids)}]}}"));
            // Returns channel property
            return JObject.Parse(channel.Content)["channel"].ToObject<DMChannel>(GuildedSerializer);
        }
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
        public async Task<IList<DMChannel>> GetDMChannelsAsync() {
            // Get response
            IRestResponse<object> channel = await ExecuteRequest(new Endpoint($"users/{CurrentUser.Id}/channels", Method.GET));
            // Returns channel property
            return JObject.Parse(channel.Content)["channels"].ToObject<IList<DMChannel>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets all DM channels. Sync version of <see cref="GetDMChannelsAsync"/>.
        /// </summary>
        /// <returns>Channel</returns>
        public IList<DMChannel> GetDMChannels() =>
            GetDMChannelsAsync().GetAwaiter().GetResult();
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamAsync(GId id) =>
            JObject.Parse((await ExecuteRequest(new Endpoint($"/teams/{id}", Method.GET))).Content)["team"].ToObject<Team>(GuildedSerializer);
        /// <summary>
        /// Gets team with given ID. Sync version of <see cref="GetTeamAsync"/>.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        public Team GetTeam(GId id) =>
            GetTeamAsync(id).GetAwaiter().GetResult();
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        public async Task<Channels> GetChannelsAsync(GId teamId) =>
            Convert<Channels>((await ExecuteRequest(new Endpoint($"/teams/{teamId}/channels", Method.GET))).Content);
        /// <summary>
        /// List of channels and categories in given team. Sync version of <see cref="GetChannelsAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        public Channels GetChannels(GId teamId) =>
            GetChannelsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        public async Task<IList<Group>> GetGroupsAsync(GId teamId) =>
            JArray.Parse((await ExecuteRequest(new Endpoint($"/teams/{teamId}/groups", Method.GET))).Content)["groups"].ToObject<IList<Group>>(GuildedSerializer);
        /// <summary>
        /// List of groups in given team. Sync version of <see cref="GetGroupsAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        public IList<Group> GetGroups(GId teamId) =>
            GetGroupsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets message inside a specific channel.
        /// </summary>
        /// <param name="channel">Channel to get message in</param>
        /// <param name="messageId">ID of the message</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageAsync(Channel channel, Guid messageId) {
            // Get messages
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"content/route/metadata?route=/{(await channel.GetTeamAsync()).Subdomain}/groups/{channel.GroupId}/channels/{channel.Id}/chat?messageId={messageId}", Method.GET));
            // Parse the content and get message itself
            return JObject.Parse(response.Content)["metadata"]["message"].ToObject<Message>(GuildedSerializer);
        }
        /// <summary>
        /// Gets message inside a specific channel. Sync version of <see cref="GetMessageAsync"/>.
        /// </summary>
        /// <param name="channel">Channel to get message in</param>
        /// <param name="messageId">ID of the message</param>
        /// <returns>Message</returns>
        public Message GetMessage(Channel channel, Guid messageId) =>
            GetMessageAsync(channel, messageId).GetAwaiter().GetResult();
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>Async task</returns>
        public async Task ChangeNameAsync(string name) =>
            await ExecuteRequest(new Endpoint($"users/{CurrentUser.Id}/profilev2", Method.POST), new JsonBody(new { name = "name" }));
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
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <returns>Async task</returns>
        public async Task CreateChannelAsync(GId team, GId group, ChannelType type, bool @public, string name) {
            // Creates object for serialization
            var info = new {
                name,
                contentType = type,
                isPublic = @public
            };
            // Creates a channel based on info
            await ExecuteRequest(new Endpoint($"teams/{team}/groups/{group}/channels", Method.POST), new JsonBody(JsonConvert.SerializeObject(info, Converters)));
        }
        /// <summary>
        /// Creates a new channel in a specific team and group. Sync version of <see cref="CreateChannelAsync"/>.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        public void CreateChannel(GId team, GId group, ChannelType type, bool @public, string name) =>
            CreateChannelAsync(team, group, type, @public, name).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        /// <returns>Async task</returns>
        public async Task DeleteChannelAsync(GId team, GId group, Guid channel) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/groups/{group}/channels/{channel}", Method.DELETE));
        /// <summary>
        /// Deletes a channel in a specific team and group. Sync version of <see cref="DeleteChannelAsync"/>.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        public void DeleteChannel(GId team, GId group, Guid channel) =>
            DeleteChannelAsync(team, group, channel).GetAwaiter().GetResult();
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        public async Task<IList<Message>> GetMessagesAsync(Guid channel, uint limit) {
            // Gets message list request response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channel}/messages?limit={limit}", Method.GET));
            // Parses the response and gets the `message` property
            JToken messages = JObject.Parse(response.Content)["messages"];
            // Turns array to list of messages
            return messages.ToObject<IList<Message>>();
        }
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        public IList<Message> GetMessages(Guid channel, uint limit) =>
            GetMessagesAsync(channel, limit).GetAwaiter().GetResult();
        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        /// <returns>Async task</returns>
        public async Task JoinTeamAsync(GId team) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{CurrentUser.Id}/join", Method.PUT), new JsonBody("{}"));
        /// <summary>
        /// Joins a specific team. Sync version of <see cref="JoinTeamAsync"/>.
        /// </summary>
        /// <param name="team">Team to join</param>
        public void JoinTeam(GId team) =>
            JoinTeamAsync(team).GetAwaiter().GetResult();
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        /// <returns>Async task</returns>
        public async Task LeaveTeamAsync(GId team) =>
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{CurrentUser.Id}", Method.DELETE));
        /// <summary>
        /// Leaves a specific team. Sync version of <see cref="JoinTeamAsync"/>.
        /// </summary>
        /// <param name="team">Team to leave</param>
        public void LeaveTeam(GId team) =>
            LeaveTeamAsync(team).GetAwaiter().GetResult();

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
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        public async Task<TeamMember> GetMemberAsync(GId team, GId user) =>
            (await GetTeamAsync(team)).Members.First(x => x.Id == user);
        /// <summary>
        /// Gets member with given ID. Sync version of <see cref="GetMemberAsync"/>.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        public TeamMember GetMember(GId team, GId user) =>
            GetMemberAsync(team, user).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        public async Task<Channel> GetChannelAsync(GId teamId, Guid channelId) {
            // Gets all channels
            IList<Channel> channels = (await GetChannelsAsync(teamId)).AllChannels;
            // If there is 0 channels, then return null
            if(channels.Count == 0) return null;
            // If not, then return first channel with given ID
            return channels.FirstOrDefault(x => x.Id == channelId);
        }
        /// <summary>
        /// Gets a channel by ID. Sync version of <see cref="GetChannelAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        public Channel GetChannel(GId teamId, Guid channelId) =>
            GetChannelAsync(teamId, channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        public async Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint maxItems, DateTime? beforeDate = null) {
            // Turns beforeDate to string query
            string before = beforeDate == null ? "" : $"&beforeDate={beforeDate}";
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/forums?maxItems={maxItems}{before}", Method.GET));
            // Gets the response as an object
            JObject obj = JObject.Parse(response.Content);
            // Gets and returns list of forum posts
            return obj["threads"].ToObject<IList<ForumPost>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        public IList<ForumPost> GetForumPosts(Guid channelId, uint maxItems, DateTime? beforeDate = null) =>
            GetForumPostsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        public async Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint maxItems, DateTime? afterDate = null) {
            // Turns afterDate to string query
            string after = afterDate == null ? "" : $"&afterDate={afterDate}";
            // Gets response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies?maxItems={maxItems}{after}", Method.GET));
            // Gets the response as an object
            JObject obj = JObject.Parse(response.Content);
            // Gets and returns list of forum posts
            return obj["threadReplies"].ToObject<IList<ForumReply>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets forum replies from a forum post.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        public IList<ForumReply> GetForumReplies(Guid channelId, uint postId, uint maxItems, DateTime? afterDate = null) =>
            GetForumRepliesAsync(channelId, postId, maxItems, afterDate).GetAwaiter().GetResult();
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        public async Task CreateForumPostAsync(Guid channelId, string title, MessageContent message) {
            // Generates a forum post
            var forumPost = new {
                threadId = RandomId.Next(1000000000, int.MaxValue),
                title,
                message
            };
            // Sends it
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums", Method.POST), new JsonBody(JsonConvert.SerializeObject(forumPost, Converters)));
        }
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        public void CreateForumPost(Guid channelId, string title, MessageContent message) =>
            CreateForumPostAsync(channelId, title, message).GetAwaiter().GetResult();
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        public async Task CreateForumReplyAsync(Guid channelId, uint postId, MessageContent message) {
            // Generates a forum reply
            var forumPost = new {
                threadId = RandomId.Next(1000000000, int.MaxValue),
                message
            };
            // Sends it
            await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies", Method.POST), new JsonBody(JsonConvert.SerializeObject(forumPost, Converters)));
        }
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        public void CreateForumReply(Guid channelId, uint postId, MessageContent message) =>
            CreateForumReplyAsync(channelId, postId, message).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a new thread as a response to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel this thread should be created in</param>
        /// <param name="threadMessage">Message to respond to</param>
        /// <param name="responseMessage">Message as a response to the other message</param>
        /// <param name="name">Name of the thread</param>
        /// <returns>Thread created</returns>
        // public async Task<ThreadChannel> CreateThreadAsync(Guid channelId, Message threadMessage, NewMessage responseMessage, string name) {
        //     // Creates object to be serialized
        //     var info = new {
        //         message = responseMessage,
        //         channelId,
        //         threadMessageId = Guid.NewGuid(),
        //         name,
        //         initialThreadMessage = threadMessage,
        //         contentType = ChannelType.Chat,
        //         confirmed = false
        //     };
        //     // JSON to send
        //     JsonBody body = new JsonBody(JsonConvert.SerializeObject(info, Converters));
        //     // Creates new thread
        //     var response = await ExecuteRequest(new Endpoint($"channels/{channelId}/threads", Method.POST), body);
        //     // Get it as object
        //     JObject obj = JObject.Parse(response.Content);
        //     // Get thread property
        //     return obj["thread"].ToObject<ThreadChannel>();
        // }
        /// <summary>
        /// Creates a new thread as a response to a specific message. Sync version of <see cref="CreateThreadAsync"/>.
        /// </summary>
        /// <param name="channelId">ID of the channel this thread should be created in</param>
        /// <param name="threadMessage">Message to respond to</param>
        /// <param name="responseMessage">Message as a response to the other message</param>
        /// <param name="name">Name of the thread</param>
        /// <returns>Thread created</returns>
        // public ThreadChannel CreateThread(Guid channelId, Message threadMessage, NewMessage responseMessage, string name) =>
        //     CreateThreadAsync(channelId, threadMessage, responseMessage, name).GetAwaiter().GetResult();
    }
}