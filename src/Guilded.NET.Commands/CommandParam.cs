using System;

namespace Guilded.NET.Commands
{
    /// <summary>
    /// Defines a method argument as a command parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class CommandParamAttribute : Attribute
    {
        /// <summary>
        /// The name of the parameter that will be shown in command usage.
        /// </summary>
        /// <value>Name?</value>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Creates a new command parameter with automatic name fetching.
        /// </summary>
        public CommandParamAttribute() { }
        /// <summary>
        /// Creates a new command parameter with the name <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        public CommandParamAttribute(string name) =>
            Name = name;
    }
}