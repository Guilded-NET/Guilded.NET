using System;

namespace Guilded.NET
{
    /// <summary>
    /// Information about Guilded.NET event.
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
        /// Type of the argument that should be given.
        /// </summary>
        /// <value>Type</value>
        public Type ArgumentType
        {
            get; set;
        }
        /// <summary>
        /// Information about Guilded.NET event.
        /// </summary>
        /// <param name="argumentType">Type of the argument that should be given</param>
        public EventInfo(Type argumentType) =>
            ArgumentType = argumentType;
    }
}