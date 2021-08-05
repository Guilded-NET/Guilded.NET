using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The identifier for forms and media uploads.
    /// </summary>
    [TypeConverter(typeof(GIdConverter))]
    [JsonConverter(typeof(IdConverter))]
    public struct FormId : IEquatable<FormId>
    {
        internal readonly string _;
        private static readonly int partLength = 7;
        private static readonly Random random = new Random();
        /// <summary>
        /// Creates a random form ID.
        /// </summary>
        /// <value>New form ID</value>
        public static FormId Random => new FormId($"r-{random.Next(1000000, 9999999)}-{random.Next(1000000, 9999999)}");
        /// <summary>
        /// Represents form and media upload identifierss.
        /// </summary>
        /// <param name="id">String which represents form identifier</param>
        /// <exception cref="InvalidIdException">String couldn't be parsed</exception>
        public FormId(string id)
        {
            // Makes sure that given string is in correct format
            if (!IsCorrect(id)) throw GId.IdParseException;
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
        /// Gets identifier's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => _.GetHashCode() * 2 - 1000;
        /// <summary>
        /// Checks if given ID is equal to this ID.
        /// </summary>
        /// <param name="id">ID to compare</param>
        /// <returns>Are equal</returns>
        public bool Equals(FormId id) =>
            id == this;
        /// <summary>
        /// Checks if given object is equal to this ID.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is FormId id && id == this;
        #endregion

        #region Operators
        /// <summary>
        /// Checks if given <see cref="FormId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Are equal</returns>
        public static bool operator ==(FormId id0, FormId id1) => id0._ == id1._;
        /// <summary>
        /// Checks if given <see cref="FormId"/>s are the same.
        /// </summary>
        /// <param name="id0">First ID to be compared</param>
        /// <param name="id1">Second ID to be compared</param>
        /// <returns>Aren't equal</returns>
        public static bool operator !=(FormId id0, FormId id1) => !(id0 == id1);
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
            // If it does not starts with a specific character('r'), it's not correct
            else if (!str.StartsWith('r')) return false;
            // Splits the string by divider('-') and skips first item(because it's starting character)
            List<string> split = str.Split('-').Skip(1).ToList();
            // If everything is correct, then return new instance of it
            if (split.Count == 2 && split.FirstOrDefault(x => x.Length != partLength) is null) return true;
            // If length in both strings is wrong
            else return false;
        }
        #endregion
    }
}