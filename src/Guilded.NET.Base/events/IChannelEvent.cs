using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Users;
    using Chat;
    using System;

    /// <summary>
    /// A base for events that may occur in a channel.
    /// </summary>
    public interface IChannelEvent
    {
        /// <summary>
        /// ID of the channel where the event occurred.
        /// </summary>
        /// <value>Channel ID</value>
        Guid ChannelId { get; }
    }
}