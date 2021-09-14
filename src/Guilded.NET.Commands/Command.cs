using System;
using System.Collections.Generic;

namespace Guilded.NET.Commands
{
    /// <summary>
    /// Defines a method as a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The name of this command.
        /// </summary>
        /// <value>Name?</value>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The list of name aliases for this command.
        /// </summary>
        /// <value>List of names?</value>
        public IList<string> Aliases
        {
            get; set;
        }
        /// <summary>
        /// The description explaining the command.
        /// </summary>
        /// <value>Description?</value>
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// Creates a new command method with automatic name fetching.
        /// </summary>
        public CommandAttribute() { }
        /// <summary>
        /// Creates a new command method with aliases <paramref name="aliases"/>.
        /// </summary>
        /// <param name="aliases">The array of name aliases for this command</param>
        public CommandAttribute(params string[] aliases) =>
            Aliases = aliases;
    }
}