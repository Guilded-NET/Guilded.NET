namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// What kind of mention it is.
    /// </summary>
    public enum MentionType {
        /// <summary>
        /// Mention of a specific person or a member.
        /// </summary>
        Person,
        /// <summary>
        /// Mentions all people currently in the channel and online.
        /// </summary>
        Here,
        /// <summary>
        /// Mentions every user.
        /// </summary>
        Everyone,
        /// <summary>
        /// Mentions everyone in a specific role.
        /// </summary>
        Role
    }
}