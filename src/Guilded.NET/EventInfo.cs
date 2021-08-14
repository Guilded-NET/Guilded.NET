using System;

namespace Guilded.NET
{
    /// <summary>
    /// An information about a Guilded.NET event handler.
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        /// An event delegates that can be invoked.
        /// </summary>
        /// <value>Delegates</value>
        public Delegate Invokable
        {
            get; set;
        } = null;
        /// <summary>
        /// The type of the arguments that should be used.
        /// </summary>
        /// <value>Type</value>
        public Type ArgumentType
        {
            get; set;
        }
        /// <summary>
        /// An information about a Guilded.NET event.
        /// </summary>
        /// <param name="argumentType">The type of the arguments that should be used</param>
        public EventInfo(Type argumentType) =>
            ArgumentType = argumentType;
    }
}