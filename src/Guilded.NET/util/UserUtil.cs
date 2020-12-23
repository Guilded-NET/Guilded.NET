using System.Threading.Tasks;

namespace Guilded.NET.Util {
    using Guilded.NET.Objects.Chat;
    using Objects;
    using Objects.Teams;
    /// <summary>
    /// Utilities for user related things.
    /// </summary>
    public static class UserUtil {
        /// <summary>
        /// Creates a new DM channel.
        /// </summary>
        /// <param name="user">User to create DMs with</param>
        /// <returns>Channel</returns>
        public static async Task<DMChannel> CreateDMAsync(this User user) =>
            await user.ParentClient.CreateDMChannelAsync(user.Id);
        /// <summary>
        /// Creates a new DM channel. Sync version of <see cref="CreateDMAsync"/>.
        /// </summary>
        /// <param name="user">User to create DMs with</param>
        /// <returns>Channel</returns>
        public static DMChannel CreateDM(this User user) =>
            user.ParentClient.CreateDMChannel(user.Id);
        /// <summary>
        /// Creates a mention based on a user.
        /// </summary>
        /// <param name="user">User to mention</param>
        /// <returns>User mention</returns>
        public static Mention CreateMention(this User user) =>
            Mention.Generate(user);
    }
}