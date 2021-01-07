using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Events {
    using Chat;
    using Teams;
    /// <summary>
    /// Event when message is posted in the chat.
    /// </summary>
    public class MessageCreatedEvent: CommonEvent {
        /// <summary>
        /// Type of the content.
        /// </summary>
        /// <value>Content type</value>
        [JsonProperty("contentType")]
        public ChannelType ContentType {
            get; set;
        }
        /// <summary>
        /// The message which was posted.
        /// </summary>
        /// <value>Message</value>
        [JsonProperty("message")]
        public Message Message {
            get; set;
        }
        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="response">Message response</param>
        /// <returns>Async task</returns>
        public async Task<object> RespondAsync(NewMessage response) =>
            await ParentClient.SendMessageAsync(ChannelId, response);
        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="response">Message response</param>
        public void Respond(NewMessage response) =>
            ParentClient.SendMessage(ChannelId, response);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        /// <returns>Async task</returns>
        public async Task<object> DeleteAsync() =>
            await ParentClient.DeleteMessageAsync(ChannelId, Message.Id);
        /// <summary>
        /// Deletes this message.
        /// </summary>
        public void Delete() =>
            ParentClient.DeleteMessage(ChannelId, Message.Id);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <returns>Parent channel</returns>
        public async Task<BaseChannel> GetChannelAsync() =>
            await ParentClient.GetChannelAsync(TeamId, ChannelId);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <returns>Parent channel</returns>
        public BaseChannel GetChannel() =>
            ParentClient.GetChannel(TeamId, ChannelId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public async Task<Team> GetTeamAsync() =>
            await ParentClient.GetTeamAsync(TeamId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public Team GetTeam() =>
            ParentClient.GetTeam(TeamId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public async Task<User> GetAuthorAsync() =>
            await ParentClient.GetUserAsync(Message.AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public User GetAuthor() =>
            ParentClient.GetUser(Message.AuthorId);
        /// <summary>
        /// If this message was posted by given user.
        /// </summary>
        /// <param name="user">User to check if it's message author</param>
        /// <returns>Message by that user</returns>
        public bool IsMessageOf(User user) =>
            Message.IsMessageOf(user);
    }
}