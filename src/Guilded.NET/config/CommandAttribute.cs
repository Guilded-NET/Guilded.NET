using System;
using System.Collections.Generic;
using System.Linq;

namespace Guilded.NET {
    /// <summary>
    /// Declares method as a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CommandAttribute: Attribute {
        /// <summary>
        /// The name of the command. Used for finding the command.
        /// </summary>
        /// <value>Command name</value>
        public string Name {
            get; protected set;
        }
        /// <summary>
        /// Description of the command. Can be used for help commands.
        /// </summary>
        /// <value></value>
        public string Description {
            get; set;
        }
        /// <summary>
        /// Alternative names for the command.
        /// </summary>
        /// <value>Array of names</value>
        public string[] Alias {
            get; set;
        }
        /// <summary>
        /// How this command should be used.
        /// </summary>
        /// <value>Command usage</value>
        public string Usage {
            get; set;
        }
        /// <summary>
        /// All of the extra info this command should hold. Can be used for more info on help commands. E.g., sub-command list
        /// </summary>
        /// <value>Info</value>
        public Dictionary<string, object> ExtraInfo {
            get; set;
        }
        /// <summary>
        /// If the command name and alias should ignore the case. False by default.
        /// </summary>
        /// <value>Ignore</value>
        public bool IgnoreCase {
            get; set;
        }
        /// <summary>
        /// If all people can use this command. If false, then only owner can use it.
        /// </summary>
        /// <value>Public</value>
        public bool IsPublic {
            get; set;
        }
        /// <summary>
        /// Declares method as a command.
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="alias">Alternative names of the command</param>
        public CommandAttribute(string name, params string[] alias) =>
            (Name, Description, Alias, IgnoreCase, Usage, ExtraInfo) = (name, "A simple command.", alias, false, "", new Dictionary<string, object>());
        /// <summary>
        /// If the given command name is equal to this attribute's command name or alias.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>If it equals to any of the names</returns>
        public bool IsNameEqual(string name) {
            // Get lowcase versions of them, if case should be ignored
            string newer = IgnoreCase ? name.ToLower() : name;
            string lowername = IgnoreCase ? Name.ToLower() : Name;
            IEnumerable<string> newaliases = IgnoreCase ? Alias.Select(x => x.ToLower()) : Alias;
            // If Name is equal to name or name is equal to any alias
            if (newer == lowername || newaliases.Contains(newer)) return true;
            // If not, return false
            else return false;
        }
    }
}