using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions for streaming/stream channel.
    /// </summary>
    [Flags]
    public enum StreamPermissions
    {
        /// <summary>
        /// Allows you to add a stream and also talk in the stream channel
        /// </summary>
        AddStream = 1,
        /// <summary>
        /// Allows you to view streams
        /// </summary>
        ViewStreams = 2,
        /// <summary>
        /// Allows you to talk in stream channel
        /// </summary>
        JoinVoice = 16,
        /// <summary>
        /// Allows you to send message in stream channel
        /// </summary>
        SendMessages = 32
    }
}