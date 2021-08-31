using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Chat;
    using Base.Content;
    using Base.Embeds;
    using Base.Permissions;
    using Base.Teams;
    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    public abstract partial class AbstractGuildedClient
    {
        private const int messageLimit = 4000;

        // #region Webhook
        // /// <summary>
        // /// Creates a webhook in a given channel.
        // /// </summary>
        // /// <param name="channelId">The identifier of the parent channel</param>
        // /// <param name="name">The name of the webhook</param>
        // /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        // /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
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
        // /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        // /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        // /// <permission cref="GeneralPermissions.ManageWebhooks">Required for managing webhooks</permission>
        // /// <returns>Updated webhook</returns>
        // public override async Task<Webhook> UpdateWebhookAsync(Guid channelId, Guid webhookId, string name = null, Uri avatar = null)
        // {
        //     // If both arguments are null
        //     if (name is null && avatar is null) throw new ArgumentException($"Both {nameof(name)} and {nameof(avatar)} cannot be null");
        //     // Creates a dictionary for the Webhook update
        //     Dictionary<string, object> change = new Dictionary<string, object>()
        //     {
        //         {"channelId", channelId}
        //     };
        //     if (!(name is null)) change.Add("name", name);
        //     if (!(avatar is null)) change.Add("iconUrl", avatar);
        //     // Sets a new name, avatar or both
        //     return await GetObject<Webhook>($"webhooks/{webhookId}", Method.PUT, body: change);
        // }
        // /// <summary>
        // /// Deletes a given webhook.
        // /// </summary>
        // /// <param name="webhookId">The identifier of the webhook to delete</param>
        // /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        // /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
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
        // /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
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
        /// <example>
        /// <code>
        /// IList&lt;Message&gt; msg = await client.GetMessagesAsync(message.ChannelId);
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="limit">How many messages it should get</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>List of messages</returns>
        public override async Task<IList<Message>> GetMessagesAsync(Guid channelId, uint limit = 50) =>
            // TODO: Add limit query
            await GetObject<IList<Message>>($"channels/{channelId}/messages", Method.GET, key: "messages").ConfigureAwait(false);
        /// <summary>
        /// Gets a message in a specific channel.
        /// </summary>
        /// <example>
        /// <code>
        /// Message msg = await client.GetMessageAsync(channelId, arg);
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of message it should get</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <returns>Message</returns>
        public override async Task<Message> GetMessageAsync(Guid channelId, Guid messageId) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.GET, key: "message").ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.CreateMessageAsync(channelId, new MessageContent
        /// (
        ///     new Leaf("Welcome to "),
        ///     new Leaf(team.Name, MarkType.Bold),
        ///     new Leaf("!")
        /// );
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public override async Task<Message> CreateMessageAsync(Guid channelId, MessageContent content) =>
            await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", new { content }).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.CreateMessageAsync(channelId, "Hello!");
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public override async Task<Message> CreateMessageAsync(Guid channelId, string content)
        {
            // Don't allow to send empty messages
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            // Make sure it is on the limit
            else if (content.Length > messageLimit) throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            // Creates a new message
            return await GetObject<Message>($"channels/{channelId}/messages", Method.POST, "message", new { content }).ConfigureAwait(false);
        }
        /// <summary>
        /// Updates the contents of a message.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.UpdateMessageAsync(channelId, messageId, new MessageContent("Edited message"));
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to edit</param>
        /// <param name="content">The new content of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, MessageContent content) =>
            await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.PUT, "message", new { content }).ConfigureAwait(false);
        /// <summary>
        /// Updates the contents of a message.
        /// </summary>
        /// <remarks>
        /// <para>Updates the contents of a message based on Markdown, if the permissions are met.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// await client.UpdateMessageAsync(channelId, messageId, "Edited message");
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to edit</param>
        /// <param name="content">The new content of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for editing your own messages posted in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for editing your own messages posted in a thread</permission>
        /// <returns>Message edited</returns>
        public override async Task<Message> UpdateMessageAsync(Guid channelId, Guid messageId, string content)
        {
            // Don't allow to send empty messages
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            // Make sure it is on the limit
            else if (content.Length > messageLimit) throw new ArgumentOutOfRangeException(nameof(content), content, $"{nameof(content)} exceeds the 4000 character message limit");
            // Update the message
            return await GetObject<Message>($"channels/{channelId}/messages/{messageId}", Method.PUT, "message", new { content }).ConfigureAwait(false);
        }
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.DeleteMessageAsync(channelId, messageId);
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to delete</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
        public override async Task DeleteMessageAsync(Guid channelId, Guid messageId) =>
            await ExecuteRequest($"channels/{channelId}/messages/{messageId}", Method.DELETE).ConfigureAwait(false);
        /// <summary>
        /// Adds a reaction to a message.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.AddReactionAsync(channelId, messageId, 90002569);
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to add a reaction on</param>
        /// <param name="emoteId">The identifier of the emote to add</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
        /// <returns>Reaction added</returns>
        public override async Task<Reaction> AddReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await GetObject<Reaction>($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.PUT, key: "emote").ConfigureAwait(false);
        /// <summary>
        /// Removes a reaction from a message.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.RemoveReactionAsync(channelId, messageId, 90002569);
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="messageId">The identifier of the message to remove a reaction from</param>
        /// <param name="emoteId">The identifier of the emote to remove</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel <paramref name="channelId"/>, the message <paramref name="messageId"/> or both have not been found</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
        public override async Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId) =>
            await ExecuteRequest($"channels/{channelId}/content/{messageId}/emotes/{emoteId}", Method.DELETE).ConfigureAwait(false);
        // /// <summary>
        // /// Starts typing in a specific channel.
        // /// </summary>
        // /// <param name="channelId">The identifier of the channel to type</param>
        // public void StartTyping(Guid channelId)
        // {
        //     // Makes sure that the main/default WebSocket exists
        //     if (!Websockets.ContainsKey(""))
        //         throw new KeyNotFoundException("Could not find default WebSocket");
        //     // Sends typing data to the WebSocket
        //     Websockets[""].Send($"42[\"ChatChannelTyping\",{{\"channelId\":\"{channelId}\"}}]");
        // }
        #endregion

        #region Forum channels
        /// <summary>
        /// Creates a forum post in a forum channel.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.CreateForumThreadAsync(channelId, "Daily post #1", new MessageContent
        /// (
        ///     new BlockQuote("..."),
        ///     new Paragraph("...")
        /// );
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ForumPermissions.ReadForums">Required to create a forum thread in forums you can read</permission>
        /// <permission cref="ForumPermissions.CreateTopics">Required to create forum threads</permission>
        /// <returns>Forum post created</returns>
        public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, MessageContent content) =>
            await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            }).ConfigureAwait(false);
        /// <summary>
        /// Creates a forum post in a forum channel.
        /// </summary>
        /// <example>
        /// <code>
        /// await client.CreateForumThreadAsync(channelId, "Daily post #1", "The first daily post ever!");
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="title">The title of the forum post</param>
        /// <param name="content">The content of the forum post</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ForumPermissions.ReadForums">Required to create a forum thread in forums you can read</permission>
        /// <permission cref="ForumPermissions.CreateTopics">Required to create forum threads</permission>
        /// <returns>Forum post created</returns>
        public override async Task<ForumThread> CreateForumThreadAsync(Guid channelId, string title, string content) =>
            await GetObject<ForumThread>($"channels/{channelId}/forum", Method.POST, "forumThread", new
            {
                title,
                content
            }).ConfigureAwait(false);
        #endregion

        #region List channels
        /// <summary>
        /// Creates a new list item in a list channel.
        /// </summary>
        /// <example>
        /// <para>Without a note:</para>
        /// <code>
        /// await client.CreateListItemAsync(channelId, new MessageContent("Reach {0} servers", count));
        /// </code>
        /// <para>With a note:</para>
        /// <code>
        /// await client.CreateListItemAsync(channelId,
        ///     new MessageContent("Reach {0} servers", count),
        ///     new MessageContent("Reach {0} servers with a bot", count)
        /// );
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ListPermissions.ViewListItems">Required to create a list item in list channel you can view</permission>
        /// <permission cref="ListPermissions.CreateListItem">Required to create list items</permission>
        /// <returns>List item created</returns>
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, MessageContent message, MessageContent note = null) =>
            await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            }).ConfigureAwait(false);
        /// <summary>
        /// Creates a new list item in a list channel.
        /// </summary>
        /// <example>
        /// <para>Without a note:</para>
        /// <code>
        /// await client.CreateListItemAsync(channelId, "Reach 100 servers");
        /// </code>
        /// <para>With a note:</para>
        /// <code>
        /// await client.CreateListItemAsync(channelId, "Reach 100 servers", "Reach 100 or more servers with a bot.");
        /// </code>
        /// </example>
        /// <param name="channelId">The identifier of the parent channel</param>
        /// <param name="message">The title content of this list item</param>
        /// <param name="note">The note of this list item</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="GuildedPermissionException">When the client is missing requested permissions</exception>
        /// <exception cref="GuildedResourceException">When the channel of identifier <paramref name="channelId"/> has not been found</exception>
        /// <permission cref="ListPermissions.ViewListItems">Required to create a list item in list channel you can view</permission>
        /// <permission cref="ListPermissions.CreateListItem">Required to create list items</permission>
        /// <returns>List item created</returns>
        public override async Task<ListItem> CreateListItemAsync(Guid channelId, string message, string note = null) =>
            await GetObject<ListItem>($"channels/{channelId}/list", Method.POST, "listItem", new
            {
                message,
                note
            }).ConfigureAwait(false);
        #endregion
    }
}