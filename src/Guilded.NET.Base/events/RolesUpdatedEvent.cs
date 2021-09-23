using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Teams;
    /// <summary>
    /// An event that occurs once someone receives or loses a role.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once role holder either loses a role or receives it.</para>
    /// <para>This event does not give a list of lost/received events or give a previous role
    /// list, so previous role list must be cached, if necessary.</para>
    /// <para>In API, this event is called <c>teamRolesUpdated</c>.</para>
    /// </remarks>
    /// <seealso cref="MemberUpdatedEvent"/>
    /// <seealso cref="XpAddedEvent"/>
    /// <seealso cref="WelcomeEvent"/>
    public class RolesUpdatedEvent : BaseObject
    {
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
        public IList<MemberRoles> MemberRoleIds
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