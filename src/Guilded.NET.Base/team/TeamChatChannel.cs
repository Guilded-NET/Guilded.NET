using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Chat;
    /// <summary>
    /// Interface for team channels and categories.
    /// </summary>
    public abstract class TeamChannel : TeamChannel<Guid>
    {
        #region JSON properties
        /// <summary>
        /// Who archived this channel.
        /// </summary>
        /// <value>Archived by</value>
        [JsonProperty(Required = Required.AllowNull)]
        public GId ArchivedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel got archived.
        /// </summary>
        /// <value>Archived at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? ArchivedAt
        {
            get; set;
        }
        /// <summary>
        /// Type of this channel.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty(Required = Required.Always)]
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
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created this channel.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel should get archived.
        /// </summary>
        /// <value>Auto archive at</value>
        [JsonProperty(Required = Required.AllowNull)]
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
        #endregion

        
        #region Additional
        /// <summary>
        /// Sends a message to the specific channel.
        /// </summary>
        /// <param name="message">Message to send to the channel</param>
        public async Task CreateMessageAsync(string message) =>
            await ParentClient.CreateMessageAsync(Id, message);
        /// <summary>
        /// Gets a message in this channel.
        /// </summary>
        /// <param name="messageId">Message it should get</param>
        /// <returns>Message</returns>
        public async Task<Message> GetMessageAsync(Guid messageId) =>
            await ParentClient.GetMessageAsync(Id, messageId);
        /// <summary>
        /// Deletes a specified message.
        /// </summary>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>Response</returns>
        public async Task DeleteMessageAsync(Guid messageId) =>
            await ParentClient.DeleteMessageAsync(Id, messageId);
        /// <summary>
        /// Updates the contents of the message.
        /// </summary>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="content">New content of the message to replace old content with</param>
        /// <returns>Response</returns>
        public async Task UpdateMessageAsync(Guid messageId, string content) =>
            await ParentClient.UpdateMessageAsync(Id, messageId, content);
        #endregion
    }
}