using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    using Teams;
    /// <summary>
    /// An event that occurs once a member gets updated.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once member receives any update, apart from role update(see <see cref="RolesUpdatedEvent"/>).</para>
    /// <para>API calls this event <c>TeamMemberUpdated</c>.</para>
    /// </remarks>
    /// <seealso cref="RolesUpdatedEvent"/>
    /// <seealso cref="XpAddedEvent"/>
    /// <seealso cref="WelcomeEvent"/>
    public class MemberUpdatedEvent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The info about updated member.
        /// </summary>
        /// <remarks>
        /// <para>Defines an update that the member has received.</para>
        /// <para>As of now, this only means <see cref="Member.Nickname"/> has been updated.</para>
        /// </remarks>
        /// <value>Member info</value>
        [JsonProperty(Required = Required.Always)]
        public Member UserInfo
        {
            get; set;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The identifier of the member.
        /// </summary>
        /// <remarks>
        /// <para>Gets the identifier of the updated member.</para>
        /// <para>Fetched from <see cref="UserInfo"/> property.</para>
        /// </remarks>
        [JsonIgnore]
        public GId MemberId => UserInfo.Id;
        #endregion
    }
}