using System;

namespace Guilded.NET.Objects.Permissions
{
    /// <summary>
    /// Interface for user and role permissions.
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// Date when this permission was created.
        /// </summary>
        /// <value>Date</value>
        DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// Date when this permission was last updated.
        /// </summary>
        /// <value>Nullable date</value>
        DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// All of the denied permissions.
        /// </summary>
        /// <value>Disallowed permissions</value>
        PermissionList DenyPermissions
        {
            get; set;
        }
        /// <summary>
        /// All of the allowed permissions.
        /// </summary>
        /// <value>Allowed permissions</value>
        PermissionList AllowPermissions
        {
            get; set;
        }
    }
}