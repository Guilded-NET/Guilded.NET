using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guilded.NET.Base
{
    using Embeds;
    using Content;
    using Permissions;
    public abstract partial class BaseGuildedClient
    {
        #region Webhook
        /// <summary>
        /// Creates a message in a chat using provided webhook.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message using the specified webhook.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="webhookId">The identifier of the webhook to execute</param>
        /// <param name="token">The token of executed webhook</param>
        /// <param name="content">The contents of message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedResourceException"/>
        public abstract Task CreateHookMessageAsync(Guid webhookId, string token, string content);
        /// <summary>
        /// Creates a message in a chat using provided webhook.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message using the specified webhook.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="webhookId">The identifier of the webhook to execute</param>
        /// <param name="token">The token of executed webhook</param>
        /// <param name="content">The contents of message in Markdown plain text</param>
        /// <param name="embeds">The list of embeds to add in the message</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedResourceException"/>
        public abstract Task CreateHookMessageAsync(Guid webhookId, string token, string content, IList<Embed> embeds);
        /// <summary>
        /// Creates a message in a chat using provided webhook.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message using the specified webhook.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="webhookId">The identifier of the webhook to execute</param>
        /// <param name="token">The token of executed webhook</param>
        /// <param name="content">The contents of message in Markdown plain text</param>
        /// <param name="embeds">The array of embeds to add in the message</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedResourceException"/>
        public async Task CreateHookMessageAsync(Guid webhookId, string token, string content, params Embed[] embeds) =>
            await CreateHookMessageAsync(webhookId, token, content, (IList<Embed>)embeds).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat using provided webhook.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message using the specified webhook.</para>
        /// </remarks>
        /// <param name="webhookId">The identifier of the webhook to execute</param>
        /// <param name="token">The token of executed webhook</param>
        /// <param name="embeds">The list of embeds to add in the message</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedResourceException"/>
        public abstract Task CreateHookMessageAsync(Guid webhookId, string token, IList<Embed> embeds);
        /// <summary>
        /// Creates a message in a chat using provided webhook.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message using the specified webhook.</para>
        /// </remarks>
        /// <param name="webhookId">The identifier of the webhook to execute</param>
        /// <param name="token">The token of executed webhook</param>
        /// <param name="embeds">The array of embeds to add in the message</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedRequestException"/>
        /// <exception cref="GuildedResourceException"/>
        public async Task CreateHookMessageAsync(Guid webhookId, string token, params Embed[] embeds) =>
            await CreateHookMessageAsync(webhookId, token, (IList<Embed>)embeds).ConfigureAwait(false);

        #endregion

        #region Chat channels
        /// <summary>
        /// Gets a set of messages.
        /// </summary>
        /// <remarks>
        /// <para>Gets a set of messages in the specified channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="includePrivate">Whether to get private replies or not</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>List of messages</returns>
        public abstract Task<IList<Message>> GetMessagesAsync(Guid channelId, bool includePrivate = false);
        /// <summary>
        /// Gets a message.
        /// </summary>
        /// <remarks>
        /// <para>Gets the specified message.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of message it should get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>Message</returns>
        public abstract Task<Message> GetMessageAsync(Guid channelId, Guid messageId);
        /// <summary>
        /// Creates a message in chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new chat messsage in the specified channel.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content, params Guid[] replyMessageIds);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, string content, bool isPrivate, params Guid[] replyMessageIds);
        /// <summary>
        /// Updates the specified message.
        /// </summary>
        /// <remarks>
        /// <para>Edits the contents of the specified message.</para>
        /// <para>The <paramref name="content"/> will be formatted in Markdown.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to edit</param>
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
        /// <returns>Message updated</returns>
        public abstract Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content);
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <remarks>
        /// <para>Removes the specified message, whether it be from the client or another user.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to delete</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public abstract Task DeleteMessageAsync(Guid channelId, Guid messageId);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <remarks>
        /// <para>Adds a specified emote as a reaction to the given message.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Reaction added</returns>
        public abstract Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <remarks>
        /// <para>Removes a specified reaction from the given message.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public abstract Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        #endregion

        #region Forum channels
        /// <summary>
        /// Creates a forum thread.
        /// </summary>
        /// <remarks>
        /// <para>Creates a forum thread/post in forums.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ForumPermissions.ReadForums">Required to create a forum thread in forums you can read</permission>
        /// <permission cref="ForumPermissions.CreateTopics">Required to create forum threads</permission>
        /// <returns>Forum thread created</returns>
        public abstract Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content);
        #endregion

        #region List channels
        /// <summary>
        /// Creates a list item.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new list item in list/task channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ListPermissions.ViewListItems">Required to create a list item in list channel you can view</permission>
        /// <permission cref="ListPermissions.CreateListItem">Required to create list items</permission>
        /// <returns>List item created</returns>
        public abstract Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null);
        #endregion

        #region Content
        /// <summary>
        /// Adds a reaction to the content.
        /// </summary>
        /// <remarks>
        /// <para>Adds a specified emote as a reaction to the given content.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="contentId">The identifier of the content to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required for adding a reaction to a document you see</permission>
        /// <permission cref="MediaPermissions.SeeMedia">Required for adding a reaction to a media post you see</permission>
        /// <permission cref="ForumPermissions.ReadForums">Required for adding a reaction to a forum thread you see</permission>
        /// <permission cref="CalendarPermissions.ViewEvents">Required for adding a reaction to a calendar event you see</permission>
        /// <returns>Reaction added</returns>
        public abstract Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId);
        /// <summary>
        /// Removes a reaction from the content.
        /// </summary>
        /// <remarks>
        /// <para>Removes a specified reaction from the given message.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="contentId">The identifier of the content to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required for removing a reaction from a document you see</permission>
        /// <permission cref="MediaPermissions.SeeMedia">Required for removing a reaction from a media post you see</permission>
        /// <permission cref="ForumPermissions.ReadForums">Required for removing a reaction from a forum thread you see</permission>
        /// <permission cref="CalendarPermissions.ViewEvents">Required for removing a reaction from a calendar event you see</permission>
        public abstract Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId);
        #endregion
    }
}