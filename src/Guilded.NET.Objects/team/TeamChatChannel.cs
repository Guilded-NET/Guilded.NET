using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams
{
    using Chat;
    /// <summary>
    /// Interface for team channels and categories.
    /// </summary>
    public class TeamChatChannel : TeamChannel<Guid>
    {
        /// <summary>
        /// Who archived this channel.
        /// </summary>
        /// <value>Archived by</value>
        [JsonProperty("archivedBy", Required = Required.AllowNull)]
        public GId ArchivedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel got archived.
        /// </summary>
        /// <value>Archived at</value>
        [JsonProperty("archivedAt", Required = Required.AllowNull)]
        public DateTime? ArchivedAt
        {
            get; set;
        }
        /// <summary>
        /// Type of this channel.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType ContentType
        {
            get; set;
        }
        /// <summary>
        /// ID of the parent channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("parentChannelId", Required = Required.AllowNull)]
        public Guid? ParentChannel
        {
            get; set;
        }
        /// <summary>
        /// When this channel got deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty("deletedAt", Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created this channel.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel should get archived.
        /// </summary>
        /// <value>Auto archive at</value>
        [JsonProperty("autoArchiveAt", Required = Required.AllowNull)]
        public DateTime? AutoArchiveAt
        {
            get; set;
        }
        /// <summary>
        /// Which webhook created this channel.
        /// </summary>
        /// <value>Created by webhook ID</value>
        [JsonProperty("createdByWebhookId", Required = Required.AllowNull)]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// Which webhook archived this channel.
        /// </summary>
        /// <value>Archived by webhook ID</value>
        [JsonProperty("archivedByWebhookId", Required = Required.AllowNull)]
        public Guid? ArchivedByWebhook
        {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="message">Message itself</param>
        public async Task SendMessageAsync(NewMessage message) =>
            await ParentClient.SendMessageAsync(Id, message);
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="message">Message itself</param>
        public void SendMessage(NewMessage message) =>
            ParentClient.SendMessage(Id, message);
        /// <summary>
        /// Gets a message in this channel.
        /// </summary>
        /// <param name="messageId">Message it should get</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageAsync(Guid messageId) =>
            await ParentClient.GetMessageAsync((await GetTeamAsync()).Subdomain, GroupId, Id, messageId);
        /// <summary>
        /// Gets a message in this channel.
        /// </summary>
        /// <param name="messageId">Message it should get</param>
        /// <returns>Message</returns>
        public Message GetMessage(Guid messageId) =>
            ParentClient.GetMessage(GetTeam().Subdomain, GroupId, Id, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public async Task<object> DeleteMessageAsync(Guid messageId) =>
            await ParentClient.DeleteMessageAsync(Id, messageId);
        /// <summary>
        /// Deletes a message posted in the chat.
        /// </summary>
        /// <param name="messageId">ID of the message to delete</param>
        public void DeleteMessage(Guid messageId) =>
            ParentClient.DeleteMessage(Id, messageId);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        /// <returns>Response</returns>
        public async Task<object> EditMessageAsync(Guid messageId, MessageContent content) =>
            await ParentClient.EditMessageAsync(Id, messageId, content);
        /// <summary>
        /// Edits message of the bot posted in the chat.
        /// </summary>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message</param>
        public void EditMessage(Guid messageId, MessageContent content) =>
            ParentClient.EditMessage(Id, messageId, content);
    }
}