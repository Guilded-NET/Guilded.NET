using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Content;
    using Base.Permissions;
    public abstract partial class AbstractGuildedClient
    {
        private const int messageLimit = 4000;

        // #region Webhook
        // /// <summary>
        // /// Creates a webhook in a given channel.
        // /// </summary>
        // /// <param name="channelId">The identifier of the parent channel</param>
        // /// <param name="name">The name of the webhook</param>
        // /// <exception cref="GuildedException"/>
        // /// <exception cref="GuildedPermissionException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Created webhook</returns>
        // public override async Task<Webhook> CreateWebhookAsync(Guid channelId, string name) =>
        //     await GetObject<Webhook>("webhooks", Method.POST, body: new
        //     {
        //         channelId,
        //         name
        //     });
        // /// <summary>
        // /// Updates webhook's name or profile picture.
        // /// </summary>
        // /// <param name="channelId">The identifier of the parent channel    `</param>
        // /// <param name="webhookId">The identifier of the webhook to update</param>
        // /// <param name="name">The new name of the webhook</param>
        // /// <param name="avatar">The new profile picture/icon of the webhook</param>
        // /// <exception cref="GuildedException"/>
        // /// <exception cref="GuildedPermissionException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Updated webhook</returns>
        // public override async Task<Webhook> UpdateWebhookAsync(Guid channelId, Guid webhookId, string name = null, Uri avatar = null)
        // {
        //     // Expect both arguments
        //     if (name is null && avatar is null)
        //         throw new ArgumentException($"Both {nameof(name)} and {nameof(avatar)} cannot be null");
        //     
        //     Dictionary<string, object> change = new Dictionary<string, object>()
        //     {
        //         {"channelId", channelId}
        //     };
        //     if (!(name is null))
        //         change.Add("name", name);
        //     if (!(avatar is null))
        //         change.Add("iconUrl", avatar);
        //     
        //     return await GetObject<Webhook>($"webhooks/{webhookId}", Method.PUT, body: change);
        // }
        // /// <summary>
        // /// Deletes a given webhook.
        // /// </summary>
        // /// <param name="webhookId">The identifier of the webhook to delete</param>
        // /// <exception cref="GuildedException"/>
        // /// <exception cref="GuildedPermissionException"/>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Deleted webhook</returns>
        // public override async Task<Webhook> DeleteWebhookAsync(Guid webhookId) =>
        //     await GetObject<Webhook>($"webhooks/{webhookId}", Method.DELETE);
        // /// <summary>
        // /// Posts a message using a webhook.
        // /// </summary>
        // /// <param name="webhookId">The identifier of the webhook</param>
        // /// <param name="token">The token of this webhook</param>
        // /// <param name="content">The message to send using the webhook</param>
        // /// <param name="embeds">An array of embeds to send</param>
        // /// <exception cref="GuildedException"/>
        // public override async Task ExecuteWebhookAsync(Guid webhookId, string token, string content = null, params Embed[] embeds) =>
        //     await ExecuteRequest(new Uri(GuildedUrl.Media, $"webhooks/{webhookId}/{token}"), Method.POST, body: new
        //     {
        //         content,
        //         embeds
        //     });
        // #endregion

        #region Chat channel
        /// <summary>
        /// Gets messages with a specific limit.
        /// </summary>
        /// <remarks>
        /// <para>Gets a list of messages in a specified channel of identifier <paramref name="channelId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="limit">How many messages it should get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>List of messages</returns>
        public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, uint limit = 50) =>
            // TODO: Add limit query
            await GetObject<IList<Message>>($"channels/{channelId}/messages", Method.GET, key: "messages").ConfigureAwait(false);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <remarks>
        /// <para>Gets a specified message with an identifier <paramref name="messageId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of message it should get</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>Message</returns>
        public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.GET, key: "message").ConfigureAwait(false);
        /// <summary>
        /// A base for creating a message
        /// </summary>
        /// <param name="channelId">Endpoint path channelId parameter</param>
        /// <param name="message">Request body</param>
        /// <returns>Created message</returns>
        private async Task<Message> CreateMessageAsync(Guid channelId, CreatableMessage message) =>
            await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", message).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new message with <paramref name="content"/> formatted in Markdown.</para>
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
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            else if (content.Length > messageLimit)
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            else
                return await CreateMessageAsync(channelId, new CreatableMessage { Content = content }).ConfigureAwait(false);
        }
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content, params Guid[] replyMessageIds)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content,
                    ReplyMessageIds = replyMessageIds
                }).ConfigureAwait(false);
            }
        }
        /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <param name="isPrivate">Whether the reply is private</param>
        /// <param name="replyMessageIds">The array of all messages it is replying to(5 max)</param>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content, bool isPrivate, params Guid[] replyMessageIds)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content,
                    IsPrivate = isPrivate,
                    ReplyMessageIds = replyMessageIds
                }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>Edits the message <paramref name="messageId"/>, if the specified message is from the client. This does not work if the client is not the creator of the message.</para>
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
        public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            else if (content.Length > messageLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            }
            else
            {
                return await CreateMessageAsync(channelId, new CreatableMessage
                {
                    Content = content
                }).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <remarks>
        /// <para>Removes the message of identifier <paramref name="messageId"/>, whether it be from the client or another user.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to delete</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
            await ExecuteRequest($"channels/{channelId}/messages/{messageId}", Method.DELETE).ConfigureAwait(false);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <remarks>
        /// <para>Adds a reaction of identifier <paramref name="emoteId"/> to <paramref name="messageId"/>.</para>
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
        public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.PUT, key: "emote").ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <remarks>
        /// <para>Remove a reaction of identifier <paramref name="emoteId"/> from <paramref name="messageId"/>.</para>
        /// </remarks>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException"/>
        /// <exception cref="GuildedAuthorizationException"/>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.DELETE).ConfigureAwait(false);
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
        public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            else if(string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            return await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            }).ConfigureAwait(false);
        }
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
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null)
        {
            if(string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            }).ConfigureAwait(false);
        }
        #endregion

        #region Content
        /// <summary>
        /// Adds a reaction to the content.
        /// </summary>
        /// <remarks>
        /// <para>Adds a reaction of identifier <paramref name="emoteId"/> to content of identifier <paramref name="contentId"/>.</para>
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
        public override async Task<Reaction> AddReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.PUT, key: "emote").ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from the content.
        /// </summary>
        /// <remarks>
        /// <para>Remove a reaction of identifier <paramref name="emoteId"/> from content of identifier <paramref name="contentId"/>.</para>
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
        public override async Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{contentId}/emotes/{emoteId}", Method.DELETE).ConfigureAwait(false);
        #endregion
    }
}