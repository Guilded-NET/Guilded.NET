using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Events;
    using Permissions;
    /// <summary>
    /// A message posted in the chat.
    /// </summary>
    /// <remarks>
    /// <para>An existing/a cached message that can be found in a chat. This can be found in chat channels, voice channels, stream channels and their equivalent threads.</para>
    /// <para>This currently includes both messages of types <see cref="MessageType.Default"/> and <see cref="MessageType.System"/>, but it could be changed in the future.</para>
    /// </remarks>
    /// <seealso cref="ListItem"/>
    /// <seealso cref="ForumThread"/>
    /// <seealso cref="MessageCreatedEvent"/>
    /// <seealso cref="MessageUpdatedEvent"/>
    public class Message : ChannelContent<Guid>
    {
        #region JSON properties

        #region Content
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The contents of the message in Markdown format.</para>
        /// <para>This includes images and videos, which are in the format of <c>![](source_url)</c>.</para>
        /// </remarks>
        /// <value>Content in Markdown</value>
        [JsonProperty(Required = Required.Always)]
        public string Content
        {
            get; set;
        }
        /// <summary>
        /// Which messages it is replying to.
        /// </summary>
        /// <remarks>
        /// <para>Specifies which messages were replied to in this message. The max reply limit is 5.</para>
        /// </remarks>
        /// <value>List of message IDs?</value>
        public IList<Guid> ReplyMessageIds
        {
            get; set;
        }
        /// <summary>
        /// Whether the reply is private.
        /// </summary>
        /// <remarks>
        /// <para>Specifies whether the reply is private or not.</para>
        /// <para>This can only be <see langword="true"/> if <see cref="ReplyMessageIds"/> has a value.</para>
        /// </remarks>
        /// <value>Reply is private</value>
        public bool IsPrivate
        {
            get; set;
        }
        /// <summary>
        /// Whether the specified message is a reply
        /// </summary>
        /// <remarks>
        /// <para>Checks whether the message is a reply.</para>
        /// </remarks>
        /// <value>Message is a reply</value>
        [JsonIgnore]
        public bool IsReply => ReplyMessageIds?.Count > 0;
        #endregion

        /// <summary>
        /// The date of when the message was updated.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the message was edited.</para>
        /// <para>This is recorded by the server and all the delays that were created by the client will be added as well.</para>
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
        /// <para>Allows message to be determined as a <see cref="MessageType.Default"/> or <see cref="MessageType.System"/>.</para>
        /// </remarks>
        /// <value>Message type</value>
        [JsonProperty(Required = Required.Always)]
        public MessageType Type
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message in the channel of identifier <see cref="ChannelContent{T}.ChannelId"/> where the message is.</para>
        /// <para>This does not automatically include the message in the reply list.</para>
        /// </remarks>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> CreateMessageAsync(string content) =>
            await ParentClient.CreateMessageAsync(ChannelId, content).ConfigureAwait(false);
        /// <inheritdoc cref="CreateMessageAsync(string)"/>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public async Task<Message> CreateMessageAsync(string content, params Guid[] replyMessageIds) =>
            await ParentClient.CreateMessageAsync(ChannelId, content, replyMessageIds).ConfigureAwait(false);
        /// <inheritdoc cref="CreateMessageAsync(string)"/>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public async Task<Message> CreateMessageAsync(string content, bool isPrivate, params Guid[] replyMessageIds) =>
            await ParentClient.CreateMessageAsync(ChannelId, content, isPrivate, replyMessageIds).ConfigureAwait(false);
        /// <summary>
        /// Replies to the message in the chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message in the channel of identifier <see cref="ChannelContent{T}.ChannelId"/> where the message is.</para>
        /// <para>Includes the message in the reply list.</para>
        /// </remarks>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> ReplyAsync(string content) =>
            await CreateMessageAsync(content, Id).ConfigureAwait(false);
        /// <inheritdoc cref="ReplyAsync(string)"/>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        public async Task<Message> ReplyAsync(string content, bool isPrivate) =>
            await CreateMessageAsync(content, isPrivate, Id).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>Edits the message if the specified message is from the client.
        /// This does not work if the client is not the creator of the message.</para>
        /// </remarks>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public async Task<Message> UpdateMessageAsync(string content) =>
            await ParentClient.UpdateMessageAsync(ChannelId, Id, content).ConfigureAwait(false);
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <remarks>
        /// <para>Removes the message, whether it be from the client or another user.</para>
        /// </remarks>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public async Task DeleteMessageAsync() =>
            await ParentClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <remarks>
        /// <para>Adds a specified emote as a reaction to the message.</para>
        /// </remarks>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Reaction added</returns>
        public async Task<Reaction> AddReactionAsync(uint emoteId) =>
            await ParentClient.AddReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <remarks>
        /// <para>Removes a specified reaction from the message.</para>
        /// </remarks>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public async Task RemoveReactionAsync(uint emoteId) =>
            await ParentClient.RemoveReactionAsync(ChannelId, Id, emoteId).ConfigureAwait(false);
        #endregion
    }
}
