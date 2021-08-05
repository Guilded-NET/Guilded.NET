namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// If the DM channel is a group or default.
    /// </summary>
    public enum DMType
    {
        /// <summary>
        /// A normal DM channel between 2 people.
        /// </summary>
        Default,
        /// <summary>
        /// A DM channel between 3+ people.
        /// </summary>
        Group
    }
}