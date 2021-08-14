using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The identifier for Guilded teams, users, etc.
    /// </summary>
    /// <seealso cref="Guid"/>
    /// <seealso cref="FormId"/>
    [TypeConverter(typeof(GIdConverter))]
    [JsonConverter(typeof(IdConverter))]
    public struct GId : IEquatable<GId>
    {
        internal readonly string _;
        private const int idLength = 8;
        private const string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        internal static readonly FormatException FormatError = new FormatException("The given ID string is in incorrect format.");
        /// <summary>
        /// The identifier for Guilded teams, users, etc.
        /// </summary>
        /// <param name="id">The raw string in the format of Guilded ID</param>
        /// <exception cref="FormatException">When the given ID string is in incorrect format</exception>
        public GId(string id)
        {
            // Makes sure that given string is in correct format
            if (id?.Length != idLength || !Check(id)) throw FormatError;
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
        /// Checks if given string is in correct format.
        /// </summary>
        /// <param name="str">The raw string to check</param>
        /// <returns>Correct formatting</returns>
        public static bool Check(string str)
        {
            // If string is empty or isn't in 8 characters, return false
            if (string.IsNullOrWhiteSpace(str) || str.Length != 8) return false;
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