using System;
using System.ComponentModel;
using System.Linq;
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
        private const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        internal static readonly FormatException FormatError = new FormatException("The given ID string is in incorrect format.");
        /// <summary>
        /// The identifier for Guilded teams, users, etc.
        /// </summary>
        /// <param name="id">The raw string in the format of Guilded ID</param>
        /// <exception cref="FormatException">When the given ID string is in incorrect format</exception>
        public GId(string id)
        {
            // Make sure it's in correct format
            if (id?.Length != idLength || !Check(id))
                throw FormatError;

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
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(_, 2);
        /// <summary>
        /// Returns whether this and <paramref name="other"/> are equal to each other.
        /// </summary>
        /// <param name="other">Another identifier to compare</param>
        /// <returns>Are equal</returns>
        public bool Equals(GId other) =>
            other._ == _;
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is GId id && Equals(id);
        #endregion

        #region Operators
        /// <summary>
        /// Checks if given <see cref="GId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Are equal</returns>
        public static bool operator ==(GId id0, GId id1) =>
            id0._ == id1._;
        /// <summary>
        /// Checks if given <see cref="GId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Aren't equal</returns>
        public static bool operator !=(GId id0, GId id1) =>
            !(id0 == id1);
        #endregion

        #region Static methods
        /// <summary>
        /// Checks if given string is in correct format.
        /// </summary>
        /// <param name="str">The raw string to check</param>
        /// <returns>Correct formatting</returns>
        public static bool Check(string str) =>
            str?.Length == 8 && str.All(ch => allowedChars.Contains(ch));
        #endregion
    }
}