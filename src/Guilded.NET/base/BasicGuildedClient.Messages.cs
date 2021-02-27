using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json.Linq;

namespace Guilded.NET
{
    using API;

    using Objects.Events;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        /// <summary>
        /// Dictionary of command attribute and command methods. Stores all of the given commands.
        /// </summary>
        /// <value>Dictionary of key(command attribute) and value(command method)</value>
        public Dictionary<CommandAttribute, CommandMethod> CommandDictionary
        {
            get; set;
        }
        /// <summary>
        /// Checks if there's a command in the message. If there is, it executes that command.
        /// </summary>
        /// <param name="o">Who invoked this method</param>
        /// <param name="msg">Message creation event</param>
        protected virtual void CommandChecking(object o, MessageCreatedEvent msg)
        {
            // If commands are not enabled or config is null
            if (ClientConfig == null || !ClientConfig.EnableCommands) return;
            // If message was posted by this user, ignore it.
            if (msg.Message.AuthorId == Me.User.Id && ClientConfig.IgnoreOwnCommands) return;
            // If there's no prefix func, ignore the command
            if (ClientConfig.Prefix == null) return;
            // Gets prefix
            string prefix = ClientConfig.Prefix(msg);
            // Turns message content to string
            string content = msg.Message.ToString();
            // If the message doesn't start with prefix, ignore it.
            if (!content.StartsWith(prefix)) return;
            // Skip the prefix and split rest of the content
            string[] split = content[prefix.Length..].Split(ClientConfig.CommandArgumentSplit, ClientConfig.SplitOptions);
            // Get first item in the array, which is a command name
            string command = split[0].Trim();
            // Get rest of the items, which are the command arguments
            IList<string> args = split[1..];
            // Invokes command event
            CommandInvokedEvent?.Invoke(this, msg, command, args);
        }
        /// <summary>
        /// Method when someone invokes a command.
        /// </summary>
        /// <param name="client">Client currently being used</param>
        /// <param name="msg">Message created event</param>
        /// <param name="command">Command name</param>
        /// <param name="args">Command arguments</param>
        protected void InvokeCommand(BasicGuildedClient client, MessageCreatedEvent msg, string command, IList<string> args)
        {
            // Gets attribute by command name
            CommandAttribute attr = CommandDictionary.Keys.FirstOrDefault(x => x.IsNameEqual(command));
            // If attr is null, ignore the command
            if (attr == null) return;
            // Else, get the CommandMethod and execute it
            CommandDictionary[attr](client, msg, command, args);
        }
        /// <summary>
        /// Gets all command methods from the specific type.
        /// </summary>
        /// <param name="type">Type to fetch commands from</param>
        public void FetchCommands(Type type)
        {
            // Get all methods from it
            foreach (MethodInfo method in type.GetMethods())
            {
                // Gets the command attribute
                if (method.GetCustomAttribute(typeof(CommandAttribute), true) is CommandAttribute attr)
                {
                    // Create delegate instance from the method
                    object obj = Delegate.CreateDelegate(typeof(CommandMethod), method, false);
                    // If obj is null, throw an error that it does not match `CommandMethod`
                    if (obj == null) throw new InvalidProgramException($"{method.Name} must match delegate {nameof(CommandMethod)}.");
                    // Casts the object to CommandMethod and adds it to the dictionary along with attribute
                    CommandDictionary.Add(attr, (CommandMethod)obj);
                }
                else continue;
            }
        }
        /// <summary>
        /// When socket message event is invoked.
        /// </summary>
        /// <param name="o">Object which invoked the event</param>
        /// <param name="x">Socket message</param>
        protected void HandleSocketMessages(object o, SocketMessage x)
        {
            // Check if it's socket event
            if (x is SocketEvent xe)
            {
                // Get the given data
                JObject xeobj = xe.Object;
                // Get the type of the event
                switch (xe.MessageType)
                {
                    // Message events
                    case "ChatMessageCreated":
                        InvokeEvent<MessageCreatedEvent>(MessageCreatedEvent, xeobj);
                        break;
                    case "ChatMessageDeleted":
                        InvokeEvent<MessageDeletedEvent>(MessageRemovedEvent, xeobj);
                        break;
                    case "ChatMessageUpdated":
                        InvokeEvent<MessageUpdatedEvent>(MessageUpdatedEvent, xeobj);
                        break;
                    // Chat channel events
                    case "ChatChannelTyping":
                        InvokeEvent<UserTypingEvent>(UserTypingEvent, xeobj);
                        break;
                    case "ChatMessageReactionAdded":
                        InvokeEvent<ReactionUpdatedEvent>(ReactionAddedEvent, xeobj);
                        break;
                    case "ChatMessageReactionDeleted":
                        InvokeEvent<ReactionUpdatedEvent>(ReactionRemovedEvent, xeobj);
                        break;
                    case "ChatPinnedMessageCreated":
                        InvokeEvent<MessagePinUpdatedEvent>(MessagePinnedEvent, xeobj);
                        break;
                    case "ChatPinnedMessageDeleted":
                        InvokeEvent<MessagePinUpdatedEvent>(MessageUnpinnedEvent, xeobj);
                        break;
                    // Voice channel events
                    case "TeamChannelVoiceParticipantAdded":
                        InvokeEvent<VoiceUpdatedEvent>(UserVoiceJoinedEvent, xeobj);
                        break;
                    case "TeamChannelVoiceParticipantRemoved":
                        InvokeEvent<VoiceUpdatedEvent>(UserVoiceLeftEvent, xeobj);
                        break;
                    // Channel content events
                    case "TEAM_CHANNEL_CONTENT_CREATED":
                        InvokeEvent<ContentCreatedEvent>(ContentCreatedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_DELETED":
                        InvokeEvent<ContentDeletedEvent>(ContentRemovedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_UPDATED":
                        InvokeEvent<ContentUpdatedEvent>(ContentUpdatedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_REPLY_CREATED":
                        InvokeEvent<ContentReplyCreatedEvent>(ContentReplyCreatedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_REPLY_UPDATED":
                        InvokeEvent<ContentReplyUpdatedEvent>(ContentReplyUpdatedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_REPLY_DELETED":
                        InvokeEvent<ContentReplyDeletedEvent>(ContentReplyDeletedEvent, xeobj);
                        break;
                    // Channel events
                    case "TeamChannelCreated":
                        InvokeEvent<ChannelCreatedEvent>(ChannelCreatedEvent, xeobj);
                        break;
                    case "TeamChannelUpdated":
                        InvokeEvent<ChannelUpdatedEvent>(ChannelUpdatedEvent, xeobj);
                        break;
                    case "TeamChannelDeleted":
                        InvokeEvent<ChannelDeletedEvent>(ChannelDeletedEvent, xeobj);
                        break;
                    case "TemporalChannelCreated":
                        InvokeEvent<ThreadCreatedEvent>(ThreadCreatedEvent, xeobj);
                        break;
                    case "CHANNEL_BADGED":
                        InvokeEvent<ChannelBadgedEvent>(ChannelBadgedEvent, xeobj);
                        break;
                    case "CHANNEL_SEEN":
                        InvokeEvent<ChannelSeenEvent>(ChannelSeenEvent, xeobj);
                        break;
                    // Group events
                    case "TEAM_GROUP_CREATED":
                        InvokeEvent<GroupCreatedEvent>(GroupCreatedEvent, xeobj);
                        break;
                    case "TEAM_GROUP_UPDATED":
                        InvokeEvent<GroupUpdatedEvent>(GroupUpdatedEvent, xeobj);
                        break;
                    case "TeamGroupDeleted":
                        InvokeEvent<TeamGroupEvent>(GroupDeletedEvent, xeobj);
                        break;
                    case "TeamGroupArchived":
                        InvokeEvent<TeamGroupEvent>(GroupArchivedEvent, xeobj);
                        break;
                    case "TeamGroupRestored":
                        InvokeEvent<TeamGroupEvent>(GroupRestoredEvent, xeobj);
                        break;
                    // Team events
                    case "TeamMemberUpdated":
                        InvokeEvent<TeamMemberUpdatedEvent>(MemberUpdatedEvent, xeobj);
                        break;
                    case "teamRolesUpdated":
                        InvokeEvent<TeamRolesUpdatedEvent>(RolesUpdatedEvent, xeobj);
                        break;
                    // User events
                    case "USER_UPDATED":
                        // Convert xeobj to user updated event
                        UserUpdatedEvent updated = xeobj.ToObject<UserUpdatedEvent>(GuildedSerializer);
                        // If client's user got updated
                        if (updated.UserId == Me.Id)
                            MeUpdated(updated);
                        // Invoke the event
                        UserUpdatedEvent?.DynamicInvoke(this, updated);
                        break;
                    case "USER_TEAMS_UPDATED":
                        // Convert xeobj to user teams updated event
                        UserTeamsUpdated teamsUpdated = xeobj.ToObject<UserTeamsUpdated>(GuildedSerializer);
                        // Updates Me.Teams
                        MeUpdated(teamsUpdated);
                        // Invoke the event
                        TeamsUpdatedEvent?.DynamicInvoke(this, teamsUpdated);
                        break;
                }
            }
            else if (x is SocketMessage xm)
                // If it's a heartbeat response
                if (xm.Number == 3) InvokeHeartbeatEvent(this, 3);
        }
        /// <summary>
        /// Shortens long event invokation.
        /// </summary>
        /// <param name="ev">Event to be invoked</param>
        /// <param name="arg">JSON Objects which should be turned to other object</param>
        /// <typeparam name="T">Event type</typeparam>
        void InvokeEvent<T>(Delegate ev, JObject arg) where T : Event =>
            ev?.DynamicInvoke(this, arg.ToObject<T>(GuildedSerializer));
        /// <summary>
        /// Method when this user gets updated.
        /// </summary>
        /// <param name="e">User updated event</param>
        protected virtual void MeUpdated(UserUpdatedEvent e) =>
            // Username, Email, Subdomain
            (Me.User.Username, Me.User.Email, Me.User.Subdomain)
                // Updated name ?? Old name, Updated email ?? Old email, Updated subdomain ?? Old subdomain
                = (e.Name ?? Me.Username, e.Email ?? Me.User.Email, e.Subdomain ?? Me.User.Subdomain);
        /// <summary>
        /// Updates Me.Teams when <see cref="UserTeamsUpdated"/> event occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void MeUpdated(UserTeamsUpdated e)
        {
            // If the team was removed
            if (e.IsBanned || e.IsRemoved) Me.Teams = Me.Teams.Where(x => x.Id != e.TeamId).ToList();
            // Else, add that team
            else Me.Teams.Add(GetTeam(e.TeamId));
        }
    }
}