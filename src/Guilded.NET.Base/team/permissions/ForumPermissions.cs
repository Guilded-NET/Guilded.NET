using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to forums.
    /// </summary>
    /// <remarks>
    /// <para>Defines permissions related to forums.</para>
    /// </remarks>
    [Flags]
    public enum ForumPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to create forum topics
        /// </summary>
        CreateTopics = 1,
        /// <summary>
        /// Allows you to read forums
        /// </summary>
        ReadForums = 2,
        /// <summary>
        /// Allows you to remove topics and replies by others, or move them to other channels
        /// </summary>
        ManageTopics = 8,
        /// <summary>
        /// Allows you to sticky a topic
        /// </summary>
        StickyTopics = 16,
        /// <summary>
        /// Allows you to lock a topic
        /// </summary>
        LockTopics = 32,
        /// <summary>
        /// Allows you to create forum topic replies
        /// </summary>
        CreateTopicReplies = 64,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = CreateTopics | ReadForums | ManageTopics | StickyTopics | LockTopics | CreateTopicReplies,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = ManageTopics | StickyTopics | LockTopics,
        /// <summary>
        /// A simple permission combination allowing writing permissions and reading permissions.
        /// </summary>
        Basic = CreateTopics | ReadForums | CreateTopicReplies
        #endregion
    }
}