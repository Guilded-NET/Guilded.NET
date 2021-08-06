namespace Guilded.NET.Base.Events
{
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