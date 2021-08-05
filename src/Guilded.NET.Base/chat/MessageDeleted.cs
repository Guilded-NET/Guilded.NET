using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Message that was deleted.
    /// </summary>
    public class MessageDeleted : BaseMessage
    {
        #region JSON properties
        /// <summary>
        /// The date of when the message was deleted.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public DateTime DeletedAt
        {
            get; set;
        }
        #endregion
    }
}