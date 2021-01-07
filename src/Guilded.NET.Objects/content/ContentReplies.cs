using System;
using System.Collections.Generic;
using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content {
    /// <summary>
    /// A reply to a Guilded document or media.
    /// </summary>
    public class ContentReply: ChannelReply {
        /// <summary>
        /// ID of the document/media this reply was posted under.
        /// </summary>
        /// <value>Document/media ID</value>
        [JsonProperty("contentId", Required = Required.Always)]
        public uint ContentId {
            get; set;
        }
    }
}