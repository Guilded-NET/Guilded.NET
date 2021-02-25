namespace Guilded.NET.Objects
{
    /// <summary>
    /// Represents team, group and user IDs.
    /// </summary>
    public sealed class GId: BaseId {
        /// <summary>
        /// Length of the ID.
        /// </summary>
        static readonly int idLength = 8;
        /// <summary>
        /// All characters which ID can contain
        /// </summary>
        static readonly string availableChars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";
        /// <summary>
        /// Represents team, group and user IDs.
        /// </summary>
        /// <param name="id">String which represents ID</param>
        GId(string id): base(id) {}
        /// <summary>
        /// Parse string as an ID.
        /// </summary>
        /// <param name="id">String to be parsed</param>
        /// <exception cref="InvalidIdException">String couldn't be parsed</exception>
        /// <returns>ID</returns>
        public static GId Parse(string id) {
            // If string is null, return null
            if(string.IsNullOrWhiteSpace(id)) return null;
            // If length isn't 8
            else if(id.Length != idLength) throw IdParseException;
            // If each character is correct
            else if(!IsCorrect(id)) throw IdParseException;
            // Return the id
            return new GId(id);
        }
        /// <summary>
        /// Tries to parse string as an ID. Doesn't throw an error when fails.
        /// </summary>
        /// <param name="idStr">String to be parsed</param>
        /// <param name="id">Variable to be changed</param>
        /// <returns>Succeeded</returns>
        public static bool TryParse(string idStr, out GId id) {
            // Tries to parse ID. If it fails, it returns false and sets ID as null
            try {
                // Parses ID
                id = Parse(idStr);
                // If it parsed without an exception, it returns true
                return true;
            } catch {
                // If it got an exception, then sets id as null
                id = null;
                // And returns false
                return false;
            }
        }
        /// <summary>
        /// Checks if all characters in the string are in AvailableChars string.
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>Correct</returns>
        static bool IsCorrect(string str) {
            // Get every character in the string
            foreach(char c in str)
                // If AvailableChars doesn't have this character, return false
                if(!availableChars.Contains(c.ToString())) return false;
            // If any of the chars in the string didn't return false, return true
            return true;
        }
    }
}