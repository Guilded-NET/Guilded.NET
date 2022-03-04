using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to chat.
    /// </summary>
    /// <remarks>
    /// <para>Defines channel permissions for chat &amp; text related things.</para>
    /// </remarks>
    [Flags]
    public enum ChatPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
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
        ManageThreads = 64,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = SendMessages | ReadMessages | ManageMessages | CreateThreads | SendThreadMessages | ManageThreads,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="ManageMessages"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="CreateThreads"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="ManageThreads"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Manage = ManageMessages | CreateThreads | ManageThreads,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        /// <remarks>
        /// <para>Sets these permissions:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description><see cref="SendMessages"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="ReadMessages"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="SendThreadMessages"/></description>
        ///     </item>
        /// </list>
        /// </remarks>
        Basic = SendMessages | ReadMessages | SendThreadMessages
        #endregion
    }
}