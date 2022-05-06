using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Permissions;

namespace Guilded.Base;

public abstract partial class BaseGuildedClient
{
    #region Webhook
    /// <summary>
    /// Creates a <paramref name="message" /> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public abstract Task CreateHookMessageAsync(Guid webhook, string token, MessageContent message);
    /// <summary>
    /// Creates a message with content containing only <paramref name="content">text</paramref> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Content = content }).ConfigureAwait(false);
    /// <summary>
    /// Creates a message with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <param name="embeds">The list of embeds to add in the message</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Content = content, Embeds = embeds }).ConfigureAwait(false);
    /// <summary>
    /// Creates a message with content containing <paramref name="embeds" /> and <paramref name="content">text</paramref> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The text <paramref name="content" /> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <param name="embeds">The array of embeds to add in the message</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public async Task CreateHookMessageAsync(Guid webhook, string token, string content, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhook, token, content, (IList<Embed>)embeds).ConfigureAwait(false);
    /// <summary>
    /// Creates a message with content containing <paramref name="embeds" /> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="embeds">The list of embeds to add in the message</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public async Task CreateHookMessageAsync(Guid webhook, string token, IList<Embed> embeds) =>
        await CreateHookMessageAsync(webhook, token, new MessageContent { Embeds = embeds }).ConfigureAwait(false);
    /// <summary>
    /// Creates a message with content containing <paramref name="embeds" /> in a chat using a <paramref name="webhook">webhook</paramref>.
    /// </summary>
    /// <param name="webhook">The identifier of the webhook to execute</param>
    /// <param name="token">The required token for executing the webhook</param>
    /// <param name="embeds">The array of embeds to add in the message</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedRequestException"/>
    /// <exception cref="GuildedResourceException"/>
    public async Task CreateHookMessageAsync(Guid webhook, string token, params Embed[] embeds) =>
        await CreateHookMessageAsync(webhook, token, (IList<Embed>)embeds).ConfigureAwait(false);
    #endregion

