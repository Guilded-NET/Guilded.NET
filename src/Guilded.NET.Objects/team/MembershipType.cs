namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Group membership type.
    /// </summary>
    public enum MembershipType {
        /// <summary>
        /// User has joined a group.
        /// </summary>
        Joined,
        /// <summary>
        /// User has left a group.
        /// </summary>
        Left,
        /// <summary>
        /// User is following this group.
        /// </summary>
        Following
    }
}