using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// A type of a team(clan, community, guild, etc.).
    /// </summary>
    public enum TeamType {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "team")]
        Team,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "organization")]
        Organization,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "community")]
        Community,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "clan")]
        Clan,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "guild")]
        Guild,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "friends")]
        Friends,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "streaming")]
        Streaming,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "other")]
        Other
    }
}