using Guilded.NET.Objects.Events;
using System;
using Guilded.NET.Objects.Chat;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    using System.Transactions;
    using Guilded.NET.Objects.Content;
    using Teams;
    /// <summary>
    /// Represents any Guilded client.
    /// </summary>
    public interface IGuildedClient {
        /// <summary>
        /// Event when someone posts a message in the chat.
        /// </summary>
        event EventHandler<MessageCreatedEvent> MessageCreated;
        /// <summary>
        /// Event when someone starts typing in the chat.
        /// </summary>
        event EventHandler<UserTypingEvent> UserTyping;
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        /// <returns>Response</returns>
        Task<object> SendMessageAsync(Guid channel, NewMessage message);
        /// <summary>
        /// Sends a message into the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">Message to post</param>
        void SendMessage(Guid channel, NewMessage message);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        Task<object> EditMessageAsync(Guid channel, Guid messageId, MessageContent content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        void EditMessage(Guid channel, Guid messageId, MessageContent content);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        Task<object> DeleteMessageAsync(Guid channel, Guid messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="channel">ID of the channel</param>
        /// <param name="message">ID of the message to delete</param>
        void DeleteMessage(Guid channel, Guid messageId);
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
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        Task<Team> GetTeamAsync(GId id);
        /// <summary>
        /// Gets team with given ID.
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team</returns>
        Team GetTeam(GId id);
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        Task<Channels> GetChannelsAsync(GId teamId);
        /// <summary>
        /// List of channels and categories in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>Channel list</returns>
        Channels GetChannels(GId teamId);
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        Task<IList<Group>> GetGroupsAsync(GId teamId);
        /// <summary>
        /// List of groups in given team.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <returns>List of groups</returns>
        IList<Group> GetGroups(GId teamId);
        /// <summary>
        /// Gets message inside a specific channel.
        /// </summary>
        /// <param name="channel">Channel to get message in</param>
        /// <param name="messageId">ID of the message</param>
        /// <returns>Message</returns>
        Task<Message> GetMessageAsync(Channel channel, Guid messageId);
        /// <summary>
        /// Gets message inside a specific channel.
        /// </summary>
        /// <param name="channel">Channel to get message in</param>
        /// <param name="messageId">ID of the message</param>
        /// <returns>Message</returns>
        Message GetMessage(Channel channel, Guid messageId);
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
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        /// <returns>Async task</returns>
        Task ClearNotificationsAsync(Guid channelId);
        /// <summary>
        /// Clears all notifications in a specific channel.
        /// </summary>
        /// <param name="channelId">ID of the channel to clear notifications in</param>
        void ClearNotifications(Guid channelId);
        /// <summary>
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name of the channel</param>
        /// <returns>Async task</returns>
        Task CreateChannelAsync(GId team, GId group, ChannelType type, bool @public, string name);
        /// <summary>
        /// Creates a new channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to create channel in</param>
        /// <param name="group">Group to create channel in</param>
        /// <param name="type">Channel type</param>
        /// <param name="public">If channel should be public</param>
        /// <param name="name">Name of the channel</param>
        void CreateChannel(GId team, GId group, ChannelType type, bool @public, string name);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        /// <returns>Async task</returns>
        Task DeleteChannelAsync(GId team, GId group, Guid channel);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        /// <param name="team">Team to delete channel in</param>
        /// <param name="group">Group to delete channel in</param>
        /// <param name="channel">Channel to be deleted</param>
        void DeleteChannel(GId team, GId group, Guid channel);
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        Task<IList<Message>> GetMessagesAsync(Guid channel, uint limit);
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <param name="channel">Channel to get messages in</param>
        /// <param name="limit">How many messages it should get</param>
        /// <returns>List of messages</returns>
        IList<Message> GetMessages(Guid channel, uint limit);
        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        /// <returns>Async task</returns>
        Task JoinTeamAsync(GId team);
        /// <summary>
        /// Joins a specific team.
        /// </summary>
        /// <param name="team">Team to join</param>
        void JoinTeam(GId team);
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        /// <returns>Async task</returns>
        Task LeaveTeamAsync(GId team);
        /// <summary>
        /// Leaves a specific team.
        /// </summary>
        /// <param name="team">Team to leave</param>
        void LeaveTeam(GId team);
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
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        Task<TeamMember> GetMemberAsync(GId team, GId user);
        /// <summary>
        /// Gets member with given ID.
        /// </summary>
        /// <param name="team">Team ID</param>
        /// <param name="user">User ID</param>
        /// <returns>Member</returns>
        TeamMember GetMember(GId team, GId user);
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        Task<Channel> GetChannelAsync(GId teamId, Guid channelId);
        /// <summary>
        /// Gets a channel by ID.
        /// </summary>
        /// <param name="teamId">ID of the team</param>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>Channel</returns>
        Channel GetChannel(GId teamId, Guid channelId);
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
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        Task<IList<ForumPost>> GetForumPostsAsync(Guid channelId, uint maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="beforeDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <returns>Forum post list</returns>
        IList<ForumPost> GetForumPosts(Guid channelId, uint maxItems, DateTime? beforeDate);
        /// <summary>
        /// Gets forum posts from a specific forum channel.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="afterDate">Before what date it should get posts</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        Task<IList<ForumReply>> GetForumRepliesAsync(Guid channelId, uint postId, uint maxItems, DateTime? afterDate);
        /// <summary>
        /// Gets forum replies from a forum post.
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="postId">ID of the post it should get replies from</param>
        /// <param name="maxItems">How many forum posts it should get</param>
        /// <param name="afterDate">After what date it should get posts</param>
        /// <returns>Forum reply list</returns>
        IList<ForumReply> GetForumReplies(Guid channelId, uint postId, uint maxItems, DateTime? afterDate);
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        Task CreateForumPostAsync(Guid channelId, string title, MessageContent message);
        /// <summary>
        /// Create a forum post in a forum channel.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="title">A title of the forum post</param>
        /// <param name="message">Content of the forum post</param>
        void CreateForumPost(Guid channelId, string title, MessageContent message);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        /// <returns>Async task</returns>
        Task CreateForumReplyAsync(Guid channelId, uint postId, MessageContent message);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="channelId">ID of the forum channel</param>
        /// <param name="postId">ID of the post it should reply to</param>
        /// <param name="message">Content of the forum post</param>
        void CreateForumReply(Guid channelId, uint postId, MessageContent message);
        /// <summary>
        /// Creates a new thread as a response to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel this thread should be created in</param>
        /// <param name="threadMessage">Message to respond to</param>
        /// <param name="responseMessage">Message as a response to the other message</param>
        /// <param name="name">Name of the thread</param>
        /// <returns>Thread created</returns>
        //Task<ThreadChannel> CreateThreadAsync(Guid channelId, Message threadMessage, NewMessage responseMessage, string name);
        /// <summary>
        /// Creates a new thread as a response to a specific message.
        /// </summary>
        /// <param name="channelId">ID of the channel this thread should be created in</param>
        /// <param name="threadMessage">Message to respond to</param>
        /// <param name="responseMessage">Message as a response to the other message</param>
        /// <param name="name">Name of the thread</param>
        /// <returns>Thread created</returns>
        //ThreadChannel CreateThread(Guid channelId, Message threadMessage, NewMessage responseMessage, string name);
    }
}