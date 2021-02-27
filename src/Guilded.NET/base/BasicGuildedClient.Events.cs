using System;

namespace Guilded.NET
{
    using Objects.Events;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        /// <summary>
        /// When message was posted in the chat.
        /// </summary>
        protected EventHandler<MessageCreatedEvent> MessageCreatedEvent;
        /// <summary>
        /// When message was posted in the channel.
        /// </summary>
        public event EventHandler<MessageCreatedEvent> MessageCreated
        {
            add => MessageCreatedEvent += value;
            remove => MessageCreatedEvent -= value;
        }
        /// <summary>
        /// When message gets editted/updated.
        /// </summary>
        protected EventHandler<MessageUpdatedEvent> MessageUpdatedEvent;
        /// <summary>
        /// When message gets editted/updated.
        /// </summary>
        public event EventHandler<MessageUpdatedEvent> MessageUpdated
        {
            add => MessageUpdatedEvent += value;
            remove => MessageUpdatedEvent -= value;
        }
        /// <summary>
        /// When message gets deleted/removed.
        /// </summary>
        protected EventHandler<MessageDeletedEvent> MessageRemovedEvent;
        /// <summary>
        /// When message gets deleted/removed.
        /// </summary>
        public event EventHandler<MessageDeletedEvent> MessageRemoved
        {
            add => MessageRemovedEvent += value;
            remove => MessageRemovedEvent -= value;
        }

        /// <summary>
        /// When someone is typing in the chat.
        /// </summary>
        protected EventHandler<UserTypingEvent> UserTypingEvent;
        /// <summary>
        /// When someone is typing in the chat.
        /// </summary>
        public event EventHandler<UserTypingEvent> UserTyping
        {
            add => UserTypingEvent += value;
            remove => UserTypingEvent -= value;
        }

        /// <summary>
        /// When someone adds a reaction to a message.
        /// </summary>
        protected EventHandler<ReactionUpdatedEvent> ReactionAddedEvent;
        /// <summary>
        /// When someone adds a reaction to a message.
        /// </summary>
        public event EventHandler<ReactionUpdatedEvent> ReactionAdded
        {
            add => ReactionAddedEvent += value;
            remove => ReactionAddedEvent -= value;
        }
        /// <summary>
        /// When someone removes a reaction from a message.
        /// </summary>
        protected EventHandler<ReactionUpdatedEvent> ReactionRemovedEvent;
        /// <summary>
        /// When someone removes a reaction from a message.
        /// </summary>
        public event EventHandler<ReactionUpdatedEvent> ReactionRemoved
        {
            add => ReactionRemovedEvent += value;
            remove => ReactionRemovedEvent -= value;
        }

        /// <summary>
        /// When a message in chat gets pinned.
        /// </summary>
        protected EventHandler<MessagePinUpdatedEvent> MessagePinnedEvent;
        /// <summary>
        /// When a message in chat gets pinned.
        /// </summary>
        public event EventHandler<MessagePinUpdatedEvent> MessagePinned
        {
            add => MessagePinnedEvent += value;
            remove => MessagePinnedEvent -= value;
        }
        /// <summary>
        /// When a message in chat gets unpinned.
        /// </summary>
        protected EventHandler<MessagePinUpdatedEvent> MessageUnpinnedEvent;
        /// <summary>
        /// When a message in chat gets unpinned.
        /// </summary>
        public event EventHandler<MessagePinUpdatedEvent> MessageUnpinned
        {
            add => MessageUnpinnedEvent += value;
            remove => MessageUnpinnedEvent -= value;
        }

        /// <summary>
        /// When someone joins a voice or a stream channel.
        /// </summary>
        protected EventHandler<VoiceUpdatedEvent> UserVoiceJoinedEvent;
        /// <summary>
        /// When someone joins a voice or a stream channel.
        /// </summary>
        public event EventHandler<VoiceUpdatedEvent> UserVoiceJoined
        {
            add => UserVoiceJoinedEvent += value;
            remove => UserVoiceJoinedEvent -= value;
        }
        /// <summary>
        /// When someone leaves a voice or a stream channel.
        /// </summary>
        protected EventHandler<VoiceUpdatedEvent> UserVoiceLeftEvent;
        /// <summary>
        /// When someone leaves a voice or a stream channel.
        /// </summary>
        public event EventHandler<VoiceUpdatedEvent> UserVoiceLeft
        {
            add => UserVoiceLeftEvent += value;
            remove => UserVoiceLeftEvent -= value;
        }

