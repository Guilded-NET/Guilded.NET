using System.Collections.Generic;
using System;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Interface for team channels and categories.
    /// </summary>
    public interface ITeamChannel: IChannel {
        /// <summary>
        /// Permissions of the roles in this channel.
        /// </summary>
        /// <value>Role Permissions</value>
        IDictionary<string, ChannelPermission> RolePermissions {
            get; set;
        }
        /// <summary>
        /// Permissions of the users in this channel.
        /// </summary>
        /// <value>User Permissions</value>
        IList<UserPermission> UserPermissions {
            get; set;
        }
        /// <summary>
        /// ID of team this channel is in.
        /// </summary>
        /// <value>Team ID</value>
        GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        uint? ChannelCategoryId {
            get; set;
        }
        /// <summary>
        /// ID of the group this channel is in.
        /// </summary>
        /// <value>Group ID</value>
        GId GroupId {
            get; set;
        }
    }
}