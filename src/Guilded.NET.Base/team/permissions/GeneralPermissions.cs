using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Permissions related to servers.
    /// </summary>
    /// <remarks>
    /// <para>Defines permissions related to server management.</para>
    /// </remarks>
    [Flags]
    public enum GeneralPermissions
    {
        /// <summary>
        /// No given permissions.
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows you to update server's settings
        /// </summary>
        UpdateServer = 4,
        /// <summary>
        /// Allows you to directly invite members to the server
        /// </summary>
        InviteMembers = 16,
        /// <summary>
        /// Allows you to kick or ban members from the server
        /// </summary>
        KickBanMembers = 32,
        /// <summary>
        /// Allows you to create new channels and edit or delete existing ones
        /// </summary>
        ManageChannels = 1024,
        /// <summary>
        /// Allows you to create new webhooks and edit or delete existing ones
        /// </summary>
        ManageWebhooks = 2048,
        /// <summary>
        /// Allows you to create new groups and edit or delete existing ones
        /// </summary>
        ManageGroups = 4096,
        /// <summary>
        /// Allows you to use @everyone and @here mentions
        /// </summary>
        MentionEveryoneHere = 8192,
        /// <summary>
        /// Allows you to update the server's roles
        /// </summary>
        ManageRoles = 16384,

        #region Additional
        /// <summary>
        /// All of the permissions combined.
        /// </summary>
        All = UpdateServer | KickBanMembers | ManageChannels | ManageWebhooks | ManageGroups | MentionEveryoneHere | ManageRoles,
        /// <summary>
        /// All of the manage permissions combined.
        /// </summary>
        Manage = UpdateServer | KickBanMembers | ManageChannels | ManageWebhooks | ManageGroups | ManageRoles
        #endregion
    }
}