using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Group membership type.
    /// </summary>
    public enum MembershipType {
        /// <summary>
        /// User has joined a group.
        /// </summary>
        [EnumMember(Value = "joined")]
        Joined,
        /// <summary>
        /// User has left a group.
        /// </summary>
        [EnumMember(Value = "left")]
        Left,
        /// <summary>
        /// User is following this group.
        /// </summary>
        [EnumMember(Value = "following")]
        Following
    }
}