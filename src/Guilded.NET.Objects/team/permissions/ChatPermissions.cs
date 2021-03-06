using System;

namespace Guilded.NET.Objects.Permissions
{
    /// <summary>
    /// Chat/text channel related permissions.
    /// </summary>
    [Flags]
    public enum ChatPermissions
    {
        /// <summary>
        /// Allows you to send chat messages
        /// </summary>
        SendMessages = 1,
        /// <summary>
        /// Allows you to read chat messages
        /// </summary>
        ReadMessages = 2,
        /// <summary>
        /// Allows you to delete chat messages by other members or pin any message
        /// </summary>
        ManageMessages = 4,
        /// <summary>
        /// Allows you to create threads in the channel
        /// </summary>
        CreateThreads = 16,
        /// <summary>
        /// Allows you to reply to threads in the channel
        /// </summary>
        SendThreadMessages = 32,
        /// <summary>
        /// Allows you to archive and restore threads
        /// </summary>
        ManageThreads = 64
    }
}