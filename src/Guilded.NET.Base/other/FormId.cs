using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The identifier for forms and media uploads.
    /// </summary>
    /// <seealso cref="Guid"/>
    /// <seealso cref="GId"/>
    [TypeConverter(typeof(FormIdConverter))]
    [JsonConverter(typeof(IdConverter))]
    public struct FormId : IEquatable<FormId>
    {
        internal readonly string _;
        private const int partLength = 7;
        private static readonly Random random = new Random();
        /// <summary>
        /// Creates a random value of <see cref="FormId"/>.
        /// </summary>
        /// <value>New form ID</value>
        public static FormId Random => new FormId($"r-{random.Next(1000000, 9999999)}-{random.Next(1000000, 9999999)}");
        /// <summary>
        /// The identifier for forms and media uploads.
        /// </summary>
        /// <param name="id">The raw string in the format of Form/Media ID</param>
        /// <exception cref="FormatException">When the given ID string is in incorrect format</exception>
        public FormId(string id)
        {
            // Makes sure that given string is in correct format
            if (!Check(id)) throw GId.FormatError;
            // Assigns base string
            _ = id;
        }

        #region Overrides
        /// <summary>
        /// Returns string representation of <see cref="FormId"/> instance.
        /// </summary>
        /// <returns><see cref="FormId"/> as string</returns>
        public override string ToString() =>
            _;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(_, 1);
        /// <summary>
        /// Returns whether this and <paramref name="other"/> are equal to each other.
        /// </summary>
        /// <param name="other">Another identifier to compare</param>
        /// <returns>Are equal</returns>
        public bool Equals(FormId other) =>
            other == this;
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is FormId id && Equals(id);
        #endregion

        #region Operators
        /// <summary>
        /// Checks if given <see cref="FormId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Are equal</returns>
        public static bool operator ==(FormId id0, FormId id1) =>
            id0._ == id1._;
        /// <summary>
        /// Checks if given <see cref="FormId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Aren't equal</returns>
        public static bool operator !=(FormId id0, FormId id1) =>
            !(id0 == id1);
        #endregion

        #region Static methods
        /// <summary>
        /// Checks if given string is in correct format.
        /// </summary>
        /// <param name="str">The raw string to check</param>
        /// <returns>Correct formatting</returns>
        public static bool Check(string str)
        {
            // If string is empty, return false
            if (string.IsNullOrWhiteSpace(str)) return false;
            // If it does not starts with a specific character('r'), it's not correct
            else if (!str.StartsWith('r')) return false;
            // Splits the string by divider('-') and skips first item(because it's starting character)
            List<string> split = str.Split('-').Skip(1).ToList();
            // If everything is correct, then return new instance of it
            if (split.Count == 2 && !split.Any(part => part.Length != partLength)) return true;
            // If length in both strings is wrong
            else return false;
        }
        #endregion
    }
}