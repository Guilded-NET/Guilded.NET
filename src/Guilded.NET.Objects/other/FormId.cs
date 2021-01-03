using System;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Objects {
    /// <summary>
    /// ID used in form fields, options and other form related things.
    /// </summary>
    public sealed class FormId: BaseId {
        /// <summary>
        /// Max length of one part.
        /// </summary>
        static readonly int partLength = 7;
        /// <summary>
        /// A random for generating random form IDs.
        /// </summary>
        /// <returns>Random</returns>
        static readonly Random random = new Random();
        /// <summary>
        /// Creates a random form ID.
        /// </summary>
        /// <value>Form ID</value>
        public static FormId Random {
            get => Parse($"r-{random.Next(1000000, 9999999)}-{random.Next(1000000, 9999999)}");
        }
        /// <summary>
        /// ID used in form fields, options and other form related things.
        /// </summary>
        /// <param name="id">String which represents ID</param>
        FormId(string id): base(id) {}
        /// <summary>
        /// Parse string as a form field/option ID.
        /// </summary>
        /// <param name="id">String to be parsed</param>
        /// <exception cref="InvalidIdException">String couldn't be parsed</exception>
        /// <returns>Form field/option ID</returns>
        public static FormId Parse(string id) {
            // If string is null, return null
            if(string.IsNullOrWhiteSpace(id)) return null;
            // If it does not starts with a specific character('r')
            else if(!id.StartsWith('r')) throw IdParseException;
            // Splits the string by divider('-') and skips first item(because it's starting character)
            List<string> split = id.Split('-').Skip(1).ToList();
            // If there aren't 2 items in that array, then it's wrong
            if(split.Count != 2) throw IdParseException;
            // If length in both strings is wrong
            else if(split.FirstOrDefault(x => x.Length != partLength) != null) throw IdParseException;
            // If everything is correct, then return new instance of it
            return new FormId(id);
        }
        /// <summary>
        /// Tries to parse string as an ID. Doesn't throw an error when fails.
        /// </summary>
        /// <param name="idStr">String to be parsed</param>
        /// <param name="id">Variable to be changed</param>
        /// <returns>Succeeded</returns>
        public static bool TryParse(string idStr, out FormId id) {
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
    }
}