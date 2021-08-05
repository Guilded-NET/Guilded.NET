using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The identifier for Guilded teams, users, etc.
    /// </summary>
    [TypeConverter(typeof(GIdConverter))]
    [JsonConverter(typeof(IdConverter))]
    public struct GId : IEquatable<GId>
    {
        internal readonly string _;
        private const int idLength = 8;
        private static readonly string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        internal static readonly InvalidIdException IdParseException = new InvalidIdException("Could not parse the given ID string.");
        /// <summary>
        /// The identifier for Guilded teams, users, etc.
        /// </summary>
        /// <param name="id">String which represents Guilded identifier</param>
        /// <exception cref="InvalidIdException">String couldn't be parsed</exception>
        public GId(string id)
        {
            // Makes sure that given string is in correct format
            if (id?.Length != idLength || !IsCorrect(id)) throw IdParseException;
            // Assigns base string
            _ = id;
        }

        #region Overrides
        /// <summary>
        /// Returns string representation of <see cref="GId"/> instance.
        /// </summary>
        /// <returns><see cref="GId"/> as string</returns>
        public override string ToString() =>
            _;
        /// <summary>
        /// Gets identifier's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => _.GetHashCode() * 2 - 1000;
        /// <summary>
        /// Checks if given ID is equal to this ID.
        /// </summary>
        /// <param name="id">ID to compare</param>
        /// <returns>Are equal</returns>
        public bool Equals(GId id) =>
            id == this;
        /// <summary>
        /// Checks if given object is equal to this ID.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is GId id && id == this;
        #endregion

        #region Operators
        /// <summary>
        /// Checks if given <see cref="GId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Are equal</returns>
        public static bool operator ==(GId id0, GId id1) => id0._ == id1._;
        /// <summary>
        /// Checks if given <see cref="GId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Aren't equal</returns>
        public static bool operator !=(GId id0, GId id1) => !(id0 == id1);
        #endregion

        #region Static methods
        /// <summary>
        /// Checks if all characters in the string are in AvailableChars string.
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>Correct</returns>
        private static bool IsCorrect(string str)
        {
            // If string is empty, return false
            if (string.IsNullOrWhiteSpace(str)) return false;
            // Get every character in the string
            foreach (char c in str)
                // If AvailableChars doesn't have this character, return false
                if (!availableChars.Contains(c.ToString())) return false;
            // If any of the chars in the string didn't return false, return true
            return true;
        }
        #endregion
    }
}