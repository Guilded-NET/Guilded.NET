using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A base for message-related information.
    /// </summary>
    public class BaseMessage : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the channel this message is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await ParentClient.CreateMessageAsync(ChannelId, content);
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string content) =>
            await ParentClient.CreateMessageAsync(ChannelId, content);
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args));
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args));
        /// <summary>
        /// Creates a new message in same channel as a response.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException">When the client receives an error from Guilded API</exception>
        /// <exception cref="ArgumentNullException">When the given content only consists of whitespace or is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the contents of the message are above the message limit of 4000 characters</exception>
        /// <returns>Message posted</returns>
        public async Task<Message> RespondAsync(object content) =>
            await RespondAsync(content.ToString());
        #endregion

        #region Overrides
        /// <summary>
        /// Creates string equivalent of the message.
        /// </summary>
        /// <returns>Message as string</returns>
        public override string ToString() =>
            $"Message {Id}";
        #endregion
    }
}