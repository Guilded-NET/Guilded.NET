using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Permissions;
    /// <summary>
    /// The base type for <see cref="Message"/> and <see cref="MessageDeleted"/>.
    /// </summary>
    public abstract class BaseMessage : ClientObject
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
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="content">The contents of the message in rich text markup</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(MessageContent content) =>
            await ParentClient.CreateMessageAsync(ChannelId, content).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(string content) =>
            await ParentClient.CreateMessageAsync(ChannelId, content).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(string format, object arg0) =>
            await RespondAsync(string.Format(format, arg0)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <param name="arg1">The second argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(string format, object arg0, object arg1) =>
            await RespondAsync(string.Format(format, arg0, arg1)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <param name="arg1">The second argument of the format string</param>
        /// <param name="arg2">The third argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(string format, object arg0, object arg1, object arg2) =>
            await RespondAsync(string.Format(format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(string format, params object[] args) =>
            await RespondAsync(string.Format(format, args)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0) =>
            await RespondAsync(string.Format(provider, format, arg0)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <param name="arg1">The second argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0, object arg1) =>
            await RespondAsync(string.Format(provider, format, arg0, arg1)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="arg0">The first argument of the format string</param>
        /// <param name="arg1">The second argument of the format string</param>
        /// <param name="arg2">The third argument of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, object arg0, object arg1, object arg2) =>
            await RespondAsync(string.Format(provider, format, arg0, arg1, arg2)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="format"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="format"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(IFormatProvider provider, string format, params object[] args) =>
            await RespondAsync(string.Format(provider, format, args)).ConfigureAwait(false);
        /// <summary>
        /// Creates a message in a chat.
        /// </summary>
        /// <param name="content">The contents of the message in Markdown plain text</param>
        /// <exception cref="GuildedException"/>
        /// <exception cref="GuildedPermissionException"/>
        /// <exception cref="GuildedResourceException">When the channel of identifier <see cref="ChannelId"/> no longer exists</exception>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> only consists of whitespace or is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When the <paramref name="content"/> is above the message limit of 4000 characters</exception>
        /// <permission cref="ChatPermissions.ReadMessages">Required for reading all channel and thread messages</permission>
        /// <permission cref="ChatPermissions.SendMessages">Required for sending a message in a channel</permission>
        /// <permission cref="ChatPermissions.SendThreadMessages">Required for sending a message in a thread</permission>
        /// <returns>Message created</returns>
        public async Task<Message> RespondAsync(object content) =>
            await RespondAsync(content.ToString()).ConfigureAwait(false);
        #endregion

        #region Overrides
        /// <summary>
        /// Creates string equivalent of the message.
        /// </summary>
        /// <returns>Message as string</returns>
        public override string ToString() =>
            $"Message {Id}";
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is BaseMessage message && message.Id == Id;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(ChannelId, Id);
        #endregion
    }
}