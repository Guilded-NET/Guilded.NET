using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Events;
    using Permissions;
    /// <summary>
    /// A message posted in the chat.
    /// </summary>
    /// <remarks>
    /// <para>An existing/a cached message that can be found in a chat.</para>
    /// <para>This message type can be found in:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="MessageCreatedEvent"/></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="MessageUpdatedEvent"/></description>
    ///     </item>
    ///     <item>
    ///         <description>A return value from message creation and updating methods.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="MessageContent"/>
    /// <seealso cref="Paragraph"/>
    /// <seealso cref="ContainerNode{T}"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    public class Message : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the message in Markdown format.</para>
        /// </remarks>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public string Content
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the author of the message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the user that posted this message.</para>
        /// </remarks>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the webhook author of this message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the webhook that posted this message.</para>
        /// </remarks>
        /// <value>Webhook ID?</value>
        [JsonProperty("createdByWebhookId")]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the bot author of this message.
        /// </summary>
        /// <remarks>
        /// <para>The identifier of the flow bot that posted this message.</para>
        /// </remarks>
        /// <value>Bot ID?</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBot
        {
            get; set;
        }
        /// <summary>
        /// The date of when the message was created.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the message was posted in the chat.</para>
        /// <para>This is recorded by the server and all the delays that were
        /// created by the client will be added as well.</para>
        /// </remarks>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// The date of when the message was updated.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the message was edited.</para>
        /// <para>This is recorded by the server and all the delays that were
        /// created by the client will be added as well.</para>
        /// </remarks>
        /// <value>Updated at?</value>
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// The type of the message.
        /// </summary>
        /// <remarks>
        /// Allows message to be determined as a <see cref="MessageType.Default"/> or <see cref="MessageType.System"/>.
        /// </remarks>
        /// <value>Message type</value>
        [JsonProperty(Required = Required.Always)]
        public MessageType Type
        {
            get; set;
        }
        // /// <summary>
        // /// Whether this message is pinned or not.
        // /// </summary>
        // /// <value>Pinned</value>
        // public bool IsPinned
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// The identifier of the user who pinned this message.
        // /// </summary>
        // /// <value>User ID?</value>
        // public GId? PinnedBy
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// A list of all reactions on this message.
        // /// </summary>
        // /// <value>Reactions</value>
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        // public IList<Reaction> Reactions
        // {
        //     get; set;
        // } = new List<Reaction>();
        #endregion

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
        public bool ByBot => !(CreatedByBot is null) || !(CreatedByWebhook is null);
        #endregion

        #region Additional
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code lang="csharp">
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
            await ParentClient.UpdateMessageAsync(ChannelId, Id, content).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code lang="csharp">
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
        /// <code lang="csharp">
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
        /// <code lang="csharp">
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
        /// <code lang="csharp">
        /// await message.UpdateMessageAsync("Edited message");
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await ParentClient.UpdateMessageAsync(ChannelId, Id, content).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <example>
        /// <code lang="csharp">
        /// await message.UpdateMessageAsync("Result: {0}", result);
        /// </code>
        /// </example>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is null</exception>
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
        /// <code lang="csharp">
        /// await message.UpdateMessageAsync(cultureInfo, "Current time: {0}", DateTime.Now);
        /// </code>
        /// </example>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is null</exception>
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
        /// <code lang="csharp">
        /// await message.UpdateMessageAsync(result);
        /// </code>
        /// </example>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is null</exception>
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
        /// <code lang="csharp">
        /// await message.DeleteMessageAsync();
        /// </code>
        /// </example>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public async Task DeleteMessageAsync() =>
            await ParentClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <example>
        /// <code lang="csharp">
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
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <example>
        /// <code lang="csharp">
        /// await message.RemoveReactionAsync(90002569);
        /// </code>
        /// </example>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <see cref="BaseMessage.ChannelId"/> and/or this message no longer exist(s)</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        // /// <summary>
        // /// Gets whether this message was posted by <paramref name="user"/>.
        // /// </summary>
        // /// <param name="user">The potential author of this message</param>
        // /// <returns>Message by <paramref name="user"/></returns>
        // public bool Of(BaseUser user) =>
        //     CreatedBy == user?.Id;
        #endregion

        #region Overrides
        /// <summary>
        /// Converts the content of this message to Markdown representation of it.
        /// </summary>
        /// <returns>Content as string</returns>
        public override string ToString() =>
            Content;
        #endregion
    }
}
