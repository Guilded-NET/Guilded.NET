using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Permissions related to customization.
/// </summary>
/// <remarks>
/// <para>Defines team permissions related to nickname and emoji customizations.</para>
/// </remarks>
[Flags]
public enum CustomPermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,
    /// <summary>
    /// Allows the creation and management of server emoji
    /// </summary>
    ManageEmoji = 1,
    /// <summary>
    /// Members with this permission can change their own nickname.
    /// </summary>
    ChangeNickname = 16,
    /// <summary>
    /// Members with this permission can change the nickname of others.
    /// </summary>
    ManageNicknames = 32,

    #region Additional
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = ManageEmoji | ChangeNickname | ManageNicknames
    #endregion
}