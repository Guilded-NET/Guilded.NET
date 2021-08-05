using System;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Base for message nodes, text containers and leaves.
    /// </summary>
    public abstract class ChatElement : BaseObject
    {
        /// <summary>
        /// The type of this chat element.
        /// </summary>
        /// <value>Chat element type</value>
        [JsonProperty(Required = Required.Always)]
        public ElementType Object
        {
            get; set;
        }
        /// <summary>
        /// Base for message nodes, text containers and leaves.
        /// </summary>
        /// <param name="type">The type of this chat element</param>
        protected ChatElement(ElementType type) =>
            Object = type;
    }
}