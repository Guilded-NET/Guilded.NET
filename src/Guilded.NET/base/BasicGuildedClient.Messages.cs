using System.Xml.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using Objects;
    using Objects.Events;
    using Util;
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient {
        /// <summary>
        /// When message was posted in the chat.
        /// </summary>
        protected EventHandler<MessageCreatedEvent> MessageCreatedEvent;
        /// <summary>
        /// When message was posted in the channel.
        /// </summary>
        public event EventHandler<MessageCreatedEvent> MessageCreated {
            add => MessageCreatedEvent += value;
            remove => MessageCreatedEvent -= value;
        }
        /// <summary>
        /// When someone is typing in the chat.
        /// </summary>
        protected EventHandler<UserTypingEvent> UserTypingEvent;
        /// <summary>
        /// When someone is typing in the chat.
        /// </summary>
        public event EventHandler<UserTypingEvent> UserTyping {
            add => UserTypingEvent += value;
            remove => UserTypingEvent -= value;
        }
        /// <summary>
        /// When message gets editted/updated.
        /// </summary>
        protected EventHandler<MessageUpdatedEvent> MessageUpdatedEvent;
        /// <summary>
        /// When message gets editted/updated.
        /// </summary>
        public event EventHandler<MessageUpdatedEvent> MessageUpdated {
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
        public event EventHandler<MessageDeletedEvent> MessageRemoved {
            add => MessageRemovedEvent += value;
            remove => MessageRemovedEvent -= value;
        }
        /// <summary>
        /// When user leaves/joins a team or gets kicked/banned from the team.
        /// </summary>
        protected EventHandler<UserTeamsUpdated> TeamsUpdatedEvent;
        /// <summary>
        /// When user leaves/joins a team or gets kicked/banned from the team.
        /// </summary>
        public event EventHandler<UserTeamsUpdated> TeamsUpdated {
            add => TeamsUpdatedEvent += value;
            remove => TeamsUpdatedEvent -= value;
        }
        /// <summary>
        /// When someone creates a thread as a response to the specific message.
        /// </summary>
        protected EventHandler<ThreadCreatedEvent> ThreadCreatedEvent;
        /// <summary>
        /// When someone creates a thread as a response to the specific message.
        /// </summary>
        public event EventHandler<ThreadCreatedEvent> ThreadCreated {
            add => ThreadCreatedEvent += value;
            remove => ThreadCreatedEvent -= value;
        }
        /// <summary>
        /// When command gets invoked.
        /// </summary>
        protected event CommandMethod CommandInvokedEvent;
        /// <summary>
        /// When command gets invoked.
        /// </summary>
        public event CommandMethod CommandInvoked {
            add => CommandInvokedEvent += value;
            remove => CommandInvokedEvent -= value;
        }
        /// <summary>
        /// Dictionary of command attribute and command methods. Stores all of the given commands.
        /// </summary>
        /// <value>Dictionary of key(command attribute) and value(command method)</value>
        public Dictionary<CommandAttribute, CommandMethod> CommandDictionary {
            get; set;
        }
        /// <summary>
        /// An event when Guilded gives a heartbeat response.
        /// </summary>
        public event EventHandler<int> HeartbeatResponse {
            add => HeartbeatEvent += value;
            remove => HeartbeatEvent -= value;
        }
        /// <summary>
        /// Event when Websocket receives a message.
        /// </summary>
        protected event EventHandler<SocketMessage> GuildedWebsocketMessage {
            add => GuildedWebsocketMessageEvent += value;
            remove => GuildedWebsocketMessageEvent -= value;
        }
        /// <summary>
        /// When someone adds a reaction to a message.
        /// </summary>
        protected EventHandler<ReactionUpdatedEvent> ReactionAddedEvent;
        /// <summary>
        /// When someone adds a reaction to a message.
        /// </summary>
        public event EventHandler<ReactionUpdatedEvent> ReactionAdded {
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
        public event EventHandler<ReactionUpdatedEvent> ReactionRemoved {
            add => ReactionRemovedEvent += value;
            remove => ReactionRemovedEvent -= value;
        }
        /// <summary>
        /// When someone creates a forum post, media, document, etc..
        /// </summary>
        protected EventHandler<ContentCreatedEvent> ContentCreatedEvent;
        /// <summary>
        /// When someone creates a forum post, media, document, etc..
        /// </summary>
        public event EventHandler<ContentCreatedEvent> ContentCreated {
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
        public event EventHandler<ContentDeletedEvent> ContentRemoved {
            add => ContentRemovedEvent += value;
            remove => ContentRemovedEvent -= value;
        }
        /// <summary>
        /// Checks if there's a command in the message. If there is, it executes that command.
        /// </summary>
        /// <param name="o">Who invoked this method</param>
        /// <param name="msg">Message creation event</param>
        protected virtual void CommandChecking(object o, MessageCreatedEvent msg) {
            // If commands are not enabled or config is null
            if(ClientConfig == null || !ClientConfig.EnableCommands) return;
            // If message was posted by this user, ignore it.
            if(msg.Message.AuthorId == CurrentUser.Id && ClientConfig.IgnoreOwnCommands) return;
            // If there's no prefix func, ignore the command
            if(ClientConfig.Prefix == null) return;
            // Gets prefix
            string prefix = ClientConfig.Prefix(msg);
            // Turns message content to string
            string content = msg.Message.ToString();
            // If the message doesn't start with prefix, ignore it.
            if(!content.StartsWith(prefix)) return;
            // Skip the prefix and split rest of the content
            string[] split = content[prefix.Length..].Split(ClientConfig.CommandArgumentSplit, ClientConfig.SplitOptions);
            // Get first item in the array, which is a command name
            string command = split[0].Trim();
            // Get rest of the items, which are the command arguments
            IList<string> args = split.Skip(1).ToList();
            // Invokes command event
            CommandInvokedEvent?.Invoke(this, msg, command, args);
        }
        /// <summary>
        /// Method when someone invokes a command.
        /// </summary>
        /// <param name="author">Author of the message</param>
        /// <param name="team">Team the command was used in</param>
        /// <param name="client">Client currently being used</param>
        /// <param name="msg">Message created event</param>
        /// <param name="command">Command name</param>
        /// <param name="args">Command arguments</param>
        protected void InvokeCommand(IGuildedClient client, MessageCreatedEvent msg, string command, IList<string> args) {
            // Gets attribute by command name
            CommandAttribute attr = CommandDictionary.Keys.FirstOrDefault(x => x.IsNameEqual(command));
            // If attr is null, ignore the command
            if(attr == null) return;
            // Else, get the CommandMethod and execute it
            CommandDictionary[attr](client, msg, command, args);
        }
        /// <summary>
        /// Gets all command methods from the specific type.
        /// </summary>
        /// <param name="type">Type to fetch commands from</param>
        public void FetchCommands(Type type) {
            // Get all methods from it
            foreach(MethodInfo method in type.GetMethods()) {
                // Gets the command attribute
                if(method.GetCustomAttribute(typeof(CommandAttribute), true) is CommandAttribute attr) {
                    // Create delegate instance from the method
                    object obj = Delegate.CreateDelegate(typeof(CommandMethod), method, false);
                    // If obj is null, throw an error that it does not match `CommandMethod`
                    if(obj == null) throw new InvalidProgramException($"{method.Name} must match delegate {nameof(CommandMethod)}.");
                    // Casts the object to CommandMethod and adds it to the dictionary along with attribute
                    CommandDictionary.Add(attr, (CommandMethod)obj);
                } else continue;
            }
        }
        /// <summary>
        /// When socket message event is invoked.
        /// </summary>
        /// <param name="o">Object which invoked the event</param>
        /// <param name="x">Socket message</param>
        protected void HandleSocketMessages(object o, SocketMessage x) {
            // Check if it's socket event
            if(x is SocketEvent xe) {
                // Get the given data
                JObject xeobj = xe.Object;
                // Get the type of the event
                switch(xe.MessageType) {
                    case "ChatMessageCreated":
                        InvokeEvent<MessageCreatedEvent>(MessageCreatedEvent, xeobj);
                        break;
                    case "ChatMessageDeleted":
                        InvokeEvent<MessageDeletedEvent>(MessageRemovedEvent, xeobj);
                        break;
                    case "ChatMessageUpdated":
                        InvokeEvent<MessageUpdatedEvent>(MessageUpdatedEvent, xeobj);
                        break;
                    case "TemporalChannelCreated":
                        InvokeEvent<ThreadCreatedEvent>(ThreadCreatedEvent, xeobj);
                        break;
                    case "ChatChannelTyping":
                        InvokeEvent<UserTypingEvent>(UserTypingEvent, xeobj);
                        break;
                    case "USER_TEAMS_UPDATED":
                        InvokeEvent<UserTeamsUpdated>(TeamsUpdatedEvent, xeobj);
                        break;
                    case "ChatMessageReactionAdded":
                        InvokeEvent<ReactionUpdatedEvent>(ReactionAddedEvent, xeobj);
                        break;
                    case "ChatMessageReactionDeleted":
                        InvokeEvent<ReactionUpdatedEvent>(ReactionRemovedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_CREATED":
                        InvokeEvent<ContentCreatedEvent>(ContentCreatedEvent, xeobj);
                        break;
                    case "TEAM_CHANNEL_CONTENT_DELETED":
                        InvokeEvent<ContentDeletedEvent>(ContentRemovedEvent, xeobj);
                        break;  
                }
            }
            else if(x is SocketMessage xm)
                // If it's a heartbeat response
                if(xm.Number == 3) InvokeHeartbeatEvent(this, 3);
        }
        /// <summary>
        /// Shortens long event invokation.
        /// </summary>
        /// <param name="ev">Event to be invoked</param>
        /// <param name="arg">JSON Objects which should be turned to other object</param>
        /// <typeparam name="T">Event type</typeparam>
        void InvokeEvent<T>(Delegate ev, JObject arg) where T: Event =>
            ev?.DynamicInvoke(this, arg.ToObject<T>(GuildedSerializer));
    }
}