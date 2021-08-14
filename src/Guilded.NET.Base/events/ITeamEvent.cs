using System;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// A base for all events that may occur in teams.
    /// </summary>
    public interface ITeamEvent
    {
        /// <summary>
        /// The identifier of the parent channel.
        /// </summary>
        /// <value>Channel ID</value>
        Guid ChannelId { get; }
    }
}