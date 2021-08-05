using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    using Teams;
    using Users;
    /// <summary>
    /// Represents information about the invite in metadata.
    /// </summary>
    public class MetadataInvite: ClientObject
    {
        /// <summary>
        /// Who created the invite.
        /// </summary>
        /// <value>Invited by user ID</value>
        [JsonProperty( Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// Information about the user who created the invite.
        /// </summary>
        /// <value>Invited by user info</value>
        [JsonProperty(Required = Required.Always)]
        public BaseUser CreatedByInfo
        {
            get; set;
        }
        /// <summary>
        /// ID of the team invite.
        /// </summary>
        /// <value>Invite ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId InviteId
        {
            get; set;
        }
        /// <summary>
        /// Team invited to.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty(Required = Required.Always)]
        public Team Team
        {
            get; set;
        }
    }
}