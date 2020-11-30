using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Represents team, group and user IDs.
    /// </summary>
    public sealed class GId {
        /// <summary>
        /// Length of the ID.
        /// </summary>
        static readonly int idLength = 8;
        /// <summary>
        /// Exception thrown when ID parsing fails.
        /// </summary>
        static readonly InvalidIdException IdException = new InvalidIdException("Could not parse the given short ID string.");
        /// <summary>
        /// All characters which ID can contain
        /// </summary>
        static readonly string AvailableChars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";
        /// <summary>
        /// A base string for IDs
        /// </summary>
        readonly string _;
        /// <summary>
        /// Represents team, group and user IDs.
        /// </summary>
        /// <param name="id">String which represents ID</param>
        GId(string id) =>
            _ = id;
        /// <summary>
        /// Parse string as a short ID.
        /// </summary>
        /// <param name="id">String to be parsed</param>
        /// <exception cref="InvalidIdException">String couldn't be parsed</exception>
        /// <returns>Short ID</returns>
        public static GId Parse(string id) {
            // If string is null, return null
            if(string.IsNullOrWhiteSpace(id)) return null;
            // If length isn't 8
            else if(id.Length != idLength) throw IdException;
            // If each character is correct
            else if(!IsCorrect(id)) throw IdException;
            // Return the id
            return new GId(id);
        }
        /// <summary>
        /// Tries to parse string as a short ID. Doesn't throw an error when fails.
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
                if(!AvailableChars.Contains(c.ToString())) return false;
            // If any of the chars in the string didn't return false, return true
            return true;
        }
        /// <summary>
        /// Converts short ID to string.
        /// </summary>
        /// <returns>Short ID string</returns>
        public override string ToString() => _;
        /// <summary>
        /// Gets ID hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => _.GetHashCode() * 2 - 1000;
        /// <summary>
        /// Checks if given object is equal to this ID.
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            if(obj is GId id) return id._ == _;
            else return false;
        }
        /// <summary>
        /// Checks if given ID is equal to this ID.
        /// </summary>
        /// <param name="obj">Other ID</param>
        /// <returns>Boolean</returns>
        public static bool operator ==(GId id0, GId id1) => id0._ == id1._;
        /// <summary>
        /// Checks if given ID is not equal to this ID.
        /// </summary>
        /// <param name="obj">Other ID</param>
        /// <returns>Boolean</returns>
        public static bool operator !=(GId id0, GId id1) => !(id0 == id1);
    }
}