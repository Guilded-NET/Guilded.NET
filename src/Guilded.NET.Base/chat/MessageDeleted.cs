using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Events;
    /// <summary>
    /// A message that was recently deleted/removed.
    /// </summary>
    /// <remarks>
    /// <para>A no longer existing message that was deleted/removed
    /// by an author of this message or by a server staff.</para>
    /// <para>This message type can be found in:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="MessageDeletedEvent"/></description>
    ///     </item>
    ///     <item>
    ///         <description>A return value from message deletion methods.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="BaseMessage"/>
    /// <seealso cref="Message"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageDeleted : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The date of when the message was deleted.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> of when the message was removed.</para>
        /// <para>This is recorded by the server and all the delays that were
        /// created by the client will be added as well.</para>
        /// </remarks>
        /// <value>Deleted at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime DeletedAt
        {
            get; set;
        }
        #endregion
    }
}