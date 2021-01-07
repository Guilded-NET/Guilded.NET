using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using API;
    using Util;
    using Objects;
    using Objects.Chat;
    using Objects.Teams;
    using Objects.Forms;
    using Objects.Content;

    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public abstract partial class BasicGuildedClient: IGuildedClient {
        /// <summary>
        /// A random for generating IDs.
        /// </summary>
        protected Random RandomId;
        /// <summary>
        /// Serializer used to (de)serialize JSON given by Guilded or made for Guilded.
        /// </summary>
        /// <value>Serializer</value>
        public JsonSerializer GuildedSerializer {
            get; set;
        }
        T Convert<T>(string content) where T: class =>
            content != null ? JsonConvert.DeserializeObject<T>(content, Converters) : null;
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        async Task<JObject> FromObject(Endpoint endpoint, params IReqAddable[] addables) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(endpoint, addables);
            // Gets the response as an object and returns it
            return JObject.Parse(response.Content);
        }
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<T> FromObject<T>(Endpoint endpoint, params IReqAddable[] addables) =>
            (await FromObject(endpoint, addables)).ToObject<T>(GuildedSerializer);
        /// <summary>
        /// Sends a request to Guilded, gets an object and gets a specific key.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<T> FromObject<T>(Endpoint endpoint, string key, params IReqAddable[] addables) =>
            (await FromObject(endpoint, addables))[key].ToObject<T>(GuildedSerializer);
        /// <summary>
        /// Sends a request to Guilded, gets an array.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        async Task<List<T>> FromArray<T>(Endpoint endpoint, params IReqAddable[] addables) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(endpoint, addables);
            // Gets the response as an object
            JArray array = JArray.Parse(response.Content);
            // Gets and returns parsed object
            return array.ToObject<List<T>>(GuildedSerializer);
        }
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
        /// <param name="messageId">ID of the message to edit</param>
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
        /// <param name="messageId">ID of the message to delete</param>
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
            IRestResponse<object> channel = await ExecuteRequest(new Endpoint($"users/{Me.User.Id}/channels", Method.POST), new JsonBody($"{{\"users\": [{string.Join(", ", ids)}]}}"));
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
            IRestResponse<object> channel = await ExecuteRequest(new Endpoint($"users/{Me.User.Id}/channels", Method.GET));
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
        public async Task<Team> GetTeamAsync(GId id) {
            var lib = JObject.Parse((await ExecuteRequest(new Endpoint($"/teams/{id}", Method.GET))).Content)["team"];
            Console.WriteLine(lib);
            return lib.ToObject<Team>(GuildedSerializer);
        }
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
            JObject.Parse((await ExecuteRequest(new Endpoint($"/teams/{teamId}/groups", Method.GET))).Content)["groups"].ToObject<IList<Group>>(GuildedSerializer);
        /// <summary>
        /// List of groups in given team. Sync version of <see cref="GetGroupsAsync"/>.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        public IList<Group> GetGroups(GId teamId) =>
            GetGroupsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageAsync(string subdomain, GId groupId, Guid channelId, Guid messageId) =>
            (await FromObject(new Endpoint($"content/route/metadata?route=/{subdomain}/groups/{groupId}/channels/{channelId}/chat?messageId={messageId}", Method.GET)))["metadata"]["message"].ToObject<Message>(GuildedSerializer);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <param name="subdomain">Subdomain of a team where that message and channel of that message is</param>
        /// <param name="groupId">ID of the group where that channel is in</param>
        /// <param name="channelId">ID of the channel where that message is</param>
        /// <param name="messageId">ID of message it should get</param>
        /// <returns>Message</returns>
        public Message GetMessage(string subdomain, GId groupId, Guid channelId, Guid messageId) =>
            GetMessageAsync(subdomain, groupId, channelId, messageId).GetAwaiter().GetResult();
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>Async task</returns>
        public async Task ChangeNameAsync(string name) {
            // Sets a new name of this user
            await ExecuteRequest(new Endpoint($"users/{Me.User.Id}/profilev2", Method.POST), new JsonBody(new { name = "name" }));
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
        /// <inheritdoc/>
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
        /// <param name="name">Name which should be assigned to the channel</param>
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
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{Me.User.Id}/join", Method.PUT), new JsonBody("{}"));
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
            await ExecuteRequest(new Endpoint($"teams/{team}/members/{Me.User.Id}", Method.DELETE));
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
        public async Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint? maxItems = 1000, DateTime? beforeDate = null) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/forums?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET));
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
        public IList<ForumPost> GetForumPosts(Guid channelId, uint? maxItems = 1000, DateTime? beforeDate = null) =>
            GetForumPostsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        public async Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint? maxItems = 10, DateTime? afterDate = null) {
            // Gets response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/forums/{postId}/replies?maxItems={maxItems}&afterDate={afterDate ?? DateTime.Now}", Method.GET));
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
        public IList<ForumReply> GetForumReplies(Guid channelId, uint postId, uint? maxItems = 10, DateTime? afterDate = null) =>
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
                id = RandomId.Next(1000000000, int.MaxValue),
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
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <returns>List of documents</returns>
        public async Task<IList<GuildedDocument>> GetDocumentsAsync(Guid channelId, uint? maxItems = 50, DateTime? beforeDate = null) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/docs?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET));
            // Gets the response as an array
            JArray array = JArray.Parse(response.Content);
            // Gets and returns list of docs posts
            return array.ToObject<IList<GuildedDocument>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets all documents within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="maxItems">Amount of documents to get</param>
        /// <param name="beforeDate">Date before which it should get documents</param>
        /// <returns>List of documents</returns>
        public IList<GuildedDocument> GetDocuments(Guid channelId, uint? maxItems = 50, DateTime? beforeDate = null) =>
            GetDocumentsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        public async Task<GuildedDocument> GetDocumentAsync(Guid channelId, uint docId) =>
            Convert<GuildedDocument>((await ExecuteRequest(new Endpoint($"channels/{channelId}/docs/{docId}", Method.GET))).Content);
        /// <summary>
        /// Gets a specific document in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch documents from</param>
        /// <param name="docId">ID of the document</param>
        /// <returns>Document</returns>
        public GuildedDocument GetDocument(Guid channelId, uint docId) =>
            GetDocumentAsync(channelId, docId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        public async Task<IList<GuildedMedia>> GetMediaAsync(Guid channelId) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/media", Method.GET));
            // Gets the response as an object
            JArray array = JArray.Parse(response.Content);
            // Gets and returns list of media posts
            return array.ToObject<IList<GuildedMedia>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets all medias within a specific channel with given max count and before given date.
        /// </summary>
        /// <param name="channelId">ID of channel to fetch media from</param>
        /// <returns>List of media posts</returns>
        public IList<GuildedMedia> GetMedia(Guid channelId) =>
            GetMediaAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        public async Task<IList<CalendarEvent>> GetEventsAsync(Guid channelId, uint? maxItems = 250, DateTime? endDate = null, DateTime? startDate = null) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/events?endDate={endDate ?? DateTime.Now}maxItems={maxItems}&startDate={startDate ?? DateTime.Now}", Method.GET));
            // Gets the response as an object
            JObject obj = JObject.Parse(response.Content);
            // Gets and returns list of events
            return obj["events"].ToObject<IList<CalendarEvent>>(GuildedSerializer);
        }
        /// <summary>
        /// Gets given amount of events from a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many events it should get</param>
        /// <param name="endDate">At which date it should end</param>
        /// <param name="startDate">At which date it should start</param>
        /// <returns>List of calendar events</returns>
        public IList<CalendarEvent> GetEvents(Guid channelId, uint? maxItems = 250, DateTime? endDate = null, DateTime? startDate = null) =>
            GetEventsAsync(channelId, maxItems, endDate ?? DateTime.Today + TimeSpan.FromDays(30), startDate ?? DateTime.Today - TimeSpan.FromDays(30)).GetAwaiter().GetResult();
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        public async Task<IList<Availability>> GetSchedulesAsync(Guid channelId) {
            // Gets a response
            IRestResponse<object> response = await ExecuteRequest(new Endpoint($"channels/{channelId}/availability", Method.GET));
            // Gets the response as an array
            JArray array = JArray.Parse(response.Content);
            // Gets and returns list of availabilities
            return array.ToObject<IList<Availability>>(GuildedSerializer);
        }
        /// <summary>
        /// Get availabilities in a schedule channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of availabilities</returns>
        public IList<Availability> GetSchedules(Guid channelId) =>
            GetSchedulesAsync(channelId).GetAwaiter().GetResult();
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
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetAnnouncementsAsync(Guid channelId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            await FromObject<IList<Announcement>>(new Endpoint($"channels/{channelId}/announcements?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET), "");
        /// <summary>
        /// Gets a list of announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetAnnouncements(Guid channelId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            GetAnnouncementsAsync(channelId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetAnnouncementsAsync(GId teamId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            await FromObject<IList<Announcement>>(new Endpoint($"teams/{teamId}/announcements?maxItems={maxItems}&beforeDate={beforeDate ?? DateTime.Now}", Method.GET), "");
        /// <summary>
        /// Gets a list of announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="maxItems">How many announcements it should get</param>
        /// <param name="beforeDate">Before which date it should get announcements</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetAnnouncements(GId teamId, uint? maxItems = 10, DateTime? beforeDate = null) =>
            GetAnnouncementsAsync(teamId, maxItems, beforeDate).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetPinnedAnnouncementsAsync(Guid channelId) =>
            await FromObject<IList<Announcement>>(new Endpoint($"channels/{channelId}/announcements/pinned", Method.GET), "announcements");
        /// <summary>
        /// Gets a list of pinned announcements in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetPinnedAnnouncements(Guid channelId) =>
            GetPinnedAnnouncementsAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        public async Task<IList<Announcement>> GetPinnedAnnouncementsAsync(GId teamId) =>
            await FromObject<IList<Announcement>>(new Endpoint($"teams/{teamId}/announcements/pinned", Method.GET), "announcements");
        /// <summary>
        /// Gets a list of pinned announcements in a team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of announcements</returns>
        public IList<Announcement> GetPinnedAnnouncements(GId teamId) =>
            GetPinnedAnnouncementsAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        public async Task<uint> CreateFormAsync(BasicGuildedForm form) =>
            await FromObject<uint>(new Endpoint($"content/custom_forms", Method.PUT), "customFormId", new JsonBody(form.Serialize(GuildedSerializer)));
        /// <summary>
        /// Creates a form for form node.
        /// </summary>
        /// <param name="form">Form to create</param>
        /// <returns>Form ID</returns>
        public uint CreateForm(BasicGuildedForm form) =>
            CreateFormAsync(form).GetAwaiter().GetResult();
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        public async Task<FormData> GetFormAsync(uint formId) =>
            await FromObject<FormData>(new Endpoint($"content/custom_forms/{formId}", Method.GET));
        /// <summary>
        /// Gets a form or a poll by form ID.
        /// </summary>
        /// <param name="formId">ID of the form to get</param>
        /// <returns>A form and a form response</returns>
        public FormData GetForm(uint formId) =>
            GetFormAsync(formId).GetAwaiter().GetResult();
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        public async Task<uint> PostFormResponseAsync(uint formId, BasicFormResponse response) =>
            await FromObject<uint>(new Endpoint($"content/custom_forms/{formId}/responses", Method.PUT), "customFormResponseId", new JsonBody(response.Serialize(GuildedSerializer)));
        /// <summary>
        /// Submits a form response.
        /// </summary>
        /// <param name="formId">Form ID it is responding to</param>
        /// <param name="response">Response to submit</param>
        /// <returns>Response ID</returns>
        public uint PostFormResponse(uint formId, BasicFormResponse response) =>
            PostFormResponseAsync(formId, response).GetAwaiter().GetResult();
        /// <summary>
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        public async Task<IList<ListItem>> GetListItemsAsync(Guid channelId) =>
            await FromArray<ListItem>(new Endpoint($"channels/{channelId}/listitems", Method.GET));
        /// <summary>
        /// Gets list items in a given channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>List of list items</returns>
        public IList<ListItem> GetListItems(Guid channelId) =>
            GetListItemsAsync(channelId).GetAwaiter().GetResult();
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        public async Task CreateListItemAsync(Guid channelId, MessageContent title, long priority = 0, Guid? parentId = null, MessageContent note = null) {
            // Creates a new object for creating list item
            var obj = new {
                id = Guid.NewGuid(),
                message = title,
                priority,
                parentId,
                note
            };
            // Sends it to Guilded
            await ExecuteRequest(new Endpoint($"channels/{channelId}/listitems?notifyAllClients=undefined", Method.PUT), new JsonBody(JsonConvert.SerializeObject(obj, GuildedSerializer.Converters.ToArray())));
        }
        /// <summary>
        /// Creates a new list item.
        /// </summary>
        /// <param name="channelId">ID of the channel to add a list item in</param>
        /// <param name="title">Title content of this list item</param>
        /// <param name="priority">Order of this list item</param>
        /// <param name="parentId">ID of the parent</param>
        /// <param name="note">Note of this list item</param>
        public void CreateListItem(Guid channelId, MessageContent title, long priority = 0, Guid? parentId = null, MessageContent note = null) =>
            CreateListItemAsync(channelId, title, priority, parentId, note).GetAwaiter().GetResult();
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        public async Task DeleteListItemAsync(Guid channelId, Guid itemId) =>
            await ExecuteRequest(new Endpoint($"channels/{channelId}/listitems/{itemId}", Method.DELETE));
        /// <summary>
        /// Deletes a list item.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="itemId">ID of the item</param>
        public void DeleteListItem(Guid channelId, Guid itemId) =>
            DeleteListItemAsync(channelId, itemId).GetAwaiter().GetResult();
        
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        public async Task<TeamOverview> GetOverviewAsync(GId teamId) =>
            await FromObject<TeamOverview>(new Endpoint($"teams/{teamId}/overview", Method.GET));
        /// <summary>
        /// Gets an overview page of a team.
        /// </summary>
        /// <param name="teamId">Team to get overview of</param>
        /// <returns>Team overview page</returns>
        public TeamOverview GetOverview(GId teamId) =>
            GetOverviewAsync(teamId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<ContentReply>> GetAnnouncementRepliesAsync(GId announcementId) =>
            await FromArray<ContentReply>(new Endpoint($"content/announcement/{announcementId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given announcement.
        /// </summary>
        /// <param name="announcementId">ID of the announcement</param>
        /// <returns>List of content replies</returns>
        public IList<ContentReply> GetAnnouncementReplies(GId announcementId) =>
            GetAnnouncementRepliesAsync(announcementId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all comments in a given document.
        /// </summary>
        /// <param name="docId">ID of the document</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<ContentReply>> GetDocRepliesAsync(uint docId) =>
            await FromArray<ContentReply>(new Endpoint($"content/doc/{docId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given document.
        /// </summary>
        /// <param name="docId">ID of the document</param>
        /// <returns>List of content replies</returns>
        public IList<ContentReply> GetDocReplies(uint docId) =>
            GetDocRepliesAsync(docId).GetAwaiter().GetResult();
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        public async Task<IList<ContentReply>> GetMediaRepliesAsync(uint mediaId) =>
            await FromArray<ContentReply>(new Endpoint($"content/team_media/{mediaId}/replies", Method.GET));
        /// <summary>
        /// Gets all comments in a given media post.
        /// </summary>
        /// <param name="mediaId">ID of the media post</param>
        /// <returns>List of content replies</returns>
        public IList<ContentReply> GetMediaReplies(uint mediaId) =>
            GetMediaRepliesAsync(mediaId).GetAwaiter().GetResult();
    }
}