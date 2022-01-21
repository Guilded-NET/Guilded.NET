using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Guilded.NET.Base.Content;
using Guilded.NET.Base.Embeds;
using Guilded.NET.Base.Permissions;

namespace Guilded.NET.Base
{
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
        /// <returns>Specified message</returns>
        public abstract Task<Message> GetMessageAsync(Guid channelId, Guid messageId);
        /// <summary>
        /// Creates a message in chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new chat messsage in the specified channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The message to send</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Created message</returns>
        public abstract Task<Message> CreateMessageAsync(Guid channelId, MessageContent message);
        /// <summary>
        /// Creates a message in the chat.
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
        /// <returns>Created message</returns>
        public async Task<Message> CreateMessageAsync(Guid channelId, string content) =>
            await CreateMessageAsync(channelId, new MessageContent(content)).ConfigureAwait(false);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public async Task<Message> CreateMessageAsync(Guid channelId, string content, params Guid[] replyMessageIds) =>
            await CreateMessageAsync(channelId, new MessageContent(content)
            {
                ReplyMessageIds = replyMessageIds
            }).ConfigureAwait(false);
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public async Task<Message> CreateMessageAsync(Guid channelId, string content, bool isPrivate, params Guid[] replyMessageIds) =>
            await CreateMessageAsync(channelId, new MessageContent(content)
            {
                IsPrivate = isPrivate,
                ReplyMessageIds = replyMessageIds
            }).ConfigureAwait(false);
        /// <summary>
        /// Updates the message.
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
        /// <returns>Updated message</returns>
        public abstract Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content);
        /// <summary>
        /// Deletes the message.
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
        /// Adds a reaction to the message.
        /// </summary>
        /// <remarks>
        /// <para>Adds a specified emote as a reaction to the specified message.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Added reaction</returns>
        public abstract Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId);
        /// <summary>
        /// Removes a reaction from the message.
        /// </summary>
        /// <remarks>
        /// <para>Removes a specified reaction from the specified message.</para>
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
        /// Creates a thread in forums.
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
        /// <returns>Created forum thread</returns>
        public abstract Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content);
        #endregion

        #region List channels
        /// <summary>
        /// Creates an item in the list.
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
        /// <returns>Created list item</returns>
        public abstract Task<ListItem> CreateListItemAsync(Guid channelId, string message, string? note = null);
        #endregion

        #region Docs channel
        /// <summary>
        /// Gets a list of documents.
        /// </summary>
        /// <remarks>
        /// <para>Gets latest 50 documents in a specified document channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required to view a list of documents</permission>
        /// <returns>List of documents</returns>
        public abstract Task<IList<Doc>> GetDocsAsync(Guid channelId);
        /// <summary>
        /// Gets the specified document.
        /// </summary>
        /// <remarks>
        /// <para>Gets the specified document in the provided channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="docId">The identifier of the document to get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required to view a list of documents</permission>
        /// <returns>Specified document</returns>
        public abstract Task<Doc> GetDocAsync(Guid channelId, uint docId);
        /// <summary>
        /// Creates a document in document list.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new document in a document channel.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="title">The title of this document</param>
        /// <param name="content">The Markdown content of this document</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required to create a document in document channel you can view</permission>
        /// <permission cref="DocPermissions.CreateDocs">Required to create document</permission>
        /// <returns>Created document</returns>
        public abstract Task<Doc> CreatedDocAsync(Guid channelId, string title, string content);
        /// <summary>
        /// Updates the document.
        /// </summary>
        /// <remarks>
        /// <para>Updates/edits the specified document.</para>
        /// <para>This will bump the document to the very top.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="docId">The identifier of the document to update/edit</param>
        /// <param name="title">The new title of this document</param>
        /// <param name="content">The new Markdown content of this document</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required to update a document in document channel you can view</permission>
        /// <permission cref="DocPermissions.ManageDocs">Required to update/edit documents of others</permission>
        /// <returns>Updated document</returns>
        public abstract Task<Doc> UpdateDocAsync(Guid channelId, uint docId, string title, string content);
        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <remarks>
        /// <para>Deletes the specified document.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="docId">The identifier of the document to delete</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="DocPermissions.ViewDocs">Required to update a document in document channel you can view</permission>
        /// <permission cref="DocPermissions.ManageDocs">Required to update/edit documents of others</permission>
        public abstract Task DeleteDocAsync(Guid channelId, uint docId);
        #endregion

        #region Content
        /// <summary>
        /// Adds a reaction to the content.
        /// </summary>
        /// <remarks>
        /// <para>Adds a specified emote as a reaction to the specified content.</para>
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
        /// <para>Removes a specified reaction from the specified content.</para>
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