        /// <summary>
        /// When someone creates a forum post, media, document, etc..
        /// </summary>
        protected EventHandler<ContentCreatedEvent> ContentCreatedEvent;
        /// <summary>
        /// When someone creates a forum post, media, document, etc..
        /// </summary>
        public event EventHandler<ContentCreatedEvent> ContentCreated
        {
            add => ContentCreatedEvent += value;
            remove => ContentCreatedEvent -= value;
        }
        /// <summary>
        /// When someone deletes a forum post, media, document, etc..
        /// </summary>
        protected EventHandler<ContentDeletedEvent> ContentRemovedEvent;
        /// <summary>
        /// When someone deletes a forum post, media, document, etc..
        /// </summary>
        public event EventHandler<ContentDeletedEvent> ContentRemoved
        {
            add => ContentRemovedEvent += value;
            remove => ContentRemovedEvent -= value;
        }
        /// <summary>
        /// When someone updates a forum post, media, document, etc..
        /// </summary>
        protected EventHandler<ContentUpdatedEvent> ContentUpdatedEvent;
        /// <summary>
        /// When someone updates a forum post, media, document, etc..
        /// </summary>
        public event EventHandler<ContentUpdatedEvent> ContentUpdated
        {
            add => ContentUpdatedEvent += value;
            remove => ContentUpdatedEvent -= value;
        }
        /// <summary>
        /// When someone creates a forum post, media, document, etc. reply.
        /// </summary>
        protected EventHandler<ContentReplyCreatedEvent> ContentReplyCreatedEvent;
        /// <summary>
        /// When someone creates a forum post, media, document, etc. reply.
        /// </summary>
        public event EventHandler<ContentReplyCreatedEvent> ContentReplyCreated
        {
            add => ContentReplyCreatedEvent += value;
            remove => ContentReplyCreatedEvent -= value;
        }
        /// <summary>
        /// When someone updates a forum post, media, document, etc. reply.
        /// </summary>
        protected EventHandler<ContentReplyUpdatedEvent> ContentReplyUpdatedEvent;
        /// <summary>
        /// When someone updates a forum post, media, document, etc. reply.
        /// </summary>
        public event EventHandler<ContentReplyUpdatedEvent> ContentReplyUpdated
        {
            add => ContentReplyUpdatedEvent += value;
            remove => ContentReplyUpdatedEvent -= value;
        }
        /// <summary>
        /// When someone deletes a forum post, media, document, etc. reply.
        /// </summary>
        protected EventHandler<ContentReplyDeletedEvent> ContentReplyDeletedEvent;
        /// <summary>
        /// When someone deletes a forum post, media, document, etc. reply.
        /// </summary>
        public event EventHandler<ContentReplyDeletedEvent> ContentReplyDeleted
        {
            add => ContentReplyDeletedEvent += value;
            remove => ContentReplyDeletedEvent -= value;
        }

        /// <summary>
        /// When channel gets created.
        /// </summary>
        protected EventHandler<ChannelCreatedEvent> ChannelCreatedEvent;
        /// <summary>
        /// When channel gets created.
        /// </summary>
        public event EventHandler<ChannelCreatedEvent> ChannelCreated
        {
            add => ChannelCreatedEvent += value;
            remove => ChannelCreatedEvent -= value;
        }
        /// <summary>
        /// When channel gets updated.
        /// </summary>
        protected EventHandler<ChannelUpdatedEvent> ChannelUpdatedEvent;
        /// <summary>
        /// When channel gets updated.
        /// </summary>
        public event EventHandler<ChannelUpdatedEvent> ChannelUpdated
        {
            add => ChannelUpdatedEvent += value;
            remove => ChannelUpdatedEvent -= value;
        }
        /// <summary>
        /// When channel gets deleted.
        /// </summary>
        protected EventHandler<ChannelDeletedEvent> ChannelDeletedEvent;
        /// <summary>
        /// When channel gets deleted.
        /// </summary>
        public event EventHandler<ChannelDeletedEvent> ChannelDeleted
        {
            add => ChannelDeletedEvent += value;
            remove => ChannelDeletedEvent -= value;
        }
        /// <summary>
        /// When someone creates a thread as a response to the specific message.
        /// </summary>
        protected EventHandler<ThreadCreatedEvent> ThreadCreatedEvent;
        /// <summary>
        /// When someone creates a thread as a response to the specific message.
        /// </summary>
        public event EventHandler<ThreadCreatedEvent> ThreadCreated
        {
            add => ThreadCreatedEvent += value;
            remove => ThreadCreatedEvent -= value;
        }
        /// <summary>
        /// When a notification appears in a channel.
        /// </summary>
        protected EventHandler<ChannelBadgedEvent> ChannelBadgedEvent;
        /// <summary>
        /// When a notification appears in a channel.
        /// </summary>
        public event EventHandler<ChannelBadgedEvent> ChannelBadged
        {
            add => ChannelBadgedEvent += value;
            remove => ChannelBadgedEvent -= value;
        }
        /// <summary>
        /// When the client views a channel and clears the notifications.
        /// </summary>
        protected EventHandler<ChannelSeenEvent> ChannelSeenEvent;
        /// <summary>
        /// When the client views a channel and clears the notifications.
        /// </summary>
        public event EventHandler<ChannelSeenEvent> ChannelSeen
        {
            add => ChannelSeenEvent += value;
            remove => ChannelSeenEvent -= value;
        }

