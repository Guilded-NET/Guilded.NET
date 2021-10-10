using System.Linq;
using System.Collections.Generic;
using Guilded.NET.Base.Teams;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone receives or loses a role.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>teamRolesUpdated</c> and opcode <c>0</c> that occurs once role holder either loses a role or receives it. This event does not give a list of lost/received events or give a previous role list, so previous role list must be cached, if necessary.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdatedEvent"/>
    /// <seealso cref="XpAddedEvent"/>
    /// <seealso cref="WelcomeEvent"/>
    /// <seealso cref="Member"/>
    public class RolesUpdatedEvent : BaseObject
    {
        /// <summary>
        /// Defines a role list holder and their role list.
        /// </summary>
        /// <remarks>
        /// <para>Defines a receiving a role holder <see cref="UserId"/> and their current role list <see cref="RoleIds"/>. Roles that were added or removed, or previous role list are not provided. If necessary, previous role list should be cached before-hand.</para>
        /// </remarks>
        /// <seealso cref="MemberUpdatedEvent"/>
        /// <seealso cref="Member"/>
        public class RolesUpdated
        {
            /// <summary>
            /// The identifier of the role holder.
            /// </summary>
            /// <remarks>
            /// <para>The identifier of the user that has received all roles in <see cref="RoleIds"/> list.</para>
            /// </remarks>
            /// <value>User ID</value>
            [JsonProperty(Required = Required.Always)]
            public GId UserId
            {
                get; set;
            }
            /// <summary>
            /// The list of roles <see cref="UserId"/> holds.
            /// </summary>
            /// <remarks>
            /// <para>The list of roles that <see cref="UserId"/> is currently holding.</para>
            /// <para>Received or removed roles are not provided, so caching of previou role list is necessary if previous role list is needed.</para>
            /// </remarks>
            /// <value>List of role IDs</value>
            [JsonProperty(Required = Required.Always)]
            public IList<uint> RoleIds
            {
                get; set;
            }
        }

        #region JSON properties
        /// <summary>
        /// The list of receiving/losing member and current roles.
        /// </summary>
        /// <remarks>
        /// <para>The list of user and their current role list in IDs.</para>
        /// <para>This returns users that lost roles, received roles or both.</para>
        /// <para>If only updated users are needed, use <see cref="UpdatedUsers"/> property.</para>
        /// </remarks>
        /// <value>Member and role definition</value>
        [JsonProperty(Required = Required.Always)]
        public IList<RolesUpdated> MemberRoleIds
        {
            get; set;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The array of updated users.
        /// </summary>
        /// <remarks>
        /// <para>Returns the array of members that had their role list updated either by losing or receiving roles.</para>
        /// <para>This property goes through <see cref="MemberRoleIds"/> and selects user IDs.</para>
        /// </remarks>
        /// <returns>Array of user IDs</returns>
        [JsonIgnore]
        public GId[] UpdatedUsers => MemberRoleIds.Select(x => x.UserId).ToArray();
        #endregion
    }
}