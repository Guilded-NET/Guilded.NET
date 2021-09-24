using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to streaming.
    /// </summary>
    /// <remarks>
    /// <para>Defines permissions related to streaming channels.</para>
    /// </remarks>
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
        SendMessages = 32,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = AddStream | ViewStreams | JoinVoice | SendMessages,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = 0,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = ViewStreams | JoinVoice | SendMessages
        #endregion
    }
}