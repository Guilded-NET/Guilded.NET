using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// Group membership type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
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