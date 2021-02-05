namespace Guilded.NET.API {
    /// <summary>
    /// To what referer should be set as in the team.
    /// </summary>
    public enum TeamRefer {
        /// <summary>
        /// It should be set as a team's overview.
        /// </summary>
        Overview,
        /// <summary>
        /// It should be set as a team's audit log.
        /// </summary>
        Audit,
        /// <summary>
        /// It should be set as a team's application section.
        /// </summary>
        Recruitment,
        /// <summary>
        /// It should be set as a team's member section.
        /// </summary>
        Members
    }
}