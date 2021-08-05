using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions for managing bots and flowbots.
    /// </summary>
    [Flags]
    public enum BotPermissions
    {
        /// <summary>
        /// Allows you to create and edit bots for automated workflows. 
        /// NOTE: For now, bots do not enforce permissions. Anyone with this permission 
        /// can create bots to work around their role's existing permissions.
        /// </summary>
        ManageBots = 1
    }
}