        /// <summary>
        /// When group gets created in a team.
        /// </summary>
        protected EventHandler<GroupCreatedEvent> GroupCreatedEvent;
        /// <summary>
        /// When group gets created in a team.
        /// </summary>
        public event EventHandler<GroupCreatedEvent> GroupCreated
        {
            add => GroupCreatedEvent += value;
            remove => GroupCreatedEvent -= value;
        }
        /// <summary>
        /// When group gets updated in a team.
        /// </summary>
        protected EventHandler<GroupUpdatedEvent> GroupUpdatedEvent;
        /// <summary>
        /// When group gets updated in a team.
        /// </summary>
        public event EventHandler<GroupUpdatedEvent> GroupUpdated
        {
            add => GroupUpdatedEvent += value;
            remove => GroupUpdatedEvent -= value;
        }
        /// <summary>
        /// When group gets deleted in a team.
        /// </summary>
        protected EventHandler<TeamGroupEvent> GroupDeletedEvent;
        /// <summary>
        /// When group gets deleted in a team.
        /// </summary>
        public event EventHandler<TeamGroupEvent> GroupDeleted
        {
            add => GroupDeletedEvent += value;
            remove => GroupDeletedEvent -= value;
        }
        /// <summary>
        /// When group gets archived in a team.
        /// </summary>
        protected EventHandler<TeamGroupEvent> GroupArchivedEvent;
        /// <summary>
        /// When group gets archived in a team.
        /// </summary>
        public event EventHandler<TeamGroupEvent> GroupArchived
        {
            add => GroupArchivedEvent += value;
            remove => GroupArchivedEvent -= value;
        }
        /// <summary>
        /// When group gets unarchived in a team.
        /// </summary>
        protected EventHandler<TeamGroupEvent> GroupRestoredEvent;
        /// <summary>
        /// When group gets unarchived in a team.
        /// </summary>
        public event EventHandler<TeamGroupEvent> GroupRestored
        {
            add => GroupRestoredEvent += value;
            remove => GroupRestoredEvent -= value;
        }

        /// <summary>
        /// When member's information gets updated in a team.
        /// </summary>
        protected EventHandler<TeamMemberUpdatedEvent> MemberUpdatedEvent;
        /// <summary>
        /// When member's information gets updated in a team.
        /// </summary>
        public event EventHandler<TeamMemberUpdatedEvent> MemberUpdated
        {
            add => MemberUpdatedEvent += value;
            remove => MemberUpdatedEvent -= value;
        }
        /// <summary>
        /// When member's roles get updated.
        /// </summary>
        protected EventHandler<TeamRolesUpdatedEvent> RolesUpdatedEvent;
        /// <summary>
        /// When member's roles get updated.
        /// </summary>
        public event EventHandler<TeamRolesUpdatedEvent> RolesUpdated
        {
            add => RolesUpdatedEvent += value;
            remove => RolesUpdatedEvent -= value;
        }

        /// <summary>
        /// When user gets updated.
        /// </summary>
        protected EventHandler<UserUpdatedEvent> UserUpdatedEvent;
        /// <summary>
        /// When user gets updated.
        /// </summary>
        public event EventHandler<UserUpdatedEvent> UserUpdated
        {
            add => UserUpdatedEvent += value;
            remove => UserUpdatedEvent -= value;
        }
        /// <summary>
        /// When user leaves/joins a team or gets kicked/banned from the team.
        /// </summary>
        protected EventHandler<UserTeamsUpdated> TeamsUpdatedEvent;
        /// <summary>
        /// When user leaves/joins a team or gets kicked/banned from the team.
        /// </summary>
        public event EventHandler<UserTeamsUpdated> TeamsUpdated
        {
            add => TeamsUpdatedEvent += value;
            remove => TeamsUpdatedEvent -= value;
        }

        /// <summary>
        /// When command gets invoked.
        /// </summary>
        protected event CommandMethod CommandInvokedEvent;
        /// <summary>
        /// When command gets invoked.
        /// </summary>
        public event CommandMethod CommandInvoked
        {
            add => CommandInvokedEvent += value;
            remove => CommandInvokedEvent -= value;
        }
    }
}