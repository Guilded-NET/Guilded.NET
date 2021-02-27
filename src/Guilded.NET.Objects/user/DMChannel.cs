using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Guilded.NET.Objects.Chat;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Users
{
    using Teams;
    /// <summary>
    /// Represents DMs and DM groups.
    /// </summary>
    public class DMChannel : BaseChannel<Guid>
    {
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
        /// Who created this channel.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty("ownerId", Required = Required.Always)]
        public GId OwnerId
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
        /// All users in this DM channel.
        /// </summary>
        /// <value>DM channel users</value>
        [JsonProperty("users", Required = Required.Always)]
        public IList<DMUser> Users
        {
            get; set;
        }
        /// <summary>
        /// If this DM channel is a group or default.
        /// </summary>
        /// <value>Type</value>
        [JsonProperty("dmType", Required = Required.Always)]
        public DMType DMType
        {
            get; set;
        }
        /// <summary>
        /// Last message posted in this channel.
        /// </summary>
        /// <value>Last message</value>
        [JsonProperty("lastMessage", Required = Required.AllowNull)]
        public Message LastMessage
        {
            get; set;
        }
        /// <summary>
        /// Type of this channel.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType Type
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
        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"DMs {Id}: [{string.Join(", ", Users.Select(x => x.Id))}]";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) =>
            obj is DMChannel ch && ch.Id == Id;
        /// <summary>
        /// Whether or not channels are equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(DMChannel ch0, DMChannel ch1) => ch0.Id == ch1.Id;
        /// <summary>
        /// Whether or not channels are not equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(DMChannel ch0, DMChannel ch1) => !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (Id.GetHashCode() + 2000) / 2;
    }
}