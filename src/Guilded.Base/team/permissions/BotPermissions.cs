using System;

namespace Guilded.Base.Permissions
{
    /// <summary>
    /// Permissions related to bots.
    /// </summary>
    /// <remarks>
    /// <para>Defines team permissions for flowbots related things.</para>
    /// </remarks>
    [Flags]
    public enum BotPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to create and edit bots for automated workflows.
        /// NOTE: For now, bots do not enforce permissions. Anyone with this permission
        /// can create bots to work around their role's existing permissions.
        /// </summary>
        ManageBots = 1,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = ManageBots
        #endregion
    }
}