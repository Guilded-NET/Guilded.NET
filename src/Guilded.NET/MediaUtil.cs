using System;
using System.Collections.Generic;
using System.Linq;

namespace Guilded.NET.Util
{
    /// <summary>
    /// Utilities for things related to media.
    /// </summary>
    public static class MediaUtil
    {
        private const string serverChars = "abcdefghijklmnopqrstuvwxyz0123456789";
        /// <summary>
        /// All default avatar URLs.
        /// </summary>
        /// <returns>List of URLs</returns>
        public static readonly List<Uri> DefaultAvatars = new List<Uri>();
        /// <summary>
        /// Utilities for things related to media.
        /// </summary>
        static MediaUtil()
        {
            // Adds all avatars(1st to 5th)
            for (int i = 1; i < 6; i++)
                DefaultAvatars.Add(new Uri($"https://img.guildedcdn.com/asset/DefaultUserAvatars/profile_{i}.png"));
        }
        /// <summary>
        /// Generate a URL for server icon from given character.
        /// </summary>
        /// <param name="startingLetter">Starting letter of the server</param>
        /// <returns>Server's icon</returns>
        public static Uri FetchServerIcon(char startingLetter)
        {
            // Turn it to lowercase character
            string lower = startingLetter != default ? startingLetter.ToString().ToLower() : "0";
            // If given character is not in the range of allowed characters
            if (!serverChars.Contains(lower)) throw new ArgumentException($"{nameof(startingLetter)} can only be a letter or a digit.");
            // Return the URL for it
            return new Uri($"https://img.guildedcdn.com/asset/TeamPage/Avatars/default-team-avatar-{lower}@2x.png");
        }
        /// <summary>
        /// Gets the first letter allowed for server icons and returns the URL of the server icon.
        /// </summary>
        /// <param name="name">The name of the server</param>
        /// <returns>First letter/digit or default(0)</returns>
        public static Uri FetchServerIcon(string name) =>
            FetchServerIcon(name.ToLower().FirstOrDefault(c => serverChars.Contains(c)));
        /// <summary>
        /// Gets the URL to a global emote of given name.
        /// </summary>
        /// <param name="name">The name of the emote</param>
        /// <param name="isWebp">Whether to get Webp or PNG</param>
        /// <returns>Webp or PNG URL</returns>
        public static Uri FetchGlobalEmote(string name, bool isWebp = true) =>
            new Uri($"https://img.guildedcdn.com/asset/Emojis/Custom/{name.ToLower()}.{(isWebp ? "webp" : "png")}");
    }
}