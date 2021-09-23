using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET
{
    using Base;
    using Base.Content;
    /// <summary>
    /// A new creatable message.
    /// </summary>
    /// <remarks>
    /// <para>Defines a new message for create and update message methods.</para>
    /// </remarks>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
                ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class CreatableMessage : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The object that will be sent to Guilded.</para>
        /// <blockquote class="warning">
        ///     This does NOT convert the given object to string. Only Markdown string and rich text
        ///     markup are available.
        /// </blockquote>
        /// </remarks>
        /// <value>Rich text markup or Markdown string</value>
        [JsonProperty(Required = Required.Always)]
        public object Content
        {
            get; set;
        }
        /// <inheritdoc cref="Message.ReplyMessageIds"/>
        public IList<Guid> ReplyMessageIds
        {
            get; set;
        }
        /// <inheritdoc cref="Message.IsPrivate"/>
        public bool? IsPrivate
        {
            get; set;
        }
        #endregion
    }
}
