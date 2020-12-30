namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// An interface for types related to mentions
    /// </summary>
    public interface IMention {
        /// <summary>
        /// Mention matcher. Mention as a string.
        /// </summary>
        /// <value>Matcher</value>
        string Matcher {
            get; set;
        }
        /// <summary>
        /// A name of a channel, role or user.
        /// </summary>
        /// <value>Name</value>
        string Name {
            get; set;
        }
    }
}