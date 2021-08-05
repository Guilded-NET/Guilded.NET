using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Permissions;
    /// <summary>
    /// Allowed and disallowed permissions.
    /// </summary>
    public class PermissionBase : ClientObject
    {
        /// <summary>
        /// Allowed and disallowed permissions.
        /// </summary>
        public PermissionBase() =>
            (AllowPermissions, DenyPermissions) = (new PermissionList(), new PermissionList());
        /// <summary>
        /// All of the removed permissions.
        /// </summary>
        /// <value>Permissions</value>
        public PermissionList DenyPermissions
        {
            get; set;
        }
        /// <summary>
        /// All of the added permissions.
        /// </summary>
        /// <value>Permissions</value>
        public PermissionList AllowPermissions
        {
            get; set;
        }
    }
}