using System;
using System.Collections.Generic;
using Guilded.NET.Base;
using Guilded.NET.Base.Embeds;
using Guilded.NET.Base.Content;
using Newtonsoft.Json;

namespace Guilded.NET
{
    /// <summary>
    /// The contents of the message.
    /// </summary>
    /// <remarks>
    /// <para>Defines the contents of the message with which a chat message can be created.</para>
    /// </remarks>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore,
                ItemNullValueHandling = NullValueHandling.Ignore)]
    internal class MessageContent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The contents of the message.
        /// </summary>
        /// <remarks>
        /// <para>The object that will be sent to Guilded.</para>
        /// <note type="warning">This does NOT convert the given object to string. Only Markdown string and rich text markup are available.</note>
        /// </remarks>
        /// <value>Rich text markup or Markdown string</value>
        public object Content
        {
            get; set;
        }
        /// <summary>
        /// The list of embeds in the message.
        /// </summary>
        /// <remarks>
        /// <para>The list of embeds that are in the message.</para>
        /// </remarks>
        /// <value>List of embeds?</value>
        public IList<Embed> Embeds
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
