using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Chat;
    using Permissions;
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageEvent<T> : BaseObject, ITeamEvent where T : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The message received from the event.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty(Required = Required.Always)]
        public T Message
        {
            get; set;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The identifier of the channel where the message was posted.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonIgnore]
        public Guid ChannelId => Message.ChannelId;
        #endregion

        #region Additional
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(new MessageContent
        /// (
        ///     new Leaf("Hello, "),
        ///     new Leaf(username, MarkType.Bold),
        ///     new Leaf("!")
        /// ));
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await Message.RespondAsync(content).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(new MessageDocument
        /// (
        ///     new Leaf("Welcome to "),
        ///     new Leaf(username, MarkType.Bold),
        ///     new Leaf("!")
        /// ));
        /// </code>
        /// </example>
        /// <param name="document">The rich text markup document that will be used as content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(MessageDocument document) =>
            await RespondAsync(new MessageContent(document)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(new List&lt;Node&gt;
        /// {
        ///     new BlockQuote("Hello"),
        ///     new Paragraph("Hey there!")
        /// });
        /// </code>
        /// </example>
        /// <param name="nodes">The list of rich text markup nodes that will be used to create message content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(IList<Node> nodes) =>
            await RespondAsync(new MessageContent(nodes)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(new BlockQuote("Hello"), new Paragraph("Hey there!"));
        /// </code>
        /// </example>
        /// <param name="nodes">The array of rich text markup nodes that will be used to create message content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(params Node[] nodes) =>
            await RespondAsync(new MessageContent(nodes)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync("Hello!");
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string content) =>
            await Message.RespondAsync(content).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync("Results: {0}", result);
        /// </code>
        /// </example>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(cultureInfo, "Current time: {0}", DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RespondAsync(result);
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(object content) =>
            await RespondAsync(content.ToString()).ConfigureAwait(false);
        #endregion
    }
    /// <summary>
    /// The base for message-related events.
    /// </summary>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageEvent : MessageEvent<Message>
    {
        #region Properties
        /// <summary>
        /// Gets whether the message was created by a bot or webhook.
        /// </summary>
        /// <remarks>
        /// <para>Whether the message was automatically posted by a bot or a webhook.</para>
        /// <para>This relies on <see cref="CreatedByBot"/> and <see cref="CreatedByWebhook"/> properties.
        /// If one of them is not <see langword="null"/>, <see langword="true"/> will be returned. Otherwise,
        /// <see langword="false"/> will be returned.</para>
        /// </remarks>
        /// <returns>Created by bot or webhook</returns>
        [JsonIgnore]
        public bool ByBot => Message.ByBot;
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the message in Markdown format.</para>
        /// </remarks>
        /// <value>Content</value>
        [JsonIgnore]
        public string Content => Message.Content;
        /// <summary>
        /// The identifier of the author of the message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user that posted this message.</para>
        /// </remarks>
        /// <value>User ID</value>
        [JsonIgnore]
        public GId CreatedBy => Message.CreatedBy;
        /// <summary>
        /// The identifier of the webhook author of this message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the webhook that posted this message.</para>
        /// </remarks>
        /// <value>Webhook ID?</value>
        [JsonIgnore]
        public Guid? CreatedByWebhook => Message.CreatedByWebhook;
        /// <summary>
        /// The identifier of the bot author of this message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the flow bot that posted this message.</para>
        /// </remarks>
        /// <value>Bot ID?</value>
        [JsonIgnore]
        public Guid? CreatedByBot => Message.CreatedByBot;
        /// <summary>
        /// The type of the message.
        /// </summary>
        /// <remarks>
        /// Allows message to be determined as a <see cref="MessageType.Default"/> or <see cref="MessageType.System"/>.
        /// </remarks>
        /// <value>Message type</value>
        [JsonIgnore]
        public MessageType Type => Message.Type;
        #endregion

        #region Additional
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(new MessageContent("Edited message"));
        /// </code>
        /// </example>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(MessageContent content) =>
            await Message.UpdateMessageAsync(content).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(new MessageDocument("Edited message"));
        /// </code>
        /// </example>
        /// <param name="document">The rich text markup document that will be used as content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(MessageDocument document) =>
            await UpdateMessageAsync(new MessageContent(document)).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(new List&lt;Node&gt; { new Paragraph("Edited message") });
        /// </code>
        /// </example>
        /// <param name="nodes">The list of rich text markup nodes that will be used to create message content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(IList<Node> nodes) =>
            await UpdateMessageAsync(new MessageContent(nodes)).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(new Paragraph("Edited message"));
        /// </code>
        /// </example>
        /// <param name="nodes">The array of rich text markup nodes that will be used to create message content</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(params Node[] nodes) =>
            await UpdateMessageAsync(new MessageContent(nodes)).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync("Edited message");
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await Message.UpdateMessageAsync(content).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync("Result: {0}", result);
        /// </code>
        /// </example>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(format, args)).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(cultureInfo, "Current time: {0}", DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(IFormatProvider provider, string format, params object[] args) =>
            await UpdateMessageAsync(string.Format(provider, format, args)).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.UpdateMessageAsync(result);
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(object content) =>
            await UpdateMessageAsync(content).ConfigureAwait(false);
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.DeleteMessageAsync();
        /// </code>
        /// </example>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public async Task DeleteMessageAsync() =>
            await Message.DeleteMessageAsync().ConfigureAwait(false);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.AddReactionAsync(90002569);
        /// </code>
        /// </example>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await Message.AddReactionAsync(emoteId).ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// await message.RemoveReactionAsync(90002569);
        /// </code>
        /// </example>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await Message.RemoveReactionAsync(emoteId).ConfigureAwait(false);
        #endregion
    }
}