    #region Chat channels
    /// <summary>
    /// Gets a set of messages from a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>By default, private messages will not be fetched. However, if private messages need to be included, <paramref name="includePrivate" /> parameter can be set as <see langword="true" />.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="includePrivate">Whether to get private replies or not</param>
    /// <param name="limit">The limit of how many messages to get (default — <c>50</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched messages</param>
    /// <param name="after">The min limit of the creation date of fetched messages</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <returns>List of messages</returns>
    public abstract Task<IList<Message>> GetMessagesAsync(Guid channel, bool includePrivate = false, uint? limit = null, DateTime? before = null, DateTime? after = null);
    /// <summary>
    /// Gets a <paramref name="message">message</paramref> from a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The identifier of the message it should get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <returns>Specified message</returns>
    public abstract Task<Message> GetMessageAsync(Guid channel, Guid message);
    /// <summary>
    /// Creates a new <paramref name="message" /> in a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The message to send</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <exception cref="ArgumentNullException">When the <see cref="MessageContent.Content">content</see> only consists of whitespace or is <see langword="null"/> and <see cref="MessageContent.Embeds">embeds</see> are also null or its array is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <see cref="MessageContent.Content"/> is above the message limit of 4000 characters</exception>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
    /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
    /// <returns>Created message</returns>
    public abstract Task<Message> CreateMessageAsync(Guid channel, MessageContent message);
    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)"/>
    /// <remarks>
    /// <para>The text <paramref name="content"/> will be formatted in Markdown.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
    /// <returns>Created message</returns>
    public async Task<Message> CreateMessageAsync(Guid channel, string content) =>
        await CreateMessageAsync(channel, new MessageContent(content)).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <param name="isPrivate">Whether the mention is private</param>
    /// <param name="isSilent">Whether the mention is silent and does not ping anyone</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <param name="replyTo">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, params Guid[] replyTo) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, string)"/>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message in Markdown</param>
    /// <param name="isPrivate">Whether the reply is private</param>
    /// <param name="isSilent">Whether the reply is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all messages it is replying to(5 max)</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, params Guid[] replyTo) =>
        await CreateMessageAsync(channel, new MessageContent(content)
        {
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)"/>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="embeds">The array of custom embeds that will be visible in the message</param>
    public async Task<Message> CreateMessageAsync(Guid channel, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, Embed[])"/>
    /// <remarks>
    /// <para>No text contents of the message will be displayed.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="isPrivate">Whether the mention/reply is private</param>
    /// <param name="isSilent">Whether the mention/reply is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all messages it is replying to(5 max)</param>
    /// <param name="embeds">The array of custom embeds that will be visible in the message</param>
    public async Task<Message> CreateMessageAsync(Guid channel, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, MessageContent)"/>
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message</param>
    /// <param name="embeds">The array of custom embeds that will be visible in the message</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds
        }).ConfigureAwait(false);
    /// <inheritdoc cref="CreateMessageAsync(Guid, string, Embed[])"/>
    /// <remarks>
    /// <para>The <paramref name="content">text contents</paramref> will be formatted in Markdown.</para>
    /// <para><paramref name="embeds">Embeds</paramref> will be displayed alongside <paramref name="content">text content</paramref>.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The text content of the message</param>
    /// <param name="isPrivate">Whether the mention/reply is private</param>
    /// <param name="isSilent">Whether the mention/reply is silent and does not ping anyone</param>
    /// <param name="replyTo">The array of all messages it is replying to(5 max)</param>
    /// <param name="embeds">The array of custom embeds that will be visible in the message</param>
    public async Task<Message> CreateMessageAsync(Guid channel, string content, bool isPrivate = false, bool isSilent = false, Guid[]? replyTo = null, params Embed[] embeds) =>
        await CreateMessageAsync(channel, new MessageContent
        {
            Content = content,
            Embeds = embeds,
            IsPrivate = isPrivate,
            IsSilent = isSilent,
            ReplyMessageIds = replyTo
        }).ConfigureAwait(false);
    /// <summary>
    /// Edits the <paramref name="content">text contents</paramref> of a <paramref name="message">message</paramref> in a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The identifier of the message to edit</param>
    /// <param name="content">The new text contents of the message in Markdown plain text</param>
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
    public abstract Task<Message> UpdateMessageAsync(Guid channel, Guid message, string content);
    /// <summary>
    /// Deletes a <paramref name="message">message</paramref> from a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The identifier of the message to delete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
    /// <permission cref="ChatPermissions.ManageMessages">Required for deleting messages made by others</permission>
    public abstract Task DeleteMessageAsync(Guid channel, Guid message);
    /// <summary>
    /// Adds a <paramref name="emote">reaction</paramref> to a <paramref name="message">message</paramref> in a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The identifier of the message to add a reaction on</param>
    /// <param name="emote">The identifier of the emote to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ChatPermissions.ReadMessages">Required for adding a reaction to a message you see</permission>
    /// <returns>Added reaction</returns>
    public abstract Task<Reaction> AddReactionAsync(Guid channel, Guid message, uint emote);
    // /// <summary>
    // /// Removes a <paramref name="emoteId">reaction</paramref> from a <paramref name="messageId">message</paramref> in a <paramref name="channelId">channel</paramref>.
    // /// </summary>
    // /// <param name="channelId">The identifier of the parent channel</param>
    // /// <param name="messageId">The identifier of the message to remove a reaction from</param>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // /// <exception cref="GuildedException"/>
    // /// <exception cref="GuildedPermissionException"/>
    // /// <exception cref="GuildedResourceException"/>
    // /// <exception cref="GuildedAuthorizationException"/>
    // /// <permission cref="ChatPermissions.ReadMessages">Required for removing a reaction from a message you see</permission>
    // public abstract Task RemoveReactionAsync(Guid channelId, Guid messageId, uint emoteId);
    #endregion

    #region Forum channels
    /// <summary>
    /// Creates a new post in a <paramref name="channel">forum channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="title">The title of the forum post</param>
    /// <param name="content">The content of the forum post</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ForumPermissions.ReadForums">Required to create a forum thread in forums you can read</permission>
    /// <permission cref="ForumPermissions.CreateTopics">Required to create forum threads</permission>
    /// <returns>Created forum thread</returns>
    public abstract Task<ForumThread> CreateForumThreadAsync(Guid channel, string title, string content);
    #endregion

    #region List channels
    /// <summary>
    /// Gets a set of list items from a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the channel to get list items from</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to get a set of list items</permission>
    /// <returns>List of list items</returns>
    public abstract Task<IList<ListItemSummary>> GetListItemsAsync(Guid channel);
    /// <summary>
    /// Gets a <paramref name="listItem">list item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="listItem">The identifier of the list item to get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to get a list item in list channel you can view</permission>
    /// <returns>Specified list item</returns>
    public abstract Task<ListItem> GetListItemAsync(Guid channel, Guid listItem);
    /// <summary>
    /// Creates a new item in a <paramref name="channel">list</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="message">The text content of the list item</param>
    /// <param name="note">The text content of an optional note in the list item</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to create a list item in list channel you can view</permission>
    /// <permission cref="ListPermissions.CreateListItem">Required to create list items</permission>
    /// <returns>Created list item</returns>
    public abstract Task<ListItem> CreateListItemAsync(Guid channel, string message, string? note = null);
    /// <summary>
    /// Edits the <paramref name="message">text contents</paramref> of the <paramref name="listItem">list item</paramref> or the <paramref name="note">item's note</paramref> in a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="listItem">The identifier of the list item to edit</param>
    /// <param name="message">The new text content of the list item</param>
    /// <param name="note">The new text content of the note in the list item</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to update a list item in list channel you can view</permission>
    /// <permission cref="ListPermissions.ManageListItems">Required to update list items you don't own</permission>
    /// <returns>Updated list item</returns>
    public abstract Task<ListItem> UpdateListItemAsync(Guid channel, Guid listItem, string message, string? note = null);
    /// <summary>
    /// Deletes an <paramref name="listItem">item</paramref> from a <paramref name="channel">list channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the channel where the list item is</param>
    /// <param name="listItem">The identifier of the list item to delete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to delete a list item in list channel you can view</permission>
    /// <permission cref="ListPermissions.RemoveListItems">Required to delete list items</permission>
    public abstract Task DeleteListItemAsync(Guid channel, Guid listItem);
    /// <summary>
    /// Marks an <paramref name="listItem">item</paramref> from a <paramref name="channel">list channel</paramref> as completed.
    /// </summary>
    /// <param name="channel">The identifier of the channel where the list item is</param>
    /// <param name="listItem">The identifier of the list item to complete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to delete a list item in list channel you can view</permission>
    /// <permission cref="ListPermissions.CompleteListItems">Required to complete list items</permission>
    public abstract Task CompleteListItemAsync(Guid channel, Guid listItem);
    /// <summary>
    /// Marks an <paramref name="listItem">item</paramref> from a <paramref name="channel">list channel</paramref> as completed.
    /// </summary>
    /// <param name="channel">The identifier of the channel where the list item is</param>
    /// <param name="listItem">The identifier of the list item to complete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="ListPermissions.ViewListItems">Required to delete a list item in list channel you can view</permission>
    /// <permission cref="ListPermissions.CompleteListItems">Required to complete list items</permission>
    public abstract Task UncompleteListItemAsync(Guid channel, Guid listItem);
    #endregion

    #region Docs channel
    /// <summary>
    /// Gets a set of documents from a <paramref name="channel">docs channel</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>Only gets 50 documents as of now.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="limit">The limit of how many docs to get (default — <c>25</c>, values — <c>(0, 100]</c>)</param>
    /// <param name="before">The max limit of the creation date of fetched docs</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required to view a list of documents</permission>
    /// <returns>List of documents</returns>
    public abstract Task<IList<Doc>> GetDocsAsync(Guid channel, uint? limit = null, DateTime? before = null);
    /// <summary>
    /// Gets a <paramref name="doc">document</paramref> from a <paramref name="channel">doc channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="doc">The identifier of the document to get</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required to view a list of documents</permission>
    /// <returns>Specified document</returns>
    public abstract Task<Doc> GetDocAsync(Guid channel, uint doc);
    /// <summary>
    /// Creates a new document in a <paramref name="channel">doc channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="title">The title of this document</param>
    /// <param name="content">The Markdown content of this document</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required to create a document in document channel you can view</permission>
    /// <permission cref="DocPermissions.CreateDocs">Required to create document</permission>
    /// <returns>Created document</returns>
    public abstract Task<Doc> CreateDocAsync(Guid channel, string title, string content);
    /// <summary>
    /// Edits the <paramref name="content">text contents</paramref> or the <paramref name="title" /> of a <paramref name="doc">document</paramref> in a <paramref name="channel">docs channel</paramref>.
    /// </summary>
    /// <remarks>
    /// <para>The updated document will be bumped to the top.</para>
    /// </remarks>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="doc">The identifier of the document to update/edit</param>
    /// <param name="title">The new title of this document</param>
    /// <param name="content">The new Markdown content of this document</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required to update a document in document channel you can view</permission>
    /// <permission cref="DocPermissions.ManageDocs">Required to update/edit documents of others</permission>
    /// <returns>Updated document</returns>
    public abstract Task<Doc> UpdateDocAsync(Guid channel, uint doc, string title, string content);
    /// <summary>
    /// Deletes a <paramref name="doc">document</paramref> from a <paramref name="channel">docs channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="doc">The identifier of the document to delete</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required to update a document in document channel you can view</permission>
    /// <permission cref="DocPermissions.ManageDocs">Required to update/edit documents of others</permission>
    public abstract Task DeleteDocAsync(Guid channel, uint doc);
    #endregion

    #region Content
    /// <summary>
    /// Adds a <paramref name="emote">reaction</paramref> to the <paramref name="content">content</paramref> in a <paramref name="channel">channel</paramref>.
    /// </summary>
    /// <param name="channel">The identifier of the parent channel</param>
    /// <param name="content">The identifier of the content to add a reaction on</param>
    /// <param name="emote">The identifier of the emote to add</param>
    /// <exception cref="GuildedException"/>
    /// <exception cref="GuildedPermissionException"/>
    /// <exception cref="GuildedResourceException"/>
    /// <exception cref="GuildedAuthorizationException"/>
    /// <permission cref="DocPermissions.ViewDocs">Required for adding a reaction to a document you see</permission>
    /// <permission cref="MediaPermissions.SeeMedia">Required for adding a reaction to a media post you see</permission>
    /// <permission cref="ForumPermissions.ReadForums">Required for adding a reaction to a forum thread you see</permission>
    /// <permission cref="CalendarPermissions.ViewEvents">Required for adding a reaction to a calendar event you see</permission>
    /// <returns>Reaction added</returns>
    public abstract Task<Reaction> AddReactionAsync(Guid channel, uint content, uint emote);
    // /// <summary>
    // /// Removes a <paramref name="emoteId">reaction</paramref> from the <paramref name="contentId">content</paramref> in a <paramref name="channelId">channel</paramref>.
    // /// </summary>
    // /// <param name="channelId">The identifier of the parent channel</param>
    // /// <param name="contentId">The identifier of the content to remove a reaction from</param>
    // /// <param name="emoteId">The identifier of the emote to remove</param>
    // /// <exception cref="GuildedException"/>
    // /// <exception cref="GuildedPermissionException"/>
    // /// <exception cref="GuildedResourceException"/>
    // /// <exception cref="GuildedAuthorizationException"/>
    // /// <permission cref="DocPermissions.ViewDocs">Required for removing a reaction from a document you see</permission>
    // /// <permission cref="MediaPermissions.SeeMedia">Required for removing a reaction from a media post you see</permission>
    // /// <permission cref="ForumPermissions.ReadForums">Required for removing a reaction from a forum thread you see</permission>
    // /// <permission cref="CalendarPermissions.ViewEvents">Required for removing a reaction from a calendar event you see</permission>
    // public abstract Task RemoveReactionAsync(Guid channelId, uint contentId, uint emoteId);
    #endregion
}