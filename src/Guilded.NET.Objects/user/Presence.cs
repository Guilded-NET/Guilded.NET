namespace Guilded.NET.Objects {
    /// <summary>
    /// User status presence.
    /// </summary>
    public enum Presence {
        /// <summary>
        /// User is online and is currently available to talk.
        /// </summary>
        Online = 1,
        /// <summary>
        /// User is online, but temporarely away.
        /// </summary>
        Idle = 2,
        /// <summary>
        /// User is online, but can't talk.
        /// </summary>
        DoNotDisturb = 3,
        /// <summary>
        /// User appears as offline.
        /// </summary>
        Invisible = 4
    }
}