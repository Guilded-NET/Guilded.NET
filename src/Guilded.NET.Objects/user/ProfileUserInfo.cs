namespace Guilded.NET.Objects.Users
{
    /// <summary>
    /// An info about the user who posted a post or a reply in other user's/their profile.
    /// </summary>
    public class ProfileUserInfo : BaseUser
    {
        /// <summary>
        /// Gets user's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() + 10;
    }
}