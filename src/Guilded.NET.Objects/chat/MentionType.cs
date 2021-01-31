using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// What kind of mention it is.
    /// </summary>
    public enum MentionType {
        /// <summary>
        /// Mention of a specific person or a member.
        /// </summary>
        [EnumMember(Value = "person")]
        Person,
        /// <summary>
        /// Mentions all people currently in the channel and online.
        /// </summary>
        [EnumMember(Value = "here")]
        Here,
        /// <summary>
        /// Mentions every user.
        /// </summary>
        [EnumMember(Value = "everyone")]
        Everyone,
        /// <summary>
        /// Mentions everyone in a specific role.
        /// </summary>
        [EnumMember(Value = "role")]
        Role
    }
}