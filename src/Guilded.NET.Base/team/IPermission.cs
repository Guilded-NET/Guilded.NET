// using System;

// namespace Guilded.NET.Base.Teams
// {
//     using Permissions;
//     /// <summary>
//     /// Interface for user and role permissions.
//     /// </summary>
//     public interface IPermission
//     {
//         /// <summary>
//         /// Date when this permission was created.
//         /// </summary>
//         /// <value>Date</value>
//         DateTime CreatedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Date when this permission was last updated.
//         /// </summary>
//         /// <value>Nullable date</value>
//         DateTime? UpdatedAt
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Denied permissions.
//         /// </summary>
//         /// <value>Permissions</value>
//         PermissionList DenyPermissions
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Allowed permissions.
//         /// </summary>
//         /// <value>Permissions</value>
//         PermissionList AllowPermissions
//         {
//             get; set;
//         }
//     }